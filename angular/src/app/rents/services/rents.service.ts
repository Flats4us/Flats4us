import { Injectable } from '@angular/core';
import { IMeeting, IRent, IRentOpinion, IRentProposition } from '../models/rents.models';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.prod';

@Injectable()
export class RentsService {
	protected apiRoute = `${environment.apiUrl}`;

	constructor(private httpClient: HttpClient) {}

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

	public getRents(): Observable<IRent[]> {
		return this.httpClient.get<IRent[]>('./assets/rents.json');
	}

	public getRent(id: string): Observable<IRent> {
		return this.httpClient
			.get<IRent[]>('./assets/rents.json')
			.pipe(map(results => results.find(result => result.id === id) as IRent));
	}

	public getRentProposition(id: number){
		return this.httpClient.get<IRentProposition>(`${this.apiRoute}/rent/${id}/proposition`);
	}

	public postOpinion(rentId: number, opinion: IRentOpinion) {
		return this.httpClient.post(
			`${this.apiRoute}/offers/${rentId}/rent/opinion`,
			opinion
		);
	}
}
