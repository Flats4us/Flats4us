import { HttpClient } from '@angular/common/http';
import {
	ChangeDetectionStrategy,
	Component,
	OnChanges,
	OnInit,
} from '@angular/core';
import { Map, map, tileLayer, marker, icon as lIcon } from 'leaflet';
import { BaseComponent } from '@shared/components/base/base.component';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import * as L from 'leaflet';
import { StartService } from '../services/start.service';
import { IFilteredOffers, ISortOption } from '../models/start-site.models';
import { environment } from 'src/environments/environment.prod';
import { Router } from '@angular/router';

@Component({
	selector: 'app-start-map',
	templateUrl: './start-map.component.html',
	styleUrls: ['./start-map.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StartMapComponent
	extends BaseComponent
	implements OnInit, OnChanges
{
	public addresses: string[] = [
		// 'Marszałkowska 1, Warsaw, Poland',
		// 'Nowy Świat 2, Warsaw, Poland',
		// 'Aleje Jerozolimskie 3, Warsaw, Poland',
		// 'Plac Zamkowy 4, Warsaw, Poland',
		// 'Krakowskie Przedmieście 5, Warsaw, Poland',
	];
	public search = '';

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
		this.addresses.push(...this.realEstateService.addresses);
		this.addMarkersFromAddresses(this.addresses);
		this.setMapView([52, 20]);
		this.addMarkersForOffers(this.startService.mapOffersForm?.value);
	}

	public ngOnChanges() {
		this.addresses.push(...this.realEstateService.addresses);
		this.addMarkersFromAddresses(this.addresses);
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
					// const markerIcon = lIcon({
					// 	iconUrl: icon,
					// 	iconSize: [25, 41],
					// 	iconAnchor: [12, 41],
					// 	popupAnchor: [1, -34],
					// 	shadowSize: [41, 41],
					// });
					const markerOptions = {
						title: '1',
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
			title: '1',
			clickable: true,
			draggable: false,
			icon: L.icon({
				iconUrl: '../../assets/leafletIcon.png',
				iconSize: [40, 40],
				iconAnchor: [25, 16],
				popupAnchor: [-3, -76],
			}),
		};
		this.startService.getFilteredOffers(filteredOptions).subscribe(result =>
			result.result.forEach(offer =>
				marker([+offer.property.geoLat, +offer.property.geoLon], markerOptions)
					.addTo(this.map as Map)
					.bindPopup(
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
							`<img src=${this.baseUrl}/${offer.property.images[0].path}></img><a href="/offer/details/${offer.offerId}">Przejdź do widoku oferty</a>`
					)
			)
		);
	}

	public onSubmit(): void {
		this.addMarkersFromAddresses([this.search]);
		this.search = '';
	}
}
