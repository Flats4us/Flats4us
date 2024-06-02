import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import {
	IDecision,
	IOffer,
	IPromotion,
	IRentProposition,
	IResult,
	ISendOffers,
} from '../models/offer.models';

@Injectable()
export class OfferService {
	protected apiRoute = `${environment.apiUrl}`;

	public offerStatuses = new Map<number, string>([
		[0, 'Offer.offer-status0'],
		[1, 'Offer.offer-status1'],
		[2, 'Offer.offer-status2'],
		[3, 'Offer.offer-status3'],
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
			rentProposition
		);
	}
	public addOfferPromotion(id: number, duration: IPromotion) {
		return this.httpClient.post(
			`${this.apiRoute}/offers/${id}/promotion`,
			duration
		);
	}
	public addRentApproval(id: number, decision: IDecision) {
		return this.httpClient.put(
			`${this.apiRoute}/offers/${id}/rent/accept`,
			decision
		);
	}

	public getWatchedOffers(
		pageIndex: number,
		pageSize: number
	): Observable<ISendOffers> {
		return this.httpClient.get<ISendOffers>(
			`${this.apiRoute}/offers/interest?PageNumber=${
				pageIndex + 1
			}&PageSize=${pageSize}`
		);
	}

	public deleteInterest(id: number): Observable<IResult> {
		return this.httpClient.delete<IResult>(
			`${this.apiRoute}/offers/${id}/interest`
		);
	}

	public cancelOffer(id: number): Observable<IResult> {
		return this.httpClient.put<IResult>(
			`${this.apiRoute}/offers/${id}/cancel`,
			null
		);
	}
}
