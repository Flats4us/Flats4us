import { HttpClient } from '@angular/common/http';
import {
	ChangeDetectionStrategy,
	Component,
	Input,
	OnChanges,
	OnInit,
	SimpleChanges,
} from '@angular/core';
import { BaseComponent } from '@shared/components/base/base.component';
import { Map, map, tileLayer } from 'leaflet';
import * as L from 'leaflet';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { IMapAddress } from '../../models/offer.models';

@Component({
	selector: 'app-offer-map',
	templateUrl: './offer-map.component.html',
	styleUrls: ['./offer-map.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferMapComponent
	extends BaseComponent
	implements OnInit, OnChanges
{
	@Input()
	public address: IMapAddress | undefined;

	public map: Map | undefined;

	constructor(
		private http: HttpClient,
		public realEstateService: RealEstateService
	) {
		super();
		const container = L.DomUtil.get('map');
		if (container) {
			container.remove();
		}
	}

	public ngOnInit(): void {
		if (!this.map) {
			this.map = map('map').setView([0, 0], 13);
			tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
				attribution: '© OpenStreetMap contributors',
			}).addTo(this.map);
			this.getLocation();
			this.setMapView([52, 20]);
		}
		if (this.address) {
			this.addMarkersFromAddress(this.address);
		}
	}

	public ngOnChanges(changes: SimpleChanges): void {
		if (this.map && this.address && changes['address']) {
			this.addMarkersFromAddress(this.address);
		}
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

	public addMarkersFromAddress(address: IMapAddress) {
		const addressString =
			address.street +
			',' +
			address.city +
			',' +
			address.postalCode +
			',' +
			'Polska';
		const url = `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(
			addressString
		)}&format=json`;

		this.http
			.get<{ lat: number; lon: number }[]>(url)
			.subscribe((response: { lat: number; lon: number }[]) => {
				if (response.length > 0) {
					const { lat, lon } = response[0];
					const markerOptions = {
						clickable: true,
						draggable: false,
						icon: this.getPropertyIcon(address.propertyType ?? 0, false),
					};
					L.marker([+lat, +lon], markerOptions).addTo(this.map as Map);
					this.setMapView([lat, lon]);
				}
			});
	}

	private getPropertyIcon(
		propertyType: number,
		isClicked: boolean
	): L.Icon<L.IconOptions> {
		let markerIcon = L.icon({
			iconUrl: '../../assets/leafletIconShadow.svg',
			iconSize: [50, 50],
			iconAnchor: [25, 16],
			popupAnchor: [-6, -16],
		});
		switch (propertyType) {
			case 0:
				markerIcon = L.icon({
					iconUrl: isClicked
						? '../../assets/leafletIconClickedShadowFlat.svg'
						: '../../assets/leafletIconShadowFlat.svg',
					iconSize: [50, 50],
					iconAnchor: [25, 16],
					popupAnchor: [-6, -16],
				});
				break;
			case 1:
				markerIcon = L.icon({
					iconUrl: isClicked
						? '../../assets/leafletIconClickedShadowHouse.svg'
						: '../../assets/leafletIconShadowHouse.svg',
					iconSize: [50, 50],
					iconAnchor: [25, 16],
					popupAnchor: [-6, -16],
				});
				break;
			case 2:
				markerIcon = L.icon({
					iconUrl: isClicked
						? '../../assets/leafletIconClickedShadowRoom.svg'
						: '../../assets/leafletIconShadowRoom.svg',
					iconSize: [50, 50],
					iconAnchor: [25, 16],
					popupAnchor: [-6, -16],
				});
				break;
			default:
				markerIcon = L.icon({
					iconUrl: isClicked
						? '../../assets/leafletIconClickedShadow.svg'
						: '../../assets/leafletIconShadow.svg',
					iconSize: [50, 50],
					iconAnchor: [25, 16],
					popupAnchor: [-6, -16],
				});
				break;
		}
		return markerIcon;
	}
}
