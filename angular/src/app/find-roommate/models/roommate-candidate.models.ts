export interface IStudent {
	userId: number;
	name: string;
	age: number;
	interests: IInterest[];
	university: string;
	profilePicture: IProfilePicture;
}

export interface IInterest {
	interestId: number;
	name: string;
}

export interface IProfilePicture {
	name: string;
	path: string;
}
