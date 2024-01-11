export interface IUser {
	userType: number;
	userId: number;
	name: string;
	surname: string;
	address: string;
	email: string;
	phoneNumber: string;
	accountCreationDate: Date;
	verificationStatus: number;
	profilePicture: IProfilePicture;
	document: IDocument;
	documentExpireDate: Date;
	birthDate: Date;
	studentNumber: string;
	university: string;
	links: string[];
	surveyStudent: ISurveyStudent;
	interests: IInterest[];
	bankAccount: string;
	documentNumber: string;
}

interface IProfilePicture {
	name: string;
	path: string;
}

interface IDocument {
	name: string;
	path: string;
}

interface ISurveyStudent {
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
	city: string;
}

interface IInterest {
	interestId: number;
	name: string;
}
