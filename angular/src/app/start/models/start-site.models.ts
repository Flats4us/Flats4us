export interface IEquipment {
	equipmentId: number;
	name: string;
}

export interface ISortOption {
	type: string;
	direction: string;
	description: string;
}

export interface IMapProperty {
	geoLat: number;
	geoLon: number;
	propertyType: number;
	equipment: IEquipment[];
}

export interface IMapOffer {
	offerId: number;
	isPromoted: boolean;
	property: IMapProperty;
}

export interface ISendMapOffers {
	totalCount: number;
	result: IMapOffer[];
}

export interface IFilteredOffers {
	regionsGroup: string | null;
	citiesGroup: string | null;
	distance: number | null;
	property: number[];
	minPrice: number | null;
	maxPrice: number | null;
	districtsGroup: string | null;
	minArea: number | null;
	maxArea: number | null;
	year: number[];
	rooms: number | null;
	floors: number | null;
	equipment: number[];
	sorting: ISortOption | null;
}

export interface IFilteredMapOffers {
	regionsGroup: string;
	citiesGroup: string | null;
	distance: number | null;
	property: number[];
	minPrice: number | null;
	maxPrice: number | null;
	districtsGroup: string | null;
	minArea: number | null;
	maxArea: number | null;
	year: number[];
	rooms: number | null;
	floors: number | null;
	equipment: number[];
}
