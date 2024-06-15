import { IRent } from '../../rents/models/rents.models';

export interface IDispute {
	argumentId: number;
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
