import { ChangeDetectionStrategy, OnChanges } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

export interface CitiesGroup {
	region: string;
	cities: string[];
}

interface Region {
	value: string;
	viewValue: string;
}

interface Area {
	value: number;
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
	value: string;
	viewValue: string;
}

interface Property {
	value: string;
	viewValue: string;
}

export const _filter = (opt: string[], value: string): string[] => {
	const filterValue = value.toLowerCase();

	return opt.filter((item) => item.toLowerCase().startsWith(filterValue));
};

@Component({
	selector: 'app-main-site',
	templateUrl: './main-site.component.html',
	styleUrls: ['./main-site.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainSiteComponent implements OnInit, OnChanges {
	showMoreFilters = false;
	numberOfRecords = 143084;
	selectedRegion = '';

	showFilters() {
		if (this.showMoreFilters) this.showMoreFilters = false;
		else this.showMoreFilters = true;
	}
	getDescription(numberOfRecords: number) {
		if (numberOfRecords == 1) {
			return `${numberOfRecords} oferta`;
		} else if (
			(numberOfRecords > 1 && numberOfRecords <= 4) ||
			(numberOfRecords > 20 &&
				numberOfRecords % 10 > 1 &&
				numberOfRecords % 10 <= 4)
		) {
			return `${numberOfRecords} oferty`;
		} else {
			return `${numberOfRecords} ofert`;
		}
	}

	changeRegion(event: any) {
		this.selectedRegion = event.value;
	}

	// eslint-disable-next-line @typescript-eslint/no-empty-function
	search() {}

	citiesForm = this._formBuilder.group({
		citiesGroup: '',
		regionsGroup: '',
	});

	citiesGroups: CitiesGroup[] = [
		{
			region: 'dolnośląskie',
			cities: [],
		},
		{
			region: 'kujawsko-pomorskie',
			cities: [],
		},
		{
			region: 'lubelskie',
			cities: [],
		},
		{
			region: 'lubuskie',
			cities: [],
		},
		{
			region: 'łódzkie',
			cities: [],
		},
		{
			region: 'małopolskie',
			cities: [],
		},
		{
			region: 'mazowieckie',
			cities: [],
		},
		{
			region: 'opolskie',
			cities: [],
		},
		{
			region: 'podkarpackie',
			cities: [],
		},
		{
			region: 'podlaskie',
			cities: [],
		},
		{
			region: 'pomorskie',
			cities: [],
		},
		{
			region: 'śląskie',
			cities: [],
		},
		{
			region: 'świętokrzyskie',
			cities: [],
		},
		{
			region: 'warmińsko-mazurskie',
			cities: [],
		},
		{
			region: 'wielkopolskie',
			cities: [],
		},
		{
			region: 'zachodniopomorskie',
			cities: [],
		},
	];

	citiesGroupOptions!: Observable<CitiesGroup[]>;

	public regionCityArray: RegionCity[] = [];
	constructor(
		private _formBuilder: FormBuilder,
		private http: HttpClient,
		private router: Router
	) {
		this.http
			.get('./assets/wojewodztwa_miasta.csv', { responseType: 'text' })
			.subscribe(
				(data) => {
					const csvToRowArray = data.split('\n');
					for (let index = 1; index < csvToRowArray.length - 1; index++) {
						const row = csvToRowArray[index].split(';');
						const lowerCaseRegion = row[2].trim().toLowerCase();
						this.regionCityArray.push(new RegionCity(lowerCaseRegion, row[1]));

						this.citiesGroups
							.filter((group) => group.region == lowerCaseRegion)
							.map((group) => group.cities.push(row[1]));
					}

					console.log(this.regionCityArray);
				},
				(error) => {
					console.log(error);
				}
			);
	}

	ngOnChanges() {
		this.citiesForm
			.get('regionsGroup')
			?.valueChanges.subscribe((selectedValue) =>
				selectedValue === null
					? (this.selectedRegion = '')
					: (this.selectedRegion = selectedValue)
			);
	}

	ngOnInit() {
		this.citiesGroupOptions = this.citiesForm
			.get('citiesGroup')!
			.valueChanges.pipe(
				startWith(''),
				map((value) => this._filterGroup(value || ''))
			);
	}

	private _filterGroup(value: string): CitiesGroup[] {
		if (value) {
			return this.citiesGroups
				.map((group) => ({
					region: group.region,
					cities: _filter(group.cities, value),
				}))
				.filter(
					(group) => group.cities.length > 0 && group.region === this.selectedRegion
				);
		}
		return this.citiesGroups.filter(
			(group) => group.region === this.selectedRegion
		);
	}

	navigateToFlat() {
		this.router.navigate(['/']);
	}

	regions: Region[] = [
		{ value: 'dolnośląskie', viewValue: 'dolnośląskie' },
		{ value: 'kujawsko-pomorskie', viewValue: 'kujawsko-pomorskie' },
		{ value: 'lubelskie', viewValue: 'lubelskie' },
		{ value: 'lubuskie', viewValue: 'lubuskie' },
		{ value: 'łódzkie', viewValue: 'łódzkie' },
		{ value: 'małopolskie', viewValue: 'małopolskie' },
		{ value: 'mazowieckie', viewValue: 'mazowieckie' },
		{ value: 'opolskie', viewValue: 'opolskie' },
		{ value: 'podkarpackie', viewValue: 'podkarpackie' },
		{ value: 'podlaskie', viewValue: 'podlaskie' },
		{ value: 'pomorskie', viewValue: 'pomorskie' },
		{ value: 'śląskie', viewValue: 'śląskie' },
		{ value: 'świętokrzyskie', viewValue: 'świętokrzyskie' },
		{ value: 'warmińsko-mazurskie', viewValue: 'warmińsko-mazurskie' },
		{ value: 'wielkopolskie', viewValue: 'wielkopolskie' },
		{ value: 'zachodniopomorskie', viewValue: 'zachodniopomorskie' },
	];

	areaFroms: Area[] = [
		{ value: 0, viewValue: '0 m²' },
		{ value: 20, viewValue: '20 m²' },
		{ value: 40, viewValue: '40 m²' },
		{ value: 60, viewValue: '60 m²' },
		{ value: 80, viewValue: '80 m²' },
		{ value: 100, viewValue: '100 m²' },
		{ value: 120, viewValue: '120 m²' },
	];
	areaTos: Area[] = [
		{ value: 20, viewValue: '20 m²' },
		{ value: 40, viewValue: '40 m²' },
		{ value: 60, viewValue: '60 m²' },
		{ value: 80, viewValue: '80 m²' },
		{ value: 100, viewValue: '100 m²' },
		{ value: 120, viewValue: '120 m²' },
		{ value: 140, viewValue: '140 m²' },
	];
	priceMaxs: Price[] = [
		{ value: 1000, viewValue: '1000 zł' },
		{ value: 2000, viewValue: '2000 zł' },
		{ value: 3000, viewValue: '3000 zł' },
		{ value: 4000, viewValue: '4000 zł' },
		{ value: 5000, viewValue: '5000 zł' },
		{ value: 6000, viewValue: '6000 zł' },
		{ value: 7000, viewValue: '7000 zł' },
	];
	numberOfRooms: Room[] = [
		{ value: 1, viewValue: '1' },
		{ value: 2, viewValue: '2' },
		{ value: 3, viewValue: '3' },
		{ value: 4, viewValue: '4' },
		{ value: 5, viewValue: '5' },
		{ value: 6, viewValue: '6' },
		{ value: 7, viewValue: '7' },
	];
	distances: Distance[] = [
		{ value: 0, viewValue: '0 km' },
		{ value: 5, viewValue: '5 km' },
		{ value: 10, viewValue: '10 km' },
		{ value: 15, viewValue: '15 km' },
		{ value: 25, viewValue: '25 km' },
		{ value: 50, viewValue: '50 km' },
		{ value: 75, viewValue: '75 km' },
	];
	numberOfFloors: Floor[] = [
		{ value: 1, viewValue: '1' },
		{ value: 2, viewValue: '2' },
		{ value: 3, viewValue: '3' },
		{ value: 4, viewValue: '4' },
		{ value: 5, viewValue: '5' },
		{ value: 10, viewValue: '10' },
		{ value: 20, viewValue: '20' },
		{ value: 50, viewValue: '40' },
		{ value: 100, viewValue: '80' },
	];
	yearOfBuilds: Year[] = [
		{ value: 'do 1950', viewValue: 'do 1950' },
		{ value: 'od 1950 do 1989', viewValue: 'od 1950 do 1989' },
		{ value: 'od 1990 do 2010', viewValue: 'od 1990 do 2010' },
		{ value: 'od 2010', viewValue: 'od 2010' },
	];
	properties: Property[] = [
		{ value: 'Kawalerka', viewValue: 'Kawalerka' },
		{ value: 'Mieszkanie', viewValue: 'Mieszkanie' },
		{ value: 'Pokój', viewValue: 'Pokój' },
	];
}

export class RegionCity {
	region: string;
	city: string;

	constructor(region: string, city: string) {
		this.region = region;
		this.city = city;
	}
}
