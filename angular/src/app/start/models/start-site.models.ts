import { IRegionCity } from 'src/app/real-estate/models/real-estate.models';

export interface IFlatOffer {
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

export interface ISortOption {
	type: string;
	direction: string;
	description: string;
}
