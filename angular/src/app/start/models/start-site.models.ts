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
