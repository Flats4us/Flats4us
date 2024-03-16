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
import { StartService } from '../services/start.service';
import { IFilteredOffers, ISortOption } from '../models/start-site.models';
import { environment } from 'src/environments/environment.prod';
import { Router, RouterLink } from '@angular/router';
import { FormGroup } from '@angular/forms';

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
		private router: Router
	) {
		super();
	}

	public map: Map | undefined;

	protected baseUrl = environment.apiUrl.replace('/api', '');

	public filteredOptions: IFilteredOffers = {
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
		sorting: {} as ISortOption,
		pageIndex: 0,
		pageSize: 48,
	};

	public ngOnInit(): void {
		this.map = map('map').setView([0, 0], 13);
		tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
			attribution: '© OpenStreetMap contributors',
		}).addTo(this.map);
		this.getLocation();
		this.setMapView([52, 20]);
		this.addMarkersForOffers(this.startService.mapOffersForm?.value);
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

			this.http.get(url).subscribe((response: any) => {
				if (response.length > 0) {
					const { lat, lon, icon } = response[0];
					const markerOptions = {
						clickable: true,
						draggable: false,
						icon: L.icon({
							iconUrl: '../../assets/leafletIcon.png',
							iconSize: [40, 40],
							iconAnchor: [25, 16],
							popupAnchor: [-3, -76],
						}),
					};
					marker([+lat, +lon], markerOptions).addTo(this.map as Map);
				}
			});
		});
	}

	public addMarkersForOffers(filteredOptions: IFilteredOffers) {
		const markerOptions = {
			clickable: true,
			draggable: false,
			icon: L.icon({
				iconUrl: '../../assets/leafletIcon.png',
				iconSize: [40, 40],
				iconAnchor: [25, 16],
			}),
		};
		this.startService.getFilteredOffers(filteredOptions).subscribe(result =>
			result.result.forEach(offer =>
				marker([+offer.property.geoLat, +offer.property.geoLon], markerOptions)
					.addTo(this.map as Map)
					.bindPopup(
						'<style>' +
							'.inner-element:hover {' +
							'cursor: pointer;' +
							'}</style>' +
							'<b>' +
							this.realEstateService.propertyTypes.get(offer.property.propertyType) +
							'</b>' +
							' ' +
							offer.property.area +
							' m², ' +
							offer.property.city +
							', ulica ' +
							offer.property.street +
							' ' +
							offer.property.number +
							', cena: ' +
							offer.price +
							' zł' +
							`<img id="propertyImage" src=${this.baseUrl}/${offer.property.images[0].path} class="inner-element"></img><a id="propertyLink" class="inner-element">Przejdź do widoku oferty</a>`
					)
					.on('popupopen', () => {
						document
							?.getElementById('propertyImage')
							?.addEventListener('click', () => {
								this.router.navigate(['offer', 'details', offer.offerId]);
							});
						document
							?.getElementById('propertyLink')
							?.addEventListener('click', () => {
								this.router.navigate(['offer', 'details', offer.offerId]);
							});
					})
			)
		);
	}

	public navigateToFlat(id: number) {
		this.router.navigate(['offer', 'details', id]);
	}

	public onSubmit(): void {
		this.map?.eachLayer(layer => {
			if (layer instanceof L.Marker) {
				layer.remove();
			}
		});
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
}
