import { statusType } from './types';

export interface IStudent extends IUser {
	birthDate: string;
	indexNumber: string;
	university: string;
	hobbies: IInterest[];
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
	documentType: number;
	documentScan: string;
	validTill: Date;
	photo: string;
}

export interface IAddStudent {
	name: string;
	surname: string;
	address: string;
	password: string;
	email: string;
	phoneNumber: string;
	documentType: number;
	documentExpireDate: Date;
	birthDate: Date;
	studentNumber: string;
	university: string;
	links: string[];
	party: number;
	tidiness: number;
	smoking: boolean;
	sociability: boolean;
	animals: boolean;
	vegan: boolean;
	lookingForRoommate: boolean;
	maxNumberOfRoommates: number;
	roommateGender: number;
	minRoommateAge: number;
	maxRoommateAge: number;
	interests: IInterest[];
}

export interface IInterest {
	interestId: number;
	name: string;
}

export interface IAddOwner {
	name: string;
	surname: string;
	address: string;
	password: string;
	email: string;
	phoneNumber: string;
	documentType: number;
	documentExpireDate: Date;
	bankAccount: string;
	documentNumber: string;
}
