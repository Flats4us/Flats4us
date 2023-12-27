import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.prod';
import { IProperty } from '../models/offer.models';
import { map, Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';

@Injectable()
export class OfferService {
	protected apiRoute = `${environment.apiUrl}`;
	constructor(private http: HttpClient) {}

	public properties$: Observable<IProperty[]> = this.http
		.get<IProperty[]>(`${this.apiRoute}/properties`)
		.pipe(
			map(properties =>
				properties.filter(property => property.verificationStatus == 0)
			)
		);

	public addOffer(offer: FormGroup) {
		const headers = {
			'Content-Type': 'application/json',
		};
		return this.http.post(
			`${this.apiRoute}/offers`,
			JSON.stringify(offer.value),
			{ headers }
		);
	}
}
