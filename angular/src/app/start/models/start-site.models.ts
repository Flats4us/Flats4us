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

export interface IEquipment {
	equipmentId: number;
	name: string;
}

export interface ISortOption {
	type: string;
	direction: string;
	description: string;
}

export interface IFilteredOffers {
	regionsGroup: string;
	citiesGroup: string;
	distance: number | null;
	property: number[];
	minPrice: number | null;
	maxPrice: number | null;
	districtsGroup: string;
	minArea: number | null;
	maxArea: number | null;
	year: number[];
	rooms: number | null;
	floors: number | null;
	equipment: number[];
	sorting: ISortOption;
	pageIndex: number;
	pageSize: number;
}
