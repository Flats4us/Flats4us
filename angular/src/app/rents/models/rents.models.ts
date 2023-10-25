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
	title: string;
	publishDate: string;
	status: string;
	price: number;
	description: string;
	period: number;
	biddersNumber: number;
	viewsNumber: number;
	rules: string;
	imageArray: IGallery[];
	payments: IPayment[];
}

export const Status = {
	valid: 'valid',
	invalid: 'invalid',
	suspended: 'suspended',
	rented: 'rented',
};
