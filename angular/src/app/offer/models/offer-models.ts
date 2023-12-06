import { IRegionCity } from 'src/app/real-estate/models/real-estate.models';

export interface IWatchedOffer {
	id: string;
	regionCity: IRegionCity;
	district: string;
	price: number;
	rent: number;
	area: number;
	rooms: number;
	url: string;
	imgSource: string;
	type: string;
}

export interface ISendOffers {
	data: IWatchedOffer[];
	total: number;
}
