import { Injectable } from '@angular/core';
import { IPayment } from '../models/rents.models';

@Injectable()
export class RentsService {
	public imageArray: object[] = [
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
	];
	public rents: string[] = [
		'Mieszkanie1, Wilanów',
		'Mieszkanie2, Mokotów',
		'Mieszkanie3, Ursynów',
	];

	public payments: IPayment[] = [
		{ sum: 1000, date: '9-12-2022', kind: 'KAUCJA' },
		{ sum: 4000, date: '10-12-2022', kind: 'CZYNSZ' },
	];
	public displayedColumns: string[] = ['sum', 'date', 'kind'];
}
