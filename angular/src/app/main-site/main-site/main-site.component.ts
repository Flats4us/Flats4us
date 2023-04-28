import { ChangeDetectionStrategy } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { INumeric } from '../../shared/models/main-site.models';
import { IText } from '../../shared/models/main-site.models';
import { ICitiesGroup } from '../../shared/models/main-site.models';
import { IDistrictsGroup } from '../../shared/models/main-site.models';
import { RegionCity } from '../../shared/models/main-site.models';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';

export const filter = (opt: string[], value: string): string[] => {
	const filterValue = value.toLowerCase();

	return opt.filter((item) => item.toLowerCase().startsWith(filterValue));
};

@Component({
	selector: 'app-main-site',
	templateUrl: './main-site.component.html',
	styleUrls: ['./main-site.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MainSiteComponent implements OnInit {
	public showMoreFilters = false;
	public numberOfRecords = 143084;
	public selectedRegion = '';
	public selectedCity = '';

	public showFilters() {
		if (this.showMoreFilters) {
			this.showMoreFilters = false;
		} else {
			this.showMoreFilters = true;
		}
	}
	public getDescription(numberOfRecords: number) {
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

	public changeRegion(event: any) {
		this.selectedRegion = event.value;
	}

	public changeCity(event: any) {
		this.selectedCity = event.value;
	}

	public onSubmit() {
		if (this.mainSiteForm.valid) {
			this.router.navigate(['/']);
		}
	}

	public mainSiteForm: FormGroup = new FormGroup({});

	public citiesGroupOptions!: Observable<ICitiesGroup[]>;
	public districtGroupOptions!: Observable<IDistrictsGroup[]>;

	public regionCityArray: RegionCity[] = [];

	constructor(
		private formBuilder: FormBuilder,
		private http: HttpClient,
		private router: Router
	) {
		this.mainSiteForm = formBuilder.group({
			regionsGroup: new FormControl('', Validators.required),
			citiesGroup: new FormControl('', Validators.required),
			distance: new FormControl('', Validators.required),
			property: new FormControl('', Validators.required),
			minPrice: new FormControl('', [
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			maxPrice: new FormControl('', [
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			districtsGroup: new FormControl(''),
			minArea: new FormControl('', [
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			maxArea: new FormControl('', [
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			year: new FormControl('', []),
			rooms: new FormControl('', [
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			floors: new FormControl('', [
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
		});
		this.http
			.get('./assets/wojewodztwa_miasta.csv', { responseType: 'text' })
			.subscribe((data) => {
				const csvToRowArray = data.split('\n');
				for (let index = 1; index < csvToRowArray.length - 1; index++) {
					const row = csvToRowArray[index].split(';');
					const lowerCaseRegion = row[2].trim().toLowerCase();
					this.regionCityArray.push(new RegionCity(lowerCaseRegion, row[1]));

					this.citiesGroups
						.filter((group) => group.region == lowerCaseRegion)
						.map((group) => group.cities.push(row[1]));
				}
			});
	}

	public ngOnInit() {
		this.mainSiteForm
			.get('regionsGroup')
			?.valueChanges.subscribe((selectedValue) =>
				selectedValue === null
					? (this.selectedRegion = '')
					: (this.selectedRegion = selectedValue)
			);

		this.citiesGroupOptions = this.mainSiteForm
			.get('citiesGroup')!
			.valueChanges.pipe(
				startWith(''),
				map((value) => this.filterCitiesGroup(value || ''))
			);
		this.mainSiteForm
			.get('citiesGroup')
			?.valueChanges.subscribe((selectedValue) =>
				selectedValue === null
					? (this.selectedCity = '')
					: (this.selectedCity = selectedValue)
			);
		this.districtGroupOptions = this.mainSiteForm
			.get('districtsGroup')!
			.valueChanges.pipe(
				startWith(''),
				map((value) => this.filterDistrictsGroup(value || ''))
			);
	}

	private filterCitiesGroup(value: string): ICitiesGroup[] {
		return this.citiesGroups
			.map((group) => ({
				region: group.region,
				cities: filter(group.cities, value),
			}))
			.filter(
				(group) => group.cities.length > 0 && group.region === this.selectedRegion
			);
	}
	private filterDistrictsGroup(value: string): IDistrictsGroup[] {
		return this.districtGroups
			.map((group) => ({
				city: group.city,
				districts: filter(group.districts, value),
			}))
			.filter(
				(group) => group.districts.length > 0 && group.city === this.selectedCity
			);
	}

	public navigateToFlat() {
		this.router.navigate(['/']);
	}

	public citiesGroups: ICitiesGroup[] = [
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

	public districtGroups: IDistrictsGroup[] = [
		{
			city: 'Warszawa',
			districts: [
				'Bemowo',
				'Białołęka',
				'Bielany',
				'Mokotów',
				'Ochota',
				'Praga-Południe',
				'Praga-Północ',
				'Rembertów',
				'Śródmieście',
				'Targówek',
				'Ursus',
				'Ursynów',
				'Wawer',
				'Wesoła',
				'Wilanów',
				'Włochy',
				'Wola',
				'Żoliborz',
			],
		},
		{
			city: 'Gdańsk',
			districts: [
				'Aniołki',
				'Brętowo',
				'Brzeźno',
				'Chełm',
				'Jasień',
				'Kokoszki',
				'Krakowiec-Górki Zachodnie',
				'Letnica',
				'Matarnia',
				'Młyniska',
				'Nowy Port',
				'Oliwa',
				'Olszynka',
				'Orunia-Św. Wojciech-Lipce',
				'Osowa',
				'Piecki-Migowo',
				'Przeróbka',
				'Przymorze Małe',
				'Przymorze Wielkie',
				'Rudniki',
				'Siedlce',
				'Stogi',
				'Strzyża',
				'Suchanino',
				'Śródmieście',
				'Ujeścisko-Łostowice',
				'VII Dwór',
				'Wrzeszcz Dolny',
				'Wrzeszcz Górny',
				'Wyspa Sobieszewska',
				'Wzgórze Mickiewicza',
				'Zaspa-Młyniec',
				'Zaspa-Rozstaje',
				'Żabianka-Wejhera-Jelitkowo-Tysiąclecia',
			],
		},
		{
			city: 'Kraków',
			districts: [
				'Stare Miasto',
				'Grzegórzki',
				'Prądnik Czerwony',
				'Prądnik Biały',
				'Krowodrza',
				'Bronowice',
				'Zawierzyniec',
				'Dębniki',
				'Łagiewniki-Borek Fałęcki',
				'Swoszowice',
				'Podgórze Duchackie',
				'Bieżanów-Prokocim',
				'Podgórze',
				'Czyżyny',
				'Mistrzejowice',
				'Bieńczyce',
				'Wzgórze Krzesławickie',
				'Nowa Huta',
			],
		},
	];

	public regions: IText[] = [
		{ value: 'dolnośląskie' },
		{ value: 'kujawsko-pomorskie' },
		{ value: 'lubelskie' },
		{ value: 'lubuskie' },
		{ value: 'łódzkie' },
		{ value: 'małopolskie' },
		{ value: 'mazowieckie' },
		{ value: 'opolskie' },
		{ value: 'podkarpackie' },
		{ value: 'podlaskie' },
		{ value: 'pomorskie' },
		{ value: 'śląskie' },
		{ value: 'świętokrzyskie' },
		{ value: 'warmińsko-mazurskie' },
		{ value: 'wielkopolskie' },
		{ value: 'zachodniopomorskie' },
	];

	public areaFroms: INumeric[] = [
		{ value: 0, viewValue: '0 m²' },
		{ value: 20, viewValue: '20 m²' },
		{ value: 40, viewValue: '40 m²' },
		{ value: 60, viewValue: '60 m²' },
		{ value: 80, viewValue: '80 m²' },
		{ value: 100, viewValue: '100 m²' },
		{ value: 120, viewValue: '120 m²' },
	];
	public areaTos: INumeric[] = [
		{ value: 20, viewValue: '20 m²' },
		{ value: 40, viewValue: '40 m²' },
		{ value: 60, viewValue: '60 m²' },
		{ value: 80, viewValue: '80 m²' },
		{ value: 100, viewValue: '100 m²' },
		{ value: 120, viewValue: '120 m²' },
		{ value: 140, viewValue: '140 m²' },
	];
	public priceMaxs: INumeric[] = [
		{ value: 1000, viewValue: '1000 zł' },
		{ value: 2000, viewValue: '2000 zł' },
		{ value: 3000, viewValue: '3000 zł' },
		{ value: 4000, viewValue: '4000 zł' },
		{ value: 5000, viewValue: '5000 zł' },
		{ value: 6000, viewValue: '6000 zł' },
		{ value: 7000, viewValue: '7000 zł' },
	];
	public numberOfRooms: INumeric[] = [
		{ value: 1, viewValue: '1' },
		{ value: 2, viewValue: '2' },
		{ value: 3, viewValue: '3' },
		{ value: 4, viewValue: '4' },
		{ value: 5, viewValue: '5' },
		{ value: 6, viewValue: '6' },
		{ value: 7, viewValue: '7' },
	];
	public distances: INumeric[] = [
		{ value: 0, viewValue: '0 km' },
		{ value: 5, viewValue: '5 km' },
		{ value: 10, viewValue: '10 km' },
		{ value: 15, viewValue: '15 km' },
		{ value: 25, viewValue: '25 km' },
		{ value: 50, viewValue: '50 km' },
		{ value: 75, viewValue: '75 km' },
	];
	public numberOfFloors: INumeric[] = [
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
	public yearOfBuilds: IText[] = [
		{ value: 'do 1950' },
		{ value: 'od 1950 do 1989' },
		{ value: 'od 1990 do 2010' },
		{ value: 'od 2010' },
	];
	public properties: IText[] = [
		{ value: 'Kawalerka' },
		{ value: 'Mieszkanie' },
		{ value: 'Pokój' },
	];
}
