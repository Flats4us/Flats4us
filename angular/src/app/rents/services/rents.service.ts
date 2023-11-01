import { Injectable } from '@angular/core';
import { IMenuOptions, IPayment, IRent, Status } from '../models/rents.models';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class RentsService {
	public rents: IRent[] = [
		{
			link: '1',
			title: 'Mieszkanie1, Wilanów',
			publishDate: '8-12-2022',
			status: Status.valid,
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
		},
		{
			link: '2',
			title: 'Mieszkanie2, Mokotów',
			publishDate: '9-12-2022',
			status: Status.valid,
			price: 3000,
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
				{ sum: 1000, date: '9-12-2022', kind: 'KAUCJA' },
				{ sum: 3000, date: '10-12-2022', kind: 'CZYNSZ' },
			],
		},
		{
			link: '3',
			title: 'Mieszkanie3, Ursynów',
			publishDate: '11-12-2022',
			status: Status.valid,
			price: 5000,
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
				{ sum: 2000, date: '11-12-2022', kind: 'KAUCJA' },
				{ sum: 5000, date: '12-12-2022', kind: 'CZYNSZ' },
			],
		},
	];

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
}
