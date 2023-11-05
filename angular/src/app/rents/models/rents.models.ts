import { IFlatOffer } from 'src/app/start/models/start-site.models';
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

export interface IMenuOptions {
	option: string;
	description: string;
}
