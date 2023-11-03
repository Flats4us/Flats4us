import { Injectable } from '@angular/core';
import { IMenuOptions, IPayment, IRent } from '../models/rents.models';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';

@Injectable()
export class RentsService {
	public initialRent: IRent = {
		link: '1',
		title: 'Mieszkanie1, Wilanów',
		publishDate: '8-12-2022',
		status: 'valid',
		price: 4000,
		description: '',
		period: 2,
		biddersNumber: 2,
		viewsNumber: 100,
		rules: '',
		imageArray: [
			{
				image: './assets/mieszkanie.jpg',
				thumbImage: './assets/mieszkanie.jpg',
				alt: '',
				title: '',
			},
			{
				image: './assets/mieszkanie.jpg',
				thumbImage: './assets/mieszkanie.jpg',
				alt: '',
				title: '',
			},
			{
				image: './assets/mieszkanie.jpg',
				thumbImage: './assets/mieszkanie.jpg',
				alt: '',
				title: '',
			},
			{
				image: './assets/mieszkanie.jpg',
				thumbImage: './assets/mieszkanie.jpg',
				alt: '',
				title: '',
			},
		],
		payments: [
			{ sum: 1000, date: '8-12-2022', kind: 'KAUCJA' },
			{ sum: 4000, date: '9-12-2022', kind: 'CZYNSZ' },
		],
		property: {
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wilanów',
			price: 4000,
			rent: 500,
			area: 40,
			rooms: 3,
			url: '/',
			imgSource: '/',
			type: 'Mieszkanie',
		},
	};

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
	public getRent(id: string): Observable<IRent | null | undefined> {
		return this.httpClient
			.get<IRent[]>('./assets/rents.json')
			.pipe(map(results => results.find(result => result.link === id)));
	}
}
