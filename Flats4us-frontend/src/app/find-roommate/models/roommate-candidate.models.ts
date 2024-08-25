export interface IStudent {
	userId: number;
	name: string;
	age: number;
	interests: IInterest[];
	university: string;
	profilePicture: IProfilePicture;
	chatId: number | null;
}

export interface IInterest {
	interestId: number;
	name: string;
}

export interface IProfilePicture {
	name: string;
	path: string;
}

export interface IDecision {
	decision: boolean;
}
