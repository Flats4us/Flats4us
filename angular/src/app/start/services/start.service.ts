import { Injectable } from '@angular/core';
import { ISendOffers, ISortOption } from '../models/start-site.models';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

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

	constructor(private httpClient: HttpClient) {}

	public getFilteredOffers(
		region: string,
		city: string,
		distance: number,
		property: string[],
		minPrice: number,
		maxPrice: number,
		district: string,
		minArea: number,
		maxArea: number,
		yearRange: string[],
		rooms: number,
		floors: number,
		equipment: number[],
		sorting: ISortOption,
		pageIndex: number,
		pageSize: number
	): Observable<ISendOffers> {
		const properties = property.join(', ').split(', ');
		const years = yearRange.join(', ').split(', ');
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
		const queryParams = new HttpParams({ fromObject: { Equipment: equipment } })
			.append('Province', region.charAt(0).toUpperCase() + region.slice(1))
			.append('City', city)
			.append(distance ? 'Distance' : ``, distance ?? '')
			.append(properties[0] ? `PropertyTypes` : ``, properties[0] ?? '')
			.append(properties[1] ? `PropertyTypes` : ``, properties[1] ?? '')
			.append(properties[2] ? `PropertyTypes` : ``, properties[2] ?? '')
			.append(minPrice ? 'MinPrice' : ``, minPrice ?? '')
			.append(maxPrice ? 'MaxPrice' : ``, maxPrice ?? '')
			.append(district ? 'District' : ``, district ?? '')
			.append(minArea ? 'MinArea' : ``, minPrice ?? '')
			.append(maxArea ? 'MaxArea' : ``, maxPrice ?? '')
			.append(minYear ? 'MinYear' : ``, minYear ?? '')
			.append(maxYear ? 'MaxYear' : ``, maxYear ?? '')
			.append(rooms ? 'MinNumberOfRooms' : ``, rooms ?? '')
			.append(floors ? 'Floor' : ``, floors ?? '')
			.append(sorting.type ? 'Sorting' : ``, sorting.type ?? '')
			.append('PageNumber', pageIndex + 1)
			.append('PageSize', pageSize);
		return this.httpClient.get<ISendOffers>(this.apiRoute, {
			params: queryParams,
		});
	}

	public addToWatched(id: number): Observable<string> {
		const url = `${this.apiRoute}/${id}/interest`;
		return this.httpClient.post<string>(url, { responseType: 'text' });
	}
}
