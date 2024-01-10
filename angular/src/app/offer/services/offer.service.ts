import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { IPromotion, ISendOffers } from '../models/offer.models';
import { FormGroup } from '@angular/forms';

@Injectable()
export class OfferService {
	protected apiRoute = `${environment.apiUrl}`;

	constructor(private httpClient: HttpClient) {}

	public getOffers(): Observable<ISendOffers> {
		return this.httpClient.get<ISendOffers>(`${this.apiRoute}/offers/mine`);
	}
	public addOffer(offer: FormGroup) {
		return this.httpClient.post(`${this.apiRoute}/offers`, offer.value);
	}
	public addOfferPromotion(id: number, durtion: IPromotion) {
		return this.httpClient.post(
			`${this.apiRoute}/offers/${id}/promotion`,
			durtion
		);
	}
}
