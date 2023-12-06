import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IDispute } from '../components/dispute/IDispute';
import { IUser } from '../components/verification/user.interface';
import { IProperty } from '../components/verification/property.interface';

@Injectable({
	providedIn: 'root',
})
export class ModerationConsoleService {
	constructor(private http: HttpClient) {}

	public getUsers(): Observable<IUser[]> {
		const headers = new HttpHeaders().set(
			'Authorization',
			'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiem1vZGVyYXRvckBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNb2RlcmF0b3IiLCJWZXJpZmljYXRpb25TdGF0dXMiOiJWZXJpZmllZCIsImV4cCI6MTcwMTk4MDM3NX0.sL1pYJbdEGpLZRTJOlXQvULRZpFeqp_DZJOcZpOwxWHjbwODcDcje-3aXT1qdu9DBB637WIWmyqfwS5qHYP9gw'
		);
		return this.http.get<IUser[]>(
			'http://172.21.40.120:5166/api/Moderator/User',
			{
				headers,
			}
		);
	}

	public getProperty(): Observable<IProperty[]> {
		const headers = new HttpHeaders().set(
			'Authorization',
			'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiem1vZGVyYXRvckBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNb2RlcmF0b3IiLCJWZXJpZmljYXRpb25TdGF0dXMiOiJWZXJpZmllZCIsImV4cCI6MTcwMTk4MDM3NX0.sL1pYJbdEGpLZRTJOlXQvULRZpFeqp_DZJOcZpOwxWHjbwODcDcje-3aXT1qdu9DBB637WIWmyqfwS5qHYP9gw'
		);
		return this.http.get<IProperty[]>(
			'http://172.21.40.120:5166/api/Moderator/Property',
			{ headers }
		);
	}

	public getDisputes(): Observable<IDispute[]> {
		return this.http.get<IDispute[]>('./assets/disputes.json');
	}

	public acceptUser(user: IUser | undefined) {
		const headers = new HttpHeaders().set(
			'Authorization',
			'Bearer eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiem1vZGVyYXRvckBnbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJNb2RlcmF0b3IiLCJWZXJpZmljYXRpb25TdGF0dXMiOiJWZXJpZmllZCIsImV4cCI6MTcwMTk4MDM3NX0.sL1pYJbdEGpLZRTJOlXQvULRZpFeqp_DZJOcZpOwxWHjbwODcDcje-3aXT1qdu9DBB637WIWmyqfwS5qHYP9gw'
		);

		return this.http.post<IUser[]>(
			'http://172.21.40.120:5166/api/Moderator/User/Verify/' +
				user?.userId +
				'/true',
			{},
			{
				headers,
			}
		);
	}
}
