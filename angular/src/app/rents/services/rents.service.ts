import { Injectable } from '@angular/core';
import { IRent } from '../models/rents.models';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable()
export class RentsService {
	constructor(private httpClient: HttpClient) {}

	public getRents(): Observable<IRent[]> {
		return this.httpClient.get<IRent[]>('./assets/rents.json');
	}
	public getRent(id: string): Observable<IRent> {
		return this.httpClient
			.get<IRent[]>('./assets/rents.json')
			.pipe(map(results => results.find(result => result.id === id) as IRent));
	}
}
