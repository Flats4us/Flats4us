import {
	IProperty,
	IRegionCity,
} from 'src/app/real-estate/models/real-estate.models';

export interface IOffer {
	offerId: number;
	date: Date;
	offerStatus: number;
	price: number;
	deposit: number;
	description: string;
	startDate: Date;
	endDate: Date;
	numberOfInterested: number;
	regulations: string;
	isPromoted: boolean;
	property: IProperty;
	owner: IOwner;
	surveyOwnerOffer: ISurveyOwnerOffer;
}

export interface ISendOffers {
	totalCount: number;
	result: IOffer[];
}

export interface IOwner {
	userId: number;
	name: string;
	surname: string;
	email: string;
	phoneNumber: string;
	imagesPath: string;
	activityStatus: boolean;
}

export interface ISurveyOwnerOffer {
	smokingAllowed: boolean;
	partiesAllowed: boolean;
	animalsAllowed: boolean;
	gender: number;
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
	type: string;
}
