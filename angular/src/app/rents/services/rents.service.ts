import { Injectable } from '@angular/core';
import {
	IMeeting,
	IRent,
	IRentOpinion,
	IRentProposition,
	ISendRent,
} from '../models/rents.models';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable()
export class RentsService {
	protected apiRoute = `${environment.apiUrl}`;

	constructor(private httpClient: HttpClient) {}

	public paymentPurposes = new Map<number, string>([
		[0, 'czynsz'],
		[1, 'depozyt'],
		[2, 'naprawy'],
	]);

	public addMeeting(meeting: IMeeting) {
		meeting = {
			...meeting,
			date: new Date(
				Date.UTC(
					meeting.date.getFullYear(),
					meeting.date.getMonth(),
					meeting.date.getDate()
				)
			),
		};
		return this.httpClient.post(`${this.apiRoute}/meetings`, meeting);
	}

	public getRents(index?: number, size?: number): Observable<ISendRent> {
		let queryParams = new HttpParams();
		if (index) {
			queryParams = new HttpParams().append('pageNumber', index + 1);
		}
		if (size) {
			queryParams = new HttpParams().append('pageSize', size);
		}
		return this.httpClient.get<ISendRent>(`${this.apiRoute}/rent`, {
			params: queryParams,
		});
	}

	public getRentById(id: number): Observable<IRent> {
		return this.httpClient.get<IRent>(`${this.apiRoute}/rent/${id}`);
	}

	public getRentProposition(id: number) {
		return this.httpClient.get<IRentProposition>(
			`${this.apiRoute}/rent/${id}/proposition`
		);
	}

	public postOpinion(rentId: number, opinion: IRentOpinion) {
		return this.httpClient.post(
			`${this.apiRoute}/offers/${rentId}/rent/opinion`,
			opinion
		);
	}
}
