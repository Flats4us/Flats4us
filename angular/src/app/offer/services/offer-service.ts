import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ISendOffers, IWatchedOffer } from '../models/offer-models';

@Injectable()
export class OfferService {
	constructor(private httpClient: HttpClient) {}

	// public getOffers(): Observable<IWatchedOffer[]> {
	// 	return this.httpClient.get<IWatchedOffer[]>('./assets/watched-offers.json');
	// }

	public getOffers(size1: number, size2: number): Observable<ISendOffers> {
		const stream = this.httpClient
			.get<IWatchedOffer[]>('./assets/watched-offers.json')
			.pipe(
				map(el => {
					return { data: el.slice(size1, size2), total: el.length };
				})
			);
		return stream;
	}
}
