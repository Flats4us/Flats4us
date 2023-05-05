export interface ICitiesGroup {
	region: string;
	cities: string[];
}

export interface IDistrictsGroup {
	city: string;
	districts: string[];
}

export interface IText {
	value: string;
}

export interface INumeric {
	value: number;
	viewValue: string;
}

export class RegionCity {
	private region: string;
	private city: string;

	constructor(region: string, city: string) {
		this.region = region;
		this.city = city;
	}
}
