export interface ICitiesGroup {
	region: string;
	cities: string[];
}

export interface IRegion {
	value: string;
}

export interface IArea {
	value: number;
	viewValue: string;
}

export interface IRoom {
	value: number;
	viewValue: string;
}

export interface IPrice {
	value: number;
	viewValue: string;
}

export interface IDistance {
	value: number;
	viewValue: string;
}

export interface IFloor {
	value: number;
	viewValue: string;
}

export interface IYear {
	value: string;
}

export interface IProperty {
	value: string;
}

export class RegionCity {
	private region: string;
	private city: string;

	constructor(region: string, city: string) {
		this.region = region;
		this.city = city;
	}
}