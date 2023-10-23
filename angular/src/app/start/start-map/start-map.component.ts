import { HttpClient } from '@angular/common/http';
import {
	ChangeDetectionStrategy,
	Component,
	OnDestroy,
	OnInit,
} from '@angular/core';
import { Map, map, tileLayer, marker, icon as lIcon } from 'leaflet';
import { Subject } from 'rxjs';

@Component({
	selector: 'app-start-map',
	templateUrl: './start-map.component.html',
	styleUrls: ['./start-map.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StartMapComponent implements OnInit, OnDestroy {
	public destroyed$: Subject<void> = new Subject();

	public addresses: string[] = [
		'Marszałkowska 1, Warsaw, Poland',
		'Nowy Świat 2, Warsaw, Poland',
		'Aleje Jerozolimskie 3, Warsaw, Poland',
		'Plac Zamkowy 4, Warsaw, Poland',
		'Krakowskie Przedmieście 5, Warsaw, Poland',
	];
	public search = '';

	constructor(private http: HttpClient) {}

	public map: Map | undefined;

	public ngOnInit(): void {
		this.map = map('map').setView([0, 0], 13);
		tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
			attribution: '© OpenStreetMap contributors',
		}).addTo(this.map);
		this.getLocation();

		this.addMarkersFromAddresses(this.addresses);
	}

	public ngOnDestroy(): void {
		this.destroyed$.next();
		this.destroyed$.complete();
	}

	public setMapView([latitude, longitude]: [number, number]): void {
		this.map ? this.map.setView([latitude, longitude], 13) : null;
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
					const markerIcon = lIcon({
						iconUrl: icon,
						iconSize: [25, 41],
						iconAnchor: [12, 41],
						popupAnchor: [1, -34],
						shadowSize: [41, 41],
					});
					marker([+lat, +lon], { icon: markerIcon }).addTo(this.map as Map);
				}
			});
		});
	}

	public onSubmit(): void {
		this.addMarkersFromAddresses([this.search]);
		this.search = '';
	}
}
