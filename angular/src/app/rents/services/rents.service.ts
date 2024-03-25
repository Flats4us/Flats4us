import { Injectable } from '@angular/core';
import { IMenuOptions, IPayment, IRent } from '../models/rents.models';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable()
export class RentsService {
	public payments: IPayment[] = [
		{ sum: 1000, date: '9-12-2022', kind: 'KAUCJA' },
		{ sum: 4000, date: '10-12-2022', kind: 'CZYNSZ' },
	];
	public displayedColumns: string[] = ['sum', 'date', 'kind'];
	public menuOptions: IMenuOptions[] = [
		{ option: 'startDispute', description: 'Rozpocznij spór' },
		{ option: 'closeRent', description: 'Zakończ najem' },
	];

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
