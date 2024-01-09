import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IOffer } from '../models/offer.models';
import { environment } from 'src/environments/environment.prod';

@Injectable()
export class OfferService {
	protected apiRoute = `${environment.apiUrl}`;

	constructor(private httpClient: HttpClient) {}

	public getOffers(): Observable<IOffer[]> {
		return this.httpClient.get<IOffer[]>(`${this.apiRoute}/offers/mine`);
	}
}
