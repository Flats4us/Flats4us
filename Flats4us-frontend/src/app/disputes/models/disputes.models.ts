import { IRent } from '../../rents/models/rents.models';
import { IProfilePicture } from '@shared/models/user.models';

export interface IDispute {
	argumentId: number;
	title: string;
	description: string;
	startDate: Date;
	ownerAcceptanceDate: Date | null;
	studentAccceptanceDate: Date | null;
	argumentStatus: number;
	interventionNeed: boolean;
	interventionNeedDate: Date;
	groupChatId: number;
	student: IStudent;
	rent: IRent;
}

export interface IStudent {
	userId: number;
	email: string;
	fullName: string;
	profilePicture: string | null;
}

export interface IGroupChat {
	groupChatId: number;
	name: string;
	users: IParticipant[];
}

export interface IParticipant {
	userId: number;
	email: string;
	fullName: string;
	profilePicture: IProfilePicture;
}
