import { IImage } from 'src/app/real-estate/models/real-estate.models';
import { StatusType } from './types';

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

export interface IUserProfile {
	userType: number;
	avgRating: number;
	userId: number;
	name: string;
	surname: string;
	address: string;
	email: string;
	phoneNumber: string;
	accountCreationDate: Date;
	verificationStatus: number;
	profilePicture: IImage;
	document: IImage;
	documentExpireDate: Date;
	birthDate: Date;
	studentNumber: number;
	sumCommunicative: number;
	sumConflicting: number;
	sumCooperative: number;
	sumFriendly: number;
	sumHelpful: number;
	sumLackOfHygiene: number;
	sumNoisy: number;
	sumNotFollowingTheArrangements: number;
	sumRespectingPrivacy: number;
	sumTidy: number;
	sumUnfair: number;
	sumUntidy: number;
	university: string;
	links: string[];
	surveyStudent: IStudentSurvey;
	interests: IInterest[];
	bankAccount: string;
	documentNumber: string;
	userOpinions: IOpinion[];
}

export interface IStudentSurvey {
	party: number;
	tidiness: number;
	smoking: true;
	sociability: true;
	animals: true;
	vegan: false;
	lookingForRoommate: true;
	maxNumberOfRoommates: number;
	roommateGender: number;
	minRoommateAge: number;
	maxRoommateAge: number;
	city: string;
}

export interface IUser {
	id: string;
	name: string;
	surname: string;
	address: string;
	phoneNumber: string;
	email: string;
	status: StatusType;
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
	interestIds: number[];
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

export interface IOpinion {
	rating: number;
	helpful: boolean;
	cooperative: boolean;
	tidy: boolean;
	friendly: boolean;
	respectingPrivacy: boolean;
	communicative: boolean;
	lackOfHygiene: boolean;
	untidy: boolean;
	conflicting: boolean;
	noisy: boolean;
	notFollowingTheArrangements: boolean;
	description: string;
}
