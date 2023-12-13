import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
	IDispute,
	IProperty,
	IUser,
} from '../models/moderation-console.models';
import { environment } from '../../../environments/environment.prod';

@Injectable()
export class ModerationConsoleService {
	protected apiRoute = `${environment.apiUrl}/moderator`;

	constructor(private http: HttpClient) {}

	public getUsers(): Observable<IUser[]> {
		return this.http.get<IUser[]>(`${this.apiRoute}/User`);
	}

	public getProperty(): Observable<IProperty[]> {
		return this.http.get<IProperty[]>(`${this.apiRoute}/Property`);
	}

	public getDisputes(): Observable<IDispute[]> {
		return this.http.get<IDispute[]>('./assets/disputes.json');
	}

	public acceptUser(user: IUser | undefined) {
		return this.http.put<IUser>(`${this.apiRoute}/User/Verify/` + user?.userId, {
			decision: true,
		});
	}

	public rejectUser(user: IUser | undefined) {
		return this.http.put<IUser>(`${this.apiRoute}/User/Verify/` + user?.userId, {
			decision: false,
		});
	}
}
