import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.prod';
import {
	IMyProfile,
	IUser,
	IVerificationResult,
} from '@shared/models/user.models';

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

	public resetPassword(token: string, password: string) {
		return this.http.post(`${this.apiRoute}/reset-password`, {
			token,
			password,
		});
	}
}
