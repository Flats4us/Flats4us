import { IOffer } from 'src/app/offer/models/offer.models';

export interface IEquipment {
	equipmentId: number;
	name: string;
}

export interface ISortOption {
	type: string;
	direction: string;
	description: string;
}

export interface ISendOffers {
	totalCount: number;
	result: IOffer[];
}
