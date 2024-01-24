import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.prod';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { IProperty } from 'src/app/real-estate/models/real-estate.models';
import { IResult, ISendOffers } from '../models/offer.models';

@Injectable()
export class OfferService {
	protected apiRoute = `${environment.apiUrl}`;

	constructor(private http: HttpClient) {}

	public getProperties(): Observable<IProperty[]> {
		return this.http.get<IProperty[]>(
			`${this.apiRoute}/properties?showOnlyVerified=true`
		);
	}
	public addOffer(offer: FormGroup) {
		return this.http.post(`${this.apiRoute}/offers`, offer.value);
	}

	public getWatchedOffers(
		pageIndex: number,
		pageSize: number
	): Observable<ISendOffers> {
		return this.http.get<ISendOffers>(
			`${this.apiRoute}/offers/interest?PageNumber=${
				pageIndex + 1
			}&PageSize=${pageSize}`
		);
	}

	public deleteInterest(id: number): Observable<IResult> {
		return this.http.delete<IResult>(`${this.apiRoute}/offers/${id}/interest`);
	}
}
