import { Injectable } from '@angular/core';
import {
	IFilteredMapOffers,
	IFilteredOffers,
	ISendMapOffers,
	ISortOption,
} from '../models/start-site.models';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { ISendOffers } from 'src/app/offer/models/offer.models';
import { FormControl, FormGroup } from '@angular/forms';

@Injectable()
export class StartService {
	public sortByOptions: ISortOption[] = [
		{ type: 'Price ASC', direction: 'asc', description: 'ceny: od najniższej' },
		{ type: 'Price DSC', direction: 'desc', description: 'ceny: od najwyższej' },
		{
			type: 'NumberOfRooms ASC',
			direction: 'asc',
			description: 'liczby pokoi: od najniższej',
		},
		{
			type: 'NumberOfRooms DSC',
			direction: 'desc',
			description: 'liczby pokoi: od najwyższej',
		},
		{
			type: 'Area ASC',
			direction: 'asc',
			description: 'powierzchni: od najniższej',
		},
		{
			type: 'Area DSC',
			direction: 'desc',
			description: 'powierzchni: od najwyższej',
		},
	];

	protected apiRoute = `${environment.apiUrl}/offers`;

	public mapOffersForm: FormGroup = new FormGroup({
		regionsGroup: new FormControl(''),
		citiesGroup: new FormControl(''),
		distance: new FormControl(0),
		property: new FormControl([]),
		minPrice: new FormControl(null),
		maxPrice: new FormControl(null),
		districtsGroup: new FormControl(''),
		minArea: new FormControl(null),
		maxArea: new FormControl(null),
		year: new FormControl([]),
		rooms: new FormControl(null),
		floors: new FormControl(null),
		equipment: new FormControl([]),
	});

	constructor(private httpClient: HttpClient) {}

	public getFilteredOffers(
		filteredOptions: IFilteredOffers,
		index: number,
		size: number
	): Observable<ISendOffers> {
		const properties = filteredOptions.property.join(', ').split(', ');
		const years = filteredOptions.year.join(', ').split(', ');
		const minVal = Math.min(...years.map(year => parseInt(year)));
		const maxVal = Math.max(...years.map(year => parseInt(year)));
		let minYear = 0;
		let maxYear = 0;
		switch (minVal) {
			case 1: {
				minYear = 0;
				break;
			}
			case 2: {
				minYear = 1950;
				break;
			}
			case 3: {
				minYear = 1990;
				break;
			}
			case 4: {
				minYear = 2010;
				break;
			}
		}
		switch (maxVal) {
			case 1: {
				maxYear = 1950;
				break;
			}
			case 2: {
				maxYear = 1990;
				break;
			}
			case 3: {
				maxYear = 2010;
				break;
			}
			case 4: {
				maxYear = new Date().getFullYear();
				break;
			}
		}
		let queryParams = new HttpParams({
			fromObject: { Equipment: filteredOptions.equipment },
		})
			.append('pageNumber', index + 1)
			.append('pageSize', size);
		if (filteredOptions.regionsGroup) {
			queryParams = queryParams.append(
				'province',
				filteredOptions.regionsGroup.charAt(0).toUpperCase() +
					filteredOptions.regionsGroup?.slice(1)
			);
		}
		if (filteredOptions.citiesGroup) {
			queryParams = queryParams.append('city', filteredOptions.citiesGroup);
		}
		if (filteredOptions.distance) {
			queryParams = queryParams.append('distance', filteredOptions.distance);
		}
		for (let i = 0; i < 3; i++) {
			if (properties[i]) {
				queryParams = queryParams.append('propertyTypes', properties[i]);
			}
		}
		if (filteredOptions.minPrice) {
			queryParams = queryParams.append('minPrice', filteredOptions.minPrice);
		}
		if (filteredOptions.maxPrice) {
			queryParams = queryParams.append('maxPrice', filteredOptions.maxPrice);
		}
		if (filteredOptions.districtsGroup) {
			queryParams = queryParams.append('district', filteredOptions.districtsGroup);
		}
		if (filteredOptions.minArea) {
			queryParams = queryParams.append('minArea', filteredOptions.minArea);
		}
		if (filteredOptions.maxArea) {
			queryParams = queryParams.append('maxArea', filteredOptions.maxArea);
		}
		if (minYear) {
			queryParams = queryParams.append('minYear', minYear);
		}
		if (maxYear) {
			queryParams = queryParams.append('maxYear', maxYear);
		}
		if (filteredOptions.rooms) {
			queryParams = queryParams.append('minNumberOfRooms', filteredOptions.rooms);
		}
		if (filteredOptions.floors) {
			queryParams = queryParams.append('floor', filteredOptions.floors);
		}
		if (filteredOptions.sorting.type) {
			queryParams = queryParams.append('sorting', filteredOptions.sorting.type);
		}
		return this.httpClient.get<ISendOffers>(this.apiRoute, {
			params: queryParams,
		});
	}

	public getFilteredMapOffers(
		filteredOptions: IFilteredMapOffers
	): Observable<ISendMapOffers> {
		const properties = filteredOptions.property.join(', ').split(', ');
		const years = filteredOptions.year.join(', ').split(', ');
		const minVal = Math.min(...years.map(year => parseInt(year)));
		const maxVal = Math.max(...years.map(year => parseInt(year)));
		let minYear = 0;
		let maxYear = 0;
		switch (minVal) {
			case 1: {
				minYear = 0;
				break;
			}
			case 2: {
				minYear = 1950;
				break;
			}
			case 3: {
				minYear = 1990;
				break;
			}
			case 4: {
				minYear = 2010;
				break;
			}
		}
		switch (maxVal) {
			case 1: {
				maxYear = 1950;
				break;
			}
			case 2: {
				maxYear = 1990;
				break;
			}
			case 3: {
				maxYear = 2010;
				break;
			}
			case 4: {
				maxYear = new Date().getFullYear();
				break;
			}
		}
		let queryParams = new HttpParams({
			fromObject: { Equipment: filteredOptions.equipment },
		});
		if (filteredOptions.regionsGroup) {
			queryParams = queryParams.append(
				'province',
				filteredOptions.regionsGroup.charAt(0).toUpperCase() +
					filteredOptions.regionsGroup?.slice(1)
			);
		}
		if (filteredOptions.citiesGroup) {
			queryParams = queryParams.append('city', filteredOptions.citiesGroup);
		}
		if (filteredOptions.distance) {
			queryParams = queryParams.append('distance', filteredOptions.distance);
		}
		for (let i = 0; i < 3; i++) {
			if (properties[i]) {
				queryParams = queryParams.append('propertyTypes', properties[i]);
			}
		}
		if (filteredOptions.minPrice) {
			queryParams = queryParams.append('minPrice', filteredOptions.minPrice);
		}
		if (filteredOptions.maxPrice) {
			queryParams = queryParams.append('maxPrice', filteredOptions.maxPrice);
		}
		if (filteredOptions.districtsGroup) {
			queryParams = queryParams.append('district', filteredOptions.districtsGroup);
		}
		if (filteredOptions.minArea) {
			queryParams = queryParams.append('minArea', filteredOptions.minArea);
		}
		if (filteredOptions.maxArea) {
			queryParams = queryParams.append('maxArea', filteredOptions.maxArea);
		}
		if (minYear) {
			queryParams = queryParams.append('minYear', minYear);
		}
		if (maxYear) {
			queryParams = queryParams.append('maxYear', maxYear);
		}
		if (filteredOptions.rooms) {
			queryParams = queryParams.append('minNumberOfRooms', filteredOptions.rooms);
		}
		if (filteredOptions.floors) {
			queryParams = queryParams.append('floor', filteredOptions.floors);
		}
		const url = `${this.apiRoute}/map`;
		return this.httpClient.get<ISendMapOffers>(url, {
			params: queryParams,
		});
	}

	public addToWatched(id: number) {
		const url = `${this.apiRoute}/${id}/interest`;
		return this.httpClient.post(url, { responseType: 'JSON' });
	}
}
