import { Injectable } from '@angular/core';
import { IMeeting, IRent } from '../models/rents.models';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable()
export class RentsService {
	protected apiRoute = `${environment.apiUrl}`;

	constructor(private httpClient: HttpClient) {}

	public addMeeting(meeting: IMeeting): Observable<void> {
		return this.httpClient.post<void>(`${this.apiRoute}/meetings`, meeting);
	}

	public getRents(): Observable<IRent[]> {
		return this.httpClient.get<IRent[]>('./assets/rents.json');
	}
	public getRent(id: string): Observable<IRent> {
		return this.httpClient
			.get<IRent[]>('./assets/rents.json')
			.pipe(map(results => results.find(result => result.id === id) as IRent));
	}

	public getOfferStatus(index: number): string {
		switch (index) {
			case 0: {
				return 'aktualna';
				break;
			}
			case 1: {
				return 'nieaktualna';
				break;
			}
			case 2: {
				return 'zawieszona';
				break;
			}
			case 3: {
				return 'wynajęta';
				break;
			}
			default: {
				return '';
				break;
			}
		}
	}
}
