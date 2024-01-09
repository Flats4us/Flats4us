import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.prod';
import { IProperty } from '../models/offer.models';
import { Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';

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
}
