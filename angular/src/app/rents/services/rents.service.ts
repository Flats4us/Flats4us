import { Injectable } from '@angular/core';
import { IPayment, IRent, Status } from '../models/rents.models';

@Injectable()
export class RentsService {
	public rents: IRent[] = [
		{
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
				{ sum: 1000, date: '9-12-2022', kind: 'KAUCJA' },
				{ sum: 4000, date: '10-12-2022', kind: 'CZYNSZ' },
			],
		},
		{
			title: 'Mieszkanie2, Mokotów',
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
				{ sum: 1000, date: '9-12-2022', kind: 'KAUCJA' },
				{ sum: 4000, date: '10-12-2022', kind: 'CZYNSZ' },
			],
		},
		{
			title: 'Mieszkanie3, Ursynów',
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
				{ sum: 1000, date: '9-12-2022', kind: 'KAUCJA' },
				{ sum: 4000, date: '10-12-2022', kind: 'CZYNSZ' },
			],
		},
	];

	public payments: IPayment[] = [
		{ sum: 1000, date: '9-12-2022', kind: 'KAUCJA' },
		{ sum: 4000, date: '10-12-2022', kind: 'CZYNSZ' },
	];
	public displayedColumns: string[] = ['sum', 'date', 'kind'];
}
