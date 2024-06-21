import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import {
	IMyProfile,
	IUser,
	IVerificationResult,
} from '@shared/models/user.models';
import { INotificationsSettings } from '../../settings/components/notifications/models/notifications.models';

@Injectable()
export class UserService {
	protected apiRoute = `${environment.apiUrl}/users`;

	constructor(private http: HttpClient) {}

	public getMyProfile() {
		return this.http.get<IMyProfile>(`${this.apiRoute}/my-profile`);
	}

	public getUserById(id: string) {
		return this.http.get<IUser>(`${this.apiRoute}/${id}/profile`);
	}

	public checkIfEmailExist(email: string) {
		return this.http.get<IVerificationResult>(`${this.apiRoute}/${email}`);
	}

	public changeEmail(email: string) {
		return this.http.put(`${this.apiRoute}/current`, {
			email: email,
		});
	}

	public changeConsents({
		pushChatConsent,
		emailChatConsent,
		pushOtherConsent,
		emailOtherConsent,
	}: {
		pushChatConsent: boolean;
		emailChatConsent: boolean;
		pushOtherConsent: boolean;
		emailOtherConsent: boolean;
	}) {
		return this.http.post(`${this.apiRoute}/consent`, {
			pushChatConsent,
			emailChatConsent,
			pushOtherConsent,
			emailOtherConsent,
		});
	}

	public getConsents() {
		return this.http.get<INotificationsSettings>(`${this.apiRoute}/consent`);
	}
}
