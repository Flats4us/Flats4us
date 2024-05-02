export interface IUser {
	userType: number;
	userId: number;
	name: string;
	accountCreationDate: Date;
	verificationStatus: number;
	profilePicture: IProfilePicture;
	avgRating: number;
	userOpinions: IUserOpinion[];
	age: number;
	university: string;
	links: string[];
	surveyStudent: ISurveyStudent;
	interests: IInterest[];
	sumHelpful: number;
	sumCooperative: number;
	sumTidy: number;
	sumFriendly: number;
	sumRespectingPrivacy: number;
	sumCommunicative: number;
	sumUnfair: number;
	sumLackOfHygiene: number;
	sumUntidy: number;
	sumConflicting: number;
	sumNoisy: number;
	sumNotFollowingTheArrangements: number;
}

export interface IMyProfile extends IUser {
	surname: string;
	address: string;
	email: string;
	phoneNumber: string;
	document: IDocument;
	documentExpireDate: Date;
	birthDate: Date;
	studentNumber: string;
	bankAccount: string;
	documentNumber: string;
}

export interface IProfilePicture {
	name: string;
	path: string;
}

export interface IUserOpinion {
	userOpinionId: number;
	date: Date;
	rating: number;
	description: string;
	sourceUserId: number;
	sourceUserName: string;
	sourceUserProfilePicture: IDocument;
}

export interface IDocument {
	name: string;
	path: string;
}

export interface ISurveyStudent {
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

export interface IInterest {
	interestId: number;
	name: string;
}

export interface IVerificationResult {
	result: boolean;
}
