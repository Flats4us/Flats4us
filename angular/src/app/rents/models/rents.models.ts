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

export interface ISendRent {
	totalCount: number;
	result: IRent[];
}

export interface IRentPayment {
	paymentId: number;
	paymentPurpose: number;
	amount: number;
	isPaid: boolean;
	createdDate: Date;
	paymentDate: Date;
}

export interface IRent {
	rentId: number;
	propertyId: number;
	offerId: number;
	isFinished: boolean;
	startDate: Date;
	duration: number;
	endDate: Date;
	propertyAddress: string;
	propertyType: number;
	mainTenantId: number;
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

export interface ITenant {
	userId: number;
	email: string;
	fullName: string;
	profilePicture: IImage;
}

export interface IRentProposition {
	rentId: number;
	startDate: Date;
	endDate: Date;
	duration: number;
	mainTenantId: number;
	tenants: ITenant[];
}
