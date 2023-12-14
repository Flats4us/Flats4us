import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { ISendOffers, IWatchedOffer } from '../models/offer-models';

@Injectable()
// export class OfferService {
// 	private apiUrl = 'adres_twojego_backendu/api/offers';

// 	constructor(private httpClient: HttpClient) {}

// 	public getOffers(): Observable<ISendOffers[]> {
// 	  return this.httpClient.get<IWatchedOffer[]>(`${this.apiUrl}`).pipe();
// 	}

// 	public deleteOffer(offerId: string): Observable<void> {
// 	  return this.httpClient.delete<void>(`${this.apiUrl}/${offerId}`).pipe();
// 	}

// 	public getStudentCards(): Observable<IUser[]> {
//     	const headers = new HttpHeaders().set('Authorization', 'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiem1vZGVyYXRvckBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNb2RlcmF0b3IiLCJWZXJpZmljYXRpb25TdGF0dXMiOiJWZXJpZmllZCIsImV4cCI6MTcwMTg2ODAyMH0.PXkcOeHkuf_ciLxKLzd__N5MnuwLJir5okxPDNgsx7g2BVWHTHK8CheFiJBfBYzCpq2WuNt9ldYgVxJOUygCXQ');
//     		return this.http.get<IUser[]>('http://localhost:5166/api/Moderator/User', { headers });
//     }

//   }
export class OfferService {
	constructor(private httpClient: HttpClient) {}

	public getOffers(size1: number, size2: number): Observable<ISendOffers> {
		const stream = this.httpClient
			.get<IWatchedOffer[]>('./assets/watched-offers.json')
			.pipe(map(el => ({ data: el.slice(size1, size2), total: el.length })));
		return stream;
	}
}
