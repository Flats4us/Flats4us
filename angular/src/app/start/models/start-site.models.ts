export interface IGroup {
	whole: string;
	parts: string[];
}

export interface INumeric {
	value: number;
	viewValue: string;
}

export interface IRegionCity {
	region: string;
	city: string;
}

export interface IFlatOffer {
	regionCity: IRegionCity;
	district: string;
	price: number;
	rent: number;
	area: number;
	rooms: number;
	url: string;
	imgSource: string;
}
