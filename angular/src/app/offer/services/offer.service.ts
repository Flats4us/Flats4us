import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { IOffer, IPromotion, ISendOffers } from '../models/offer.models';

@Injectable()
export class OfferService {
	protected apiRoute = `${environment.apiUrl}`;

	public offerStatuses = new Map<number, string>([
		[0, 'aktualna'],
		[1, 'nieaktualna'],
		[2, 'zawieszona'],
		[3, 'wynajęta'],
	]);

	constructor(private httpClient: HttpClient) {}

	public getOfferById(id: number): Observable<IOffer> {
		return this.httpClient.get<IOffer>(`${this.apiRoute}/offers/${id}`);
	}

	public getOffers(): Observable<ISendOffers> {
		return this.httpClient.get<ISendOffers>(`${this.apiRoute}/offers/mine`);
	}
	public addOffer(offer: IOffer) {
		return this.httpClient.post(`${this.apiRoute}/offers`, offer);
	}
	public addOfferPromotion(id: number, duration: IPromotion) {
		return this.httpClient.post(
			`${this.apiRoute}/offers/${id}/promotion`,
			duration
		);
	}
}
