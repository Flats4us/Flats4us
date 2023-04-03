import { ChangeDetectionStrategy, Component } from '@angular/core';

interface Area {
	value: Number;
	viewValue: string;
}

interface City {
	value: string;
	viewValue: string;
}

interface Room {
	value: number;
	viewValue: string;
}

interface Price {
	value: number;
	viewValue: string;
}

interface Distance {
	value: number;
	viewValue: string;
}

interface Floor {
	value: number;
	viewValue: string;
}

interface Year {
	value: number;
	viewValue: string;
}

interface Property {
	value: string;
	viewValue: string;
}

@Component({
	selector: 'app-main-site',
	templateUrl: './main-site.component.html',
	styleUrls: ['./main-site.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainSiteComponent {
	showMoreFilters = false;

	showFilters() {
		if (this.showMoreFilters) this.showMoreFilters = false;
		else this.showMoreFilters = true;
	}
	search() {}

	cities: City[] = [
		{ value: 'Warszawa', viewValue: 'Warszawa' },
		{ value: 'Poznań', viewValue: 'Poznań' },
		{ value: 'Kraków', viewValue: 'Kraków' },
	];
	areaFroms: Area[] = [
		{ value: 0, viewValue: '0 m²' },
		{ value: 40, viewValue: '40 m²' },
		{ value: 80, viewValue: '80 m²' },
	];
	areaTos: Area[] = [
		{ value: 40, viewValue: '40 m²' },
		{ value: 80, viewValue: '80 m²' },
		{ value: 100, viewValue: '100 m²' },
	];
	priceMaxs: Price[] = [
		{ value: 1000, viewValue: '1000 zł' },
		{ value: 2000, viewValue: '2000 zł' },
		{ value: 3000, viewValue: '3000 zł' },
	];
	numberOfRooms: Room[] = [
		{ value: 1, viewValue: '1' },
		{ value: 2, viewValue: '2' },
		{ value: 3, viewValue: '>2' },
	];
	distances: Distance[] = [
		{ value: 1, viewValue: '0 km' },
		{ value: 2, viewValue: '5 km' },
		{ value: 3, viewValue: '10 km' },
		{ value: 4, viewValue: '15 km' },
		{ value: 5, viewValue: '25 km' },
		{ value: 6, viewValue: '50 km' },
		{ value: 7, viewValue: '75 km' },
	];
	numberOfFloors: Floor[] = [
		{ value: 1, viewValue: '1' },
		{ value: 2, viewValue: '2' },
		{ value: 3, viewValue: '>2' },
	];
	yearOfBuilds: Year[] = [
		{ value: 1, viewValue: 'do 1950' },
		{ value: 2, viewValue: '1950-1989' },
		{ value: 3, viewValue: '1990-2010' },
		{ value: 4, viewValue: 'po 2010' },
	];
	properties: Property[] = [
		{ value: 'Kawalerka', viewValue: 'Kawalerka' },
		{ value: 'Mieszkanie', viewValue: 'Mieszkanie' },
		{ value: 'Pokój', viewValue: 'Pokój' },
	];
}
