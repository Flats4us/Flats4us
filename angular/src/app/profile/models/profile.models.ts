export interface IStudent {
	id: string;
	photo: string;
	name: string;
	surname: string;
	yearOfBirth: string;
	indexNumber: string;
	university: string;
	address: string;
	phoneNumber: string;
	hobbies: string[];
	socialMedia: string[];
	documentType: string;
	documentScan: string;
	validTill: Date;
	email: string;
	password: string;
}

export interface IOwner {
	id: string;
	photo: string;
	name: string;
	surname: string;
	address: string;
	phoneNumber: string;
	documentType: string;
	documentScan: string;
	validTill: Date;
	email: string;
	password: string;
	bankAccount: string;
}

export interface IUser {
	id: string;
	name: string;
	surname: string;
	address: string;
	phoneNumber: string;
	email: string;
	password: string;
}
