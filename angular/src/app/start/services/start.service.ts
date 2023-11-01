import { Injectable } from '@angular/core';
import { IFlatOffer, ISortOption } from '../models/start-site.models';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class StartService {
	public allFlatOffers: IFlatOffer[] = [
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Bemowo',
			price: 2500,
			rent: 500,
			area: 35,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wola',
			price: 2000,
			rent: 700,
			area: 30,
			rooms: 2,
			url: '#',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Mokotów',
			price: 3000,
			rent: 900,
			area: 40,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Białołęka',
			price: 1200,
			rent: 300,
			area: 25,
			rooms: 1,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Pokój',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wawer',
			price: 1700,
			rent: 900,
			area: 30,
			rooms: 2,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Śródmieście',
			price: 2000,
			rent: 1000,
			area: 37,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Bemowo',
			price: 2500,
			rent: 500,
			area: 35,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wola',
			price: 2000,
			rent: 700,
			area: 30,
			rooms: 2,
			url: '#',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Mokotów',
			price: 3000,
			rent: 900,
			area: 40,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Białołęka',
			price: 1200,
			rent: 300,
			area: 25,
			rooms: 1,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Pokój',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wawer',
			price: 1700,
			rent: 900,
			area: 30,
			rooms: 2,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Śródmieście',
			price: 2000,
			rent: 1000,
			area: 37,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
	];
	public sortByOptions: ISortOption[] = [
		{ type: 'price', direction: 'asc', description: 'ceny: od najniższej' },
		{ type: 'price', direction: 'desc', description: 'ceny: od najwyższej' },
		{
			type: 'rooms',
			direction: 'asc',
			description: 'liczby pokoi: od najniższej',
		},
		{
			type: 'rooms',
			direction: 'desc',
			description: 'liczby pokoi: od najwyższej',
		},
		{ type: 'area', direction: 'asc', description: 'powierzchni: od najniższej' },
		{
			type: 'area',
			direction: 'desc',
			description: 'powierzchni: od najwyższej',
		},
	];

	constructor(private httpClient: HttpClient) {}

	public getOffers(): Observable<IFlatOffer[]> {
		return this.httpClient.get<IFlatOffer[]>('./assets/offers.json');
	}
}
