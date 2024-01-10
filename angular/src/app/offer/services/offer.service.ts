import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { ISendOffers } from '../models/offer.models';

@Injectable()
export class OfferService {
	protected apiRoute = `${environment.apiUrl}`;

	constructor(private httpClient: HttpClient) {}

	public getOffers(): Observable<ISendOffers> {
		return this.httpClient.get<ISendOffers>(`${this.apiRoute}/offers/mine`);
	}
}
