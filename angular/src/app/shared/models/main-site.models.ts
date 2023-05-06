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

	constructor(region: string, city: string) {
		this.region = region;
		this.city = city;
	}
}
