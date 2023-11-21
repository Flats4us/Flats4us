import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { IEvent } from '../models/calendar.models';

@Injectable({
	providedIn: 'root',
})
export class CalendarService {
	private readonly path: string = 'events';

	constructor(private httpClient: HttpClient) {}

	public getEvents(): Observable<IEvent[]> {
		return this.httpClient.get<IEvent[]>('assets/events.json');
	}

	public addEvent(event: IEvent): Observable<void> {
		return this.httpClient.post<void>(`${this.path}`, event);
	}
}
