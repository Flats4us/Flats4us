import { IRentOpinion } from 'src/app/rents/models/rents.models';
import { IEquipment } from 'src/app/start/models/start-site.models';

export interface IGroup {
	whole: string;
	parts: string[];
}

export interface INumeric {
	value: number;
	viewValue: string;
}

export interface IImage {
	name: string;
	path: string;
}

export interface IRegionCity {
	region: string;
	city: string;
}

export interface IAddResult {
	result: number;
}

export interface IGeoLocation {
	lat: number;
	lon: number;
}

export interface IAddProperty {
	propertyType: number;
	province: string;
	district: string;
	street: string;
	number: string;
	flat: number;
	city: string;
	postalCode: string;
	area: number;
	maxNumberOfInhabitants: number;
	constructionYear: number;
	numberOfRooms: number;
	numberOfFloors: number;
	plotArea: number;
	floor: number;
	equipmentIds: number[];
}

export interface IOfferMiniatures {
	offerId: number;
	startDate: Date;
	offerStatus: number;
}

export interface IProperty {
	propertyId: number;
	propertyType: number;
	province: string;
	district: string;
	street: string;
	number: string;
	flat: number;
	city: string;
	postalCode: string;
	geoLat: number;
	geoLon: number;
	area: number;
	maxNumberOfInhabitants: number;
	constructionYear: number;
	images: IImage[];
	avgRating: number;
	avgServiceRating: number;
	avgLocationRating: number;
	avgEquipmentRating: number;
	avgQualityForMoneyRating: number;
	verificationStatus: number;
	numberOfRooms: number;
	numberOfFloors: number;
	plotArea: number;
	floor: number;
	offers: IOfferMiniatures[];
	equipment: IEquipment[];
	rentOpinions: IRentOpinion[];
}
