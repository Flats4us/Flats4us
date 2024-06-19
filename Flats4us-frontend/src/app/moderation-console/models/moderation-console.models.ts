import { IImage } from '../../real-estate/models/real-estate.models';
import { IRent, ITenant } from '../../rents/models/rents.models';

export interface IPropertyData {
	totalCount: number;
	result: IProperty[];
}

export interface IProperty {
	propertyId: number;
	propertyType: number;
	ownerName: string;
	ownerEmail: string;
	address: string;
	imagesURLs: string[];
	document: IImage;
	verificationStatus: number;
	dateForVerificationSorting: Date;
}

export interface IUserData {
	totalCount: number;
	result: IUser[];
}

export interface IUser {
	userId: number;
	userType: number;
	name: string;
	surname: string;
	email: string;
	profilePicture: IImage;
	document: IImage;
	documentType: number;
	verificationStatus: number;
	documentExpireDate: Date;
	studentNumber: string;
	university: string;
	documentNumber: string;
}

export interface IDispute {
	argumentId: number;
	title: string;
	description: string;
	startDate: string;
	ownerAcceptanceDate: string;
	studentAccceptanceDate: string;
	argumentStatus: number;
	interventionNeed: boolean;
	interventionNeedDate: string;
	mederatorDecisionDate: string;
	groupChatId: number;
	student: ITenant;
	owner: ITenant;
	argumentInterventions: IIntervention[];
	rent: IRent;
}

export interface IIntervention {
	date: Date;
	justification: string;
	argumentId: number;
	moderatorId: number;
}

export interface ITechnicalProblemData {
	totalCount: number;
	result: ITechnicalProblem[];
}

export interface ITechnicalProblem {
	technicalProblemId: number;
	kind: number;
	description: string;
	date: Date;
	solved: boolean;
	userId: number;
}
