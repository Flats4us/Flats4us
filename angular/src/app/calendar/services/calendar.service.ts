import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

import { IEvent } from '../models/calendar.models';

@Injectable({
	providedIn: 'root',
})
export class CalendarService {
	protected apiRoute = `${environment.apiUrl}/meetings`;

	constructor(private httpClient: HttpClient) {}

	public getEvents(): Observable<IEvent[]> {
		return this.httpClient.get<IEvent[]>(this.apiRoute);
	}

	public addEvent(event: IEvent): Observable<void> {
		return this.httpClient.post<void>(this.apiRoute, event);
	}
}
