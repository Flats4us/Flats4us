import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import {
	IDispute,
	IProperty,
	IPropertyData,
	IUser,
	IUserData,
} from '../models/moderation-console.models';
import { environment } from '../../../environments/environment.prod';

@Injectable()
export class ModerationConsoleService {
	protected apiRoute = `${environment.apiUrl}/moderator`;

	constructor(private http: HttpClient) {}

	public getUsers(): Observable<IUser[]> {
		let params = new HttpParams();
		params = params.append('pageNumber', 1).append('pageSize', 3);
		return this.http
			.get<IUserData>(`${this.apiRoute}/users`, { params: params })
			.pipe(map(response => response.result));
	}

	public getProperties(): Observable<IProperty[]> {
		let params = new HttpParams();
		params = params.append('pageNumber', 1).append('pageSize', 3);

		return this.http
			.get<IPropertyData>(`${this.apiRoute}/properties`, { params: params })
			.pipe(map(response => response.result));
	}

	public getDisputes(): Observable<IDispute[]> {
		return this.http.get<IDispute[]>('./assets/disputes.json');
	}

	public acceptUser(userId: number) {
		return this.http.put<IUser>(`${this.apiRoute}/users/${userId}/verify`, {
			decision: true,
		});
	}

	public rejectUser(userId: number) {
		return this.http.put<string>(`${this.apiRoute}/users/${userId}/verify`, {
			decision: false,
		});
	}

	public acceptProperty(propertyId: number) {
		return this.http.put<IUser>(
			`${this.apiRoute}/properties/${propertyId}/verify`,
			{
				decision: true,
			}
		);
	}

	public rejectProperty(propertyId: number) {
		return this.http.put<IUser>(
			`${this.apiRoute}/properties/${propertyId}/verify`,
			{
				decision: false,
			}
		);
	}
}
