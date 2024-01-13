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
	distance: number;
	property: number[];
	minPrice: number;
	maxPrice: number;
	districtsGroup: string;
	minArea: number;
	maxArea: number;
	year: number[];
	rooms: number;
	floors: number;
	equipment: number[];
	sorting: ISortOption;
	pageIndex: number;
	pageSize: number;
}
