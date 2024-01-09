import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { IWatchedOffer } from '../models/offer-models';
import { environment } from 'src/environments/environment.prod';

@Injectable()
export class OfferService {
	protected apiRoute = `${environment.apiUrl}/offers`;

	constructor(private httpClient: HttpClient) {}

	public getWatchedOffers(
		pageIndex: number,
		pageSize: number
	): Observable<IWatchedOffer> {
		return this.httpClient
			.get<IWatchedOffer>(
				`${this.apiRoute}/interest?PageNumber=${pageIndex + 1}&PageSize=${pageSize}`
			)
			.pipe(
				map(response => {
					return {
						result: response.result,
						totalCount: response.totalCount,
					};
				})
			);
	}

	public deleteInterest(id: number): Observable<string> {
		return this.httpClient.delete<string>(`${this.apiRoute}/${id}/interest`);
	}
}
