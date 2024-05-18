import {
	IProperty,
	IRegionCity,
} from 'src/app/real-estate/models/real-estate.models';

export interface IOffer {
	offerId: number;
	rentPropositionToShow: number;
	isInterest: boolean;
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

export interface IPromotion {
	duration: number;
}

export interface IDecision {
	decision: boolean;
}

export interface IResult {
	result: string;
}

export interface IRentProposition {
	roommatesEmails: string[];
	startDate: Date;
	duration: number;
}

export interface IOwner {
	userId: number;
	name: string;
	surname: string;
	email: string;
	phoneNumber: string;
	profilePicture: IProfilePicture;
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

export interface IProfilePicture {
	name: string;
	path: string;
}
