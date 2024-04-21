import { IFlatOffer } from 'src/app/offer/models/offer.models';
import { statusName } from '../statusName';

export interface IGallery {
	image: string;
	thumbImage: string;
	alt: string;
	title: string;
}

export interface IPayment {
	sum: number;
	date: string;
	kind: string;
}

export interface IMeeting {
	date: Date;
	place: string;
	reason: string;
	offerId: number;
}

export interface IRent {
	id: string;
	title: string;
	publishDate: string;
	status: statusName;
	price: number;
	description: string;
	period: number;
	biddersNumber: number;
	viewsNumber: number;
	rules: string;
	imageArray: IGallery[];
	payments: IPayment[];
	property: IFlatOffer;
}

export interface IRentOpinion {
	rating: boolean;
	cleanliness: boolean;
	service: boolean;
	location: boolean;
	equipment: boolean;
	qualityForMoney: boolean;
	description: string;
}

export interface IMenuOptions {
	option: string;
	description: string;
}
