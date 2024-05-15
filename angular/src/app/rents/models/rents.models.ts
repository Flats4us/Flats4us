import { IFlatOffer } from 'src/app/offer/models/offer.models';
import { statusName } from '../statusName';
import { IImage } from 'src/app/real-estate/models/real-estate.models';

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

export interface ISendRent{
	totalCount: number;
	result: IRentOffer[];
}

export interface IRentPayment{
	paymentId: number;
	paymentPurpose: number;
	amount: number;
	isPaid: boolean;
	createdDate: Date;
	paymentDate: Date;
}

export interface IRentOffer {
	rentId: number,
    propertyId: number,
    offerId: number,
    isFinished: boolean,
    startDate: Date,
    duration: number,
    endDate: Date,
	propertyAddress: string;
	propertyType: number;
	propertyImages: IImage[];
    tenants: ITenant[];
    payments: IRentPayment[];
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

export interface ITenant{
	userId: number;
	email: string;
	fullName: string;
	profilePicture: IImage;
}

export interface IRentProposition{
  rentId: number;
  startDate: Date;
  endDate: Date;
  duration: number;
  tenants: ITenant[];
}
