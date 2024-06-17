import { IUser } from '@shared/models/user.models';

export interface IEvent {
	meetingId: number;
	date: Date;
	place: string;
	reason: string;
	offerId: number;
	studentAcceptDate: Date;
	ownerAcceptDate: Date;
	user: IUser;
	needsAction: boolean;
}
