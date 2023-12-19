import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ISendOffers, IWatchedOffer } from '../models/offer-models';

@Injectable()
export class OfferService {
	constructor(private httpClient: HttpClient) {}

	public getOffers(begin: number, end: number): Observable<ISendOffers> {
		const stream = this.httpClient
			.get<IWatchedOffer[]>('./assets/watched-offers.json')
			.pipe(map(el => ({ data: el.slice(begin, end), total: el.length })));
		return stream;
	}
}
