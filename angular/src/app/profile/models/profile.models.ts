import { statusType } from './types';

export interface IStudent extends IUser {
	yearOfBirth: string;
	indexNumber: string;
	university: string;
	hobbies: string[];
	socialMedia: string[];
}

export interface IOwner extends IUser {
	bankAccount: string;
}

export interface IUser {
	id: string;
	name: string;
	surname: string;
	address: string;
	phoneNumber: string;
	email: string;
	status: statusType;
	documentType: string;
	documentScan: string;
	validTill: Date;
	photo: string;
}
