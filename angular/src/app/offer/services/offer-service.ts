import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IWatchedOffer } from '../models/offer-models';

@Injectable()
export class OfferService {
	constructor(private httpClient: HttpClient) {}

	public getOffers(): Observable<IWatchedOffer[]> {
		return this.httpClient.get<IWatchedOffer[]>('./assets/watched-offers.json');
	}
}
