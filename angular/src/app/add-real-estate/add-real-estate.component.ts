import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { IGroup, INumeric, IRegionCity } from './models/add-real-estate.models';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Component({
	selector: 'app-add-real-estate',
	templateUrl: './add-real-estate.component.html',
	styleUrls: ['./add-real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddRealEstateComponent implements OnInit {
	public addRealEstateForm1: FormGroup = new FormGroup({});
	public addRealEstateForm2: FormGroup = new FormGroup({});

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;

	public regionCityArray: IRegionCity[] = [];

	public numberOfPhotos = 0;

	public completed = false;
	public fileName = '';
	public url: any;

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private http: HttpClient
	) {
		this.addRealEstateForm1 = formBuilder.group({
			regionsGroup: new FormControl('', Validators.required),
			citiesGroup: new FormControl('', Validators.required),
			districtsGroup: new FormControl(''),
			street: new FormControl('', Validators.required),
			streetNumber: new FormControl('', [Validators.required]),
			property: new FormControl('', Validators.required),
		});

		this.addRealEstateForm2 = formBuilder.group({
			area: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			maxResidents: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			equipment: new FormControl(''),
			rooms: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			floors: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			year: new FormControl('', Validators.required),
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
						.find((group) => group.whole == lowerCaseRegion)
						?.parts.push(row[1]);
				}
			});
	}

	public filter = (opt: string[], value: string): string[] => {
		const filterValue = value.toLowerCase();

		return opt.filter((item) => item.toLowerCase().includes(filterValue));
	};

	public onFileSelected(event: any) {
		const file: File = event.target.files[0];

		const reader = new FileReader();
		reader.readAsDataURL(file);

		reader.onload = (_event) => {
			this.url = reader.result;
		};

		if (file) {
			this.fileName = file.name;

			const formData = new FormData();

			formData.append('', file);

			const upload$ = this.http.post('', formData);

			upload$.subscribe();
		}
	}

	public showMap() {
		this.router.navigate(['start/map']);
	}

	public isDone() {
		if (this.addRealEstateForm1.valid && this.addRealEstateForm2.valid) {
			this.router.navigate(['/']);
		}
	}

	public ngOnInit() {
		this.citiesGroupOptions$ = this.addRealEstateForm1
			.get('citiesGroup')
			?.valueChanges.pipe(
				map((value) => value ?? ''),
				map((value) => this.filterCitiesGroup(value))
			);

		this.districtGroupOptions$ = this.addRealEstateForm1
			.get('districtsGroup')
			?.valueChanges.pipe(
				map((value) => value ?? ''),
				map((value) => this.filterDistrictsGroup(value))
			);

		this.addRealEstateForm1
			.get('citiesGroup')
			?.valueChanges.subscribe((value) => {
				if (this.districtGroups.find((distr) => distr.whole === value)) {
					this.addRealEstateForm1.get('districtsGroup')?.enable();
				} else {
					this.addRealEstateForm1.get('districtsGroup')?.reset();
				}
			});
		this.addRealEstateForm1.get('regionsGroup')?.valueChanges.subscribe(() => {
			this.addRealEstateForm1.get('citiesGroup')?.reset();
		});
	}

	private filterCitiesGroup(value: string): IGroup[] {
		return this.citiesGroups
			.map((group) => ({
				whole: group.whole,
				parts: this.filter(group.parts, value),
			}))
			.filter(
				(group) =>
					group.parts.length > 0 &&
					group.whole === this.addRealEstateForm1.get('regionsGroup')?.value
			);
	}
	private filterDistrictsGroup(value: string): IGroup[] {
		return this.districtGroups
			.map((group) => ({
				whole: group.whole,
				parts: this.filter(group.parts, value),
			}))
			.filter(
				(group) =>
					group.parts.length > 0 &&
					group.whole === this.addRealEstateForm1.get('citiesGroup')?.value
			);
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
	];
	public properties: string[] = ['Dom', 'Kawalerka', 'Mieszkanie', 'Pokój'];
	public equipment: string[] = ['Winda', 'Pralka', 'Zmywarka'];
}
