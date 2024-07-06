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
			whole: 'Białystok',
			parts: [],
		},
		{
			whole: 'Bydgoszcz',
			parts: [],
		},
		{
			whole: 'Chełm',
			parts: [],
		},
		{
			whole: 'Częstochowa',
			parts: [],
		},
		{
			whole: 'Gdańsk',
			parts: [],
		},
		{
			whole: 'Gorzów Wielkopolski',
			parts: [],
		},
		{
			whole: 'Katowice',
			parts: [],
		},
		{
			whole: 'Kielce',
			parts: [],
		},
		{
			whole: 'Kraków',
			parts: [],
		},
		{
			whole: 'Lublin',
			parts: [],
		},

		{
			whole: 'Łódź',
			parts: [],
		},
		{
			whole: 'Olsztyn',
			parts: [],
		},
		{
			whole: 'Opole',
			parts: [],
		},
		{
			whole: 'Poznań',
			parts: [],
		},
		{
			whole: 'Rzeszów',
			parts: [],
		},
		{
			whole: 'Szczecin',
			parts: [],
		},
		{
			whole: 'Toruń',
			parts: [],
		},
		{
			whole: 'Warszawa',
			parts: [],
		},
		{
			whole: 'Wrocław',
			parts: [],
		},
		{
			whole: 'Zielona Góra',
			parts: [],
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
					for (let index = 1; index < csvToRowArray.length; index++) {
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

	public readDistrictsForCities(): Observable<IGroup[]> {
		return this.httpClient
			.get('./assets/miasta_dzielnice.csv', { responseType: 'text' })
			.pipe(
				map(data => {
					const csvToRowArray = data.split('\n');
					for (let index = 1; index < csvToRowArray.length; index++) {
						const row = csvToRowArray[index].split(';');
						const city = row[0].trim().toLowerCase();
						for (let j = 1; j < row.length; j++) {
							this.districtGroups
								.find(group => group.whole.trim().toLowerCase() === city)
								?.parts.push(row[j]);
						}
					}
					return this.districtGroups;
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

	public getLocation(address: string): Observable<IGeoLocation[]> {
		const url = `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(
			address
		)}&polygon_geojson=1&format=jsonv2`;
		return this.httpClient.get<IGeoLocation[]>(url);
	}
	public getLocationStructured(
		street: string,
		postalCode: string,
		city: string
	): Observable<IGeoLocation[]> {
		const url = `https://nominatim.openstreetmap.org/search.php?street=${street}&city=${city}&country=Polska&postalcode=${postalCode}&polygon_geojson=1&format=jsonv2`;
		return this.httpClient.get<IGeoLocation[]>(url);
	}
}
