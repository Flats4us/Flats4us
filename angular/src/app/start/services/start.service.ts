import { Injectable } from '@angular/core';
import { IFilteredOffers, ISortOption } from '../models/start-site.models';
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

	public pageSize = 6;
	public pageIndex = 0;

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
		sorting: new FormControl(this.sortByOptions[0]),
		pageIndex: new FormControl(this.pageIndex),
		pageSize: new FormControl(this.pageSize),
	});

	constructor(private httpClient: HttpClient) {}

	public getFilteredOffers(
		filteredOptions: IFilteredOffers
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
		const queryParams = new HttpParams({
			fromObject: { Equipment: filteredOptions.equipment },
		})
			.append(
				'province',
				filteredOptions.regionsGroup.charAt(0).toUpperCase() +
					filteredOptions.regionsGroup.slice(1)
			)
			.append('city', filteredOptions.citiesGroup)
			.append(
				filteredOptions.distance ? 'distance' : ``,
				filteredOptions.distance ?? ''
			)
			.append(properties[0] ? `propertyTypes` : ``, properties[0] ?? '')
			.append(properties[1] ? `propertyTypes` : ``, properties[1] ?? '')
			.append(properties[2] ? `propertyTypes` : ``, properties[2] ?? '')
			.append(
				filteredOptions.minPrice ? 'minPrice' : ``,
				filteredOptions.minPrice ?? ''
			)
			.append(
				filteredOptions.maxPrice ? 'maxPrice' : ``,
				filteredOptions.maxPrice ?? ''
			)
			.append(
				filteredOptions.districtsGroup ? 'district' : ``,
				filteredOptions.districtsGroup ?? ''
			)
			.append(
				filteredOptions.minArea ? 'minArea' : ``,
				filteredOptions.minArea ?? ''
			)
			.append(
				filteredOptions.maxArea ? 'maxArea' : ``,
				filteredOptions.maxArea ?? ''
			)
			.append(minYear ? 'minYear' : ``, minYear ?? '')
			.append(maxYear ? 'maxYear' : ``, maxYear ?? '')
			.append(
				filteredOptions.rooms ? 'minNumberOfRooms' : ``,
				filteredOptions.rooms ?? ''
			)
			.append(filteredOptions.floors ? 'floor' : ``, filteredOptions.floors ?? '')
			.append(
				filteredOptions.sorting.type ? 'sorting' : ``,
				filteredOptions.sorting.type ?? ''
			)
			.append('pageNumber', filteredOptions.pageIndex + 1)
			.append('pageSize', filteredOptions.pageSize);
		return this.httpClient.get<ISendOffers>(this.apiRoute, {
			params: queryParams,
		});
	}

	public addToWatched(id: number) {
		const url = `${this.apiRoute}/${id}/interest`;
		return this.httpClient.post(url, { responseType: 'JSON' });
	}
}
