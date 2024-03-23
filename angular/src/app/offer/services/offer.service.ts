import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import {
	IOffer,
	IPromotion,
	IRentProposition,
	ISendOffers,
} from '../models/offer.models';

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
	public addRentProposition(rentProposition: IRentProposition, id: number) {
		return this.httpClient.post(
			`${this.apiRoute}/offers/${id}/rent`,
			rentProposition,
		);
	}
	public addOfferPromotion(id: number, duration: IPromotion) {
		return this.httpClient.post(
			`${this.apiRoute}/offers/${id}/promotion`,
			duration,
		);
	}

	public getWatchedOffers(
		pageIndex: number,
		pageSize: number,
	): Observable<ISendOffers> {
		return this.httpClient.get<ISendOffers>(
			`${this.apiRoute}/offers/interest?PageNumber=${
				pageIndex + 1
			}&PageSize=${pageSize}`,
		);
	}

	public deleteInterest(id: number): Observable<string> {
		return this.httpClient.delete<string>(
			`${this.apiRoute}/offers/${id}/interest`,
		);
	}
}
