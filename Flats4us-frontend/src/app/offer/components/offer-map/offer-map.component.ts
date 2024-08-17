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
import {
	IGeoLocation,
	IProperty,
} from 'src/app/real-estate/models/real-estate.models';

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
	public property?: IProperty;

	public map?: Map;

	constructor(public realEstateService: RealEstateService) {
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
		if (this.property) {
			this.addMarkersFromAddress(this.property);
		}
	}

	public ngOnChanges(changes: SimpleChanges): void {
		if (this.map && this.property && changes['property']) {
			this.map?.eachLayer(layer => {
				if (layer instanceof L.Marker) {
					layer.remove();
				}
			});
			this.addMarkersFromAddress(this.property);
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

	public addMarkersFromAddress(property: IProperty) {
		const addressString = `${property.street},${property.city},${property.postalCode},Polska`;
		this.realEstateService
			.getLocation(addressString)
			.pipe(this.untilDestroyed())
			.subscribe((response: IGeoLocation[]) => {
				if (response.length > 0) {
					const { lat, lon } = response[0];
					const markerOptions = {
						clickable: false,
						draggable: false,
						icon: this.getPropertyIcon(property.propertyType ?? 0, false),
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
		const baseUrl = '../../assets/';
		let markerIcon = L.icon({
			iconUrl: `${baseUrl}leafletIconShadow.svg`,
			iconSize: [50, 50],
			iconAnchor: [25, 16],
			popupAnchor: [-6, -16],
		});
		switch (propertyType) {
			case 0:
				markerIcon = L.icon({
					iconUrl: isClicked
						? `${baseUrl}leafletIconClickedShadowFlat.svg`
						: `${baseUrl}leafletIconShadowFlat.svg`,
					iconSize: [50, 50],
					iconAnchor: [25, 16],
					popupAnchor: [-6, -16],
				});
				break;
			case 1:
				markerIcon = L.icon({
					iconUrl: isClicked
						? `${baseUrl}leafletIconClickedShadowHouse.svg`
						: `${baseUrl}leafletIconShadowHouse.svg`,
					iconSize: [50, 50],
					iconAnchor: [25, 16],
					popupAnchor: [-6, -16],
				});
				break;
			case 2:
				markerIcon = L.icon({
					iconUrl: isClicked
						? `${baseUrl}leafletIconClickedShadowRoom.svg`
						: `${baseUrl}leafletIconShadowRoom.svg`,
					iconSize: [50, 50],
					iconAnchor: [25, 16],
					popupAnchor: [-6, -16],
				});
				break;
			default:
				markerIcon = L.icon({
					iconUrl: isClicked
						? `${baseUrl}leafletIconClickedShadow.svg`
						: `${baseUrl}leafletIconShadow.svg`,
					iconSize: [50, 50],
					iconAnchor: [25, 16],
					popupAnchor: [-6, -16],
				});
				break;
		}
		return markerIcon;
	}
}
