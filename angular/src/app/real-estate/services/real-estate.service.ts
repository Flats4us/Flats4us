import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IResult } from '@shared/models/shared.models';
import { Observable, map } from 'rxjs';
import {
	IAddProperty,
	IAddResult,
	IGeoLocation,
	IGroup,
	INumeric,
	IProperty,
	IRegionCity,
} from 'src/app/real-estate/models/real-estate.models';
import { IEquipment } from 'src/app/start/models/start-site.models';
import { environment } from 'src/environments/environment';

@Injectable()
export class RealEstateService {
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
	public yearOfBuilds: INumeric[] = [
		{ value: 1, viewValue: 'Real-estate.year-type0' },
		{ value: 2, viewValue: 'Real-estate.year-type1' },
		{ value: 3, viewValue: 'Real-estate.year-type2' },
		{ value: 4, viewValue: 'Real-estate.year-type3' },
	];
	public properties: INumeric[] = [
		{ value: 0, viewValue: 'Real-estate.property-type0' },
		{ value: 1, viewValue: 'Real-estate.property-type1' },
		{ value: 2, viewValue: 'Real-estate.property-type2' },
	];
	public equipment: IEquipment[] = [];

	protected apiRoute = `${environment.apiUrl}`;

	public propertyTypes = new Map<number, string>([
		[0, 'Real-estate.property-type0'],
		[1, 'Real-estate.property-type1'],
		[2, 'Real-estate.property-type2'],
	]);

	public propertyStatuses = new Map<number, string>([
		[0, 'Real-estate.property-status0'],
		[1, 'Real-estate.property-status1'],
	]);

	constructor(private httpClient: HttpClient) {}

	public getPropertyType(id?: number): string {
		const result = this.propertyTypes.get(id ?? 0);
		return result ?? 'Real-estate.property-type0';
	}

	public getPropertyStatus(id?: number): string {
		const result = this.propertyStatuses.get(id ?? 0);
		return result ?? 'Real-estate.property-status0';
	}

	public readAllEquipment(): Observable<IEquipment[]> {
		return this.getEquipment('').pipe(
			map(equipments => {
				this.equipment = [];
				equipments.forEach(equipment => this.equipment.push(equipment));
				return this.equipment;
			})
		);
	}

	public readCitiesForRegions(
		regionCityArray: IRegionCity[],
		citiesGroups: IGroup[]
	): Observable<IRegionCity[]> {
		return this.httpClient
			.get('./assets/wojewodztwa_miasta.csv', { responseType: 'text' })
			.pipe(
				map(data => {
					const csvToRowArray = data.split('\n');
					for (let index = 1; index < csvToRowArray.length - 2; index++) {
						const row = csvToRowArray[index].split(';');
						const regionToLowerCase = row[2].trim().toLowerCase();
						regionCityArray = [];
						regionCityArray.push(<IRegionCity>{
							region: regionToLowerCase,
							city: row[1],
						});
						citiesGroups
							.find(group => group.whole == regionToLowerCase)
							?.parts.push(row[1]);
					}
					return regionCityArray;
				})
			);
	}

	public addRealEstateFiles(
		id: number,
		formData: FormData
	): Observable<IResult> {
		const headers = new HttpHeaders();
		headers.append('enctype', 'multipart/form-data');
		return this.httpClient.post<IResult>(
			`${this.apiRoute}/properties/${id}/files`,
			formData,
			{ headers }
		);
	}
	public deletePhoto(propertyId: number, fileId: string) {
		return this.httpClient.delete(
			`${this.apiRoute}/properties/${propertyId}/files/${fileId}`
		);
	}
	public deleteRealEstate(id: number) {
		return this.httpClient.delete(`${this.apiRoute}/properties/${id}`);
	}
	public addRealEstate(property: IAddProperty): Observable<number> {
		return this.httpClient
			.post<IAddResult>(`${this.apiRoute}/properties`, property)
			.pipe(map(result => result.result));
	}
	public getRealEstates(showOnlyVerified: boolean): Observable<IProperty[]> {
		let params = new HttpParams();
		params = params.append('showOnlyVerified', showOnlyVerified);
		return this.httpClient.get<IProperty[]>(`${this.apiRoute}/properties`, {
			params: params,
		});
	}
	public getRealEstateById(id: number): Observable<IProperty> {
		return this.httpClient.get<IProperty>(`${this.apiRoute}/properties/${id}`);
	}
	public editRealEstate(realEstate: IAddProperty, id: number) {
		return this.httpClient.put(`${this.apiRoute}/properties/${id}`, realEstate);
	}
	public getEquipment(name: string): Observable<IEquipment[]> {
		let params = new HttpParams();
		params = params.append('name', name);
		return this.httpClient.get<IEquipment[]>(`${this.apiRoute}/equipment`, {
			params: params,
		});
	}

	public getLatLon(address: string): IGeoLocation {
		let geoLocation: IGeoLocation = { lat: 0, lon: 0 };
		const url = `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(
			address
		)}&format=json`;
		this.httpClient.get(url).subscribe((response: any) => {
			if (response.length > 0) {
				geoLocation = response[0];
			}
		});
		return geoLocation;
	}
}
