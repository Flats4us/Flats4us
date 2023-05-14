import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Map, map, tileLayer } from 'leaflet';

@Component({
	selector: 'app-start-map',
	templateUrl: './start-map.component.html',
	styleUrls: ['./start-map.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StartMapComponent implements OnInit {
	public map: Map | undefined;

	public ngOnInit(): void {
		this.map = map('map').setView([0, 0], 13);
		tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
			attribution: '© OpenStreetMap contributors',
		}).addTo(this.map);
		this.getLocation();
	}

	public setMapView([latitude, longitude]: [number, number]): void {
		this.map ? this.map.setView([latitude, longitude], 13) : null;
	}

	private getLocation(): void {
		if (navigator.geolocation) {
			navigator.geolocation.getCurrentPosition((position) =>
				this.setMapView([position.coords.latitude, position.coords.longitude])
			);
		}
	}
}
