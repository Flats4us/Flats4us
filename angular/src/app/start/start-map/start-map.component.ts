import { HttpClient } from '@angular/common/http';
import {
	ChangeDetectionStrategy,
	Component,
	ElementRef,
	OnInit,
	ViewChild,
} from '@angular/core';
import { Map, map, tileLayer, marker, icon as lIcon } from 'leaflet';
import { BaseComponent } from '@shared/components/base/base.component';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import * as L from 'leaflet';
import 'leaflet.markercluster';

import { StartService } from '../services/start.service';
import { IFilteredMapOffers } from '../models/start-site.models';
import { environment } from 'src/environments/environment.prod';
import { Router } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { OfferService } from 'src/app/offer/services/offer.service';
import { IOffer } from 'src/app/offer/models/offer.models';
import { Observable } from 'rxjs';

@Component({
	selector: 'app-start-map',
	templateUrl: './start-map.component.html',
	styleUrls: ['./start-map.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StartMapComponent extends BaseComponent implements OnInit {
	@ViewChild('searchInput')
	public searchInput!: ElementRef;

	constructor(
		private http: HttpClient,
		public realEstateService: RealEstateService,
		private startService: StartService,
		private offerService: OfferService,
		private router: Router
	) {
		super();
	}

	public map: Map | undefined;

	public markerCluster = L.markerClusterGroup();

	public markersList: L.Marker[] = [];

	protected baseUrl = environment.apiUrl.replace('/api', '');

	public filteredOptions: IFilteredMapOffers = {
		regionsGroup: '',
		citiesGroup: '',
		distance: null,
		property: [],
		minPrice: null,
		maxPrice: null,
		districtsGroup: '',
		minArea: null,
		maxArea: null,
		year: [],
		rooms: null,
		floors: null,
		equipment: [],
	};

	public ngOnInit(): void {
		this.map = map('map').setView([0, 0], 13);
		tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
			attribution: '© OpenStreetMap contributors',
		}).addTo(this.map);
		this.getLocation();
		this.setMapView([52, 20]);
		this.addMarkersForOffers(this.startService.mapOffersForm?.value);
		this.map.addLayer(this.markerCluster);
	}

	public setMapView([latitude, longitude]: [number, number]): void {
		this.map ? this.map.setView([latitude, longitude], 6) : null;
	}

	private getLocation(): void {
		if (navigator.geolocation) {
			navigator.geolocation.getCurrentPosition(position =>
				this.setMapView([position.coords.latitude, position.coords.longitude])
			);
		}
	}

	public addMarkersFromAddresses(addresses: string[]) {
		addresses.forEach(address => {
			const url = `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(
				address
			)}&format=json`;

			this.http
				.get(url)
				.pipe(this.untilDestroyed())
				.subscribe((response: any) => {
					if (response.length > 0) {
						const { lat, lon, icon } = response[0];
						const markerOptions = {
							clickable: true,
							draggable: false,
							icon: L.icon({
								iconUrl: '../../assets/leafletIconShadowHouse.svg',
								iconSize: [60, 60],
								iconAnchor: [25, 16],
								popupAnchor: [6, -16],
							}),
						};
						marker([+lat, +lon], markerOptions).addTo(this.map as Map);
					}
				});
		});
	}

	public addMarkersForOffers(filteredOptions: IFilteredMapOffers) {
		const markerOptions = {
			clickable: true,
			draggable: false,
			icon: L.icon({
				iconUrl: '../../assets/leafletIconShadow.svg',
				iconSize: [60, 60],
				iconAnchor: [25, 16],
				popupAnchor: [6, -16],
			}),
		};
		this.startService
			.getFilteredMapOffers(filteredOptions)
			.pipe(this.untilDestroyed())
			.subscribe(result => {
				result.result.forEach(offer => {
					const marker = L.marker(
						[+offer.property.geoLat, +offer.property.geoLon],
						markerOptions
					);
					let mapOffer$: Observable<IOffer> = this.offerService.getOfferById(0);
					const popupContent = document.createElement('div');
					popupContent.setAttribute('id', 'popup');
					marker
						.addEventListener('mouseover', () => {
							mapOffer$ = this.offerService.getOfferById(offer.offerId);
							mapOffer$.pipe(this.untilDestroyed()).subscribe(mapOffer => {
								let description = '';
								description =
									'<style>' +
									'.inner-element:hover {' +
									'cursor: pointer;' +
									'}</style>' +
									'<b>' +
									this.realEstateService.propertyTypes.get(offer.property.propertyType) +
									'</b>' +
									' ' +
									mapOffer.property.area +
									' m², ' +
									mapOffer.property.city +
									', ulica ' +
									mapOffer.property.street +
									' ' +
									mapOffer.property.number +
									', cena: ' +
									mapOffer.price +
									' zł' +
									`<img id="propertyImage" src=${this.baseUrl}/${mapOffer.property.images[0]?.path} class="inner-element"></img>`;
								popupContent.innerHTML = description;
							});
						})
						.setIcon(this.getPropertyIcon(offer.property.propertyType, false))
						.bindPopup(popupContent, {
							autoPan: true,
							autoClose: false,
							keepInView: true,
						})
						.on('popupopen', e => {
							e.target.setIcon(
								this.getPropertyIcon(offer.property.propertyType, true)
							);
							document?.getElementById('popup')?.addEventListener('click', () => {
								this.router.navigate(['offer', 'details', offer.offerId]);
							});
						});
					this.markersList.push(marker);
				});
				this.markerCluster.addLayers(this.markersList);
			});
	}

	public navigateToFlat(id: number) {
		this.router.navigate(['offer', 'details', id]);
	}

	public onSubmit(): void {
		this.markerCluster.removeLayers(this.markersList);
		this.map?.eachLayer(layer => {
			if (layer instanceof L.Marker) {
				layer.remove();
			}
		});
		this.markersList = [];
		const searchForm: FormGroup = this.startService.mapOffersForm;
		if (this.searchInput.nativeElement.value.includes(',')) {
			const searchArray = this.searchInput.nativeElement.value.split(',');
			const regionName = searchArray[0].trim().toLowerCase();
			const cityName = searchArray[1].trim().toLowerCase();
			searchForm.patchValue({ regionsGroup: regionName });
			searchForm.patchValue({ citiesGroup: cityName });
		}
		this.addMarkersForOffers(searchForm.value);
	}

	private getPropertyIcon(
		propertyType: number,
		isClicked: boolean
	): L.Icon<L.IconOptions> {
		let markerIcon = L.icon({
			iconUrl: '../../assets/leafletIconShadow.svg',
			iconSize: [60, 60],
			iconAnchor: [25, 16],
			popupAnchor: [6, -16],
		});
		switch (propertyType) {
			case 0:
				markerIcon = L.icon({
					iconUrl: isClicked
						? '../../assets/leafletIconClickedShadowFlat.svg'
						: '../../assets/leafletIconShadowFlat.svg',
					iconSize: [60, 60],
					iconAnchor: [25, 16],
					popupAnchor: [6, -16],
				});
				break;
			case 1:
				markerIcon = L.icon({
					iconUrl: isClicked
						? '../../assets/leafletIconClickedShadowHouse.svg'
						: '../../assets/leafletIconShadowHouse.svg',
					iconSize: [60, 60],
					iconAnchor: [25, 16],
					popupAnchor: [6, -16],
				});
				break;
			case 2:
				markerIcon = L.icon({
					iconUrl: isClicked
						? '../../assets/leafletIconClickedShadowRoom.svg'
						: '../../assets/leafletIconShadowRoom.svg',
					iconSize: [60, 60],
					iconAnchor: [25, 16],
					popupAnchor: [6, -16],
				});
				break;
			default:
				markerIcon = L.icon({
					iconUrl: isClicked
						? '../../assets/leafletIconClickedShadow.svg'
						: '../../assets/leafletIconShadow.svg',
					iconSize: [60, 60],
					iconAnchor: [25, 16],
					popupAnchor: [6, -16],
				});
				break;
		}
		return markerIcon;
	}
}
