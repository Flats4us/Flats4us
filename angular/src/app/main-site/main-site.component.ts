import { ChangeDetectionStrategy } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { INumeric } from '../shared/models/main-site.models';
import { IGroup } from '../shared/models/main-site.models';
import { IRegionCity } from '../shared/models/main-site.models';
import { FormGroup, FormControl } from '@angular/forms';
import { Validators } from '@angular/forms';

export const filter = (opt: string[], value: string): string[] => {
	const filterValue = value.toLowerCase();

	return opt.filter((item) => item.toLowerCase().includes(filterValue));
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
	public isElevator = false;

	public showFilters() {
		this.showMoreFilters = !this.showMoreFilters;
	}

	public onSubmit() {
		if (this.mainSiteForm.valid) {
			this.router.navigate(['/']);
		}
	}

	public mainSiteForm: FormGroup = new FormGroup({});

	public citiesGroupOptions!: Observable<IGroup[]>;
	public districtGroupOptions!: Observable<IGroup[]>;

	public regionCityArray: IRegionCity[] = [];

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
			year: new FormControl(''),
			rooms: new FormControl('', [
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			floors: new FormControl('', [
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			elevator: new FormControl(''),
		});
		this.http
			.get('./assets/wojewodztwa_miasta.csv', { responseType: 'text' })
			.subscribe((data) => {
				const csvToRowArray = data.split('\n');
				for (let index = 1; index < csvToRowArray.length - 1; index++) {
					const row = csvToRowArray[index].split(';');
					const lowerCaseRegion = row[2].trim().toLowerCase();
					this.regionCityArray.push(<IRegionCity>{
						region: lowerCaseRegion,
						city: row[1],
					});

					this.citiesGroups
						.filter((group) => group.whole == lowerCaseRegion)
						.map((group) => group.parts.push(row[1]));
				}
			});
	}

	public ngOnInit() {
		this.citiesGroupOptions = this.mainSiteForm
			.get('citiesGroup')!
			.valueChanges.pipe(
				startWith(''),
				map((value) => this.filterCitiesGroup(value || ''))
			);

		this.districtGroupOptions = this.mainSiteForm
			.get('districtsGroup')!
			.valueChanges.pipe(
				startWith(''),
				map((value) => this.filterDistrictsGroup(value || ''))
			);
		this.mainSiteForm
			.get('elevator')
			?.valueChanges.subscribe((value) => (this.isElevator = value));
	}

	private filterCitiesGroup(value: string): IGroup[] {
		return this.citiesGroups
			.map((group) => ({
				whole: group.whole,
				parts: filter(group.parts, value),
			}))
			.filter(
				(group) =>
					group.parts.length > 0 &&
					group.whole === this.mainSiteForm.get('regionsGroup')?.value
			);
	}
	private filterDistrictsGroup(value: string): IGroup[] {
		return this.districtGroups
			.map((group) => ({
				whole: group.whole,
				parts: filter(group.parts, value),
			}))
			.filter(
				(group) =>
					group.parts.length > 0 &&
					group.whole === this.mainSiteForm.get('citiesGroup')?.value
			);
	}

	public navigateToFlat() {
		this.router.navigate(['/']);
	}

	public citiesGroups: IGroup[] = [
		{
			whole: 'dolnośląskie',
			parts: [],
		},
		{
			whole: 'kujawsko-pomorskie',
			parts: [],
		},
		{
			whole: 'lubelskie',
			parts: [],
		},
		{
			whole: 'lubuskie',
			parts: [],
		},
		{
			whole: 'łódzkie',
			parts: [],
		},
		{
			whole: 'małopolskie',
			parts: [],
		},
		{
			whole: 'mazowieckie',
			parts: [],
		},
		{
			whole: 'opolskie',
			parts: [],
		},
		{
			whole: 'podkarpackie',
			parts: [],
		},
		{
			whole: 'podlaskie',
			parts: [],
		},
		{
			whole: 'pomorskie',
			parts: [],
		},
		{
			whole: 'śląskie',
			parts: [],
		},
		{
			whole: 'świętokrzyskie',
			parts: [],
		},
		{
			whole: 'warmińsko-mazurskie',
			parts: [],
		},
		{
			whole: 'wielkopolskie',
			parts: [],
		},
		{
			whole: 'zachodniopomorskie',
			parts: [],
		},
	];

	public districtGroups: IGroup[] = [
		{
			whole: 'Warszawa',
			parts: [
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
			whole: 'Gdańsk',
			parts: [
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
			whole: 'Kraków',
			parts: [
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

	public regions: string[] = [
		'dolnośląskie',
		'kujawsko-pomorskie',
		'lubelskie',
		'lubuskie',
		'łódzkie',
		'małopolskie',
		'mazowieckie',
		'opolskie',
		'podkarpackie',
		'podlaskie',
		'pomorskie',
		'śląskie',
		'świętokrzyskie',
		'warmińsko-mazurskie',
		'wielkopolskie',
		'zachodniopomorskie',
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
	public yearOfBuilds: string[] = [
		'do 1950',
		'od 1950 do 1989',
		'od 1990 do 2010',
		'od 2010',
		'wszystkie',
	];
	public properties: string[] = ['Kawalerka', 'Mieszkanie', 'Pokój'];
}
