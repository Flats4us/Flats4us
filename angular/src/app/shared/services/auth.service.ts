import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import {
	BehaviorSubject,
	catchError,
	Observable,
	of,
	tap,
	throwError,
} from 'rxjs';

import { IAuthTokenResponse, IUser } from '../models/auth.models';

@Injectable({
	providedIn: 'root',
})
export class AuthService {
	private authTokenSubject: BehaviorSubject<string | null>;

	constructor(private http: HttpClient) {
		this.authTokenSubject = new BehaviorSubject<string | null>(
			localStorage.getItem('currentUser') as string
		);
	}

	public login({ email, password }: IUser): Observable<IAuthTokenResponse> {
		return this.http
			.post<IAuthTokenResponse>('/api/auth/login', { email, password })
			.pipe(tap((response) => this.setToken(response)));
	}

	public register({ email, password }: IUser): Observable<IAuthTokenResponse> {
		return this.http
			.post<IAuthTokenResponse>('/api/auth/register', { email, password })
			.pipe(tap((response) => this.setToken(response)));
	}

	private setToken(response: IAuthTokenResponse): void {
		const { token, expiresAt } = response;
		localStorage.setItem('authToken', token);
		this.authTokenSubject.next(token);
		const expirationTime = new Date().getTime() + expiresAt * 1000;
		localStorage.setItem('authTokenExpirationTime', expirationTime.toString());
		this.scheduleTokenRefresh(expiresAt);
	}

	private scheduleTokenRefresh(expiresAt: number): void {
		const refreshTime = new Date().getTime() + expiresAt * 500;
		const timeoutId = setTimeout(() => {
			this.refreshToken().subscribe();
		}, refreshTime);
		localStorage.setItem('authTokenTimeoutId', timeoutId.toString());
	}

	private refreshToken(): Observable<IAuthTokenResponse> {
		const token = localStorage.getItem('authToken');
		if (!token) {
			return throwError('No auth token found');
		}
		return this.http
			.post<IAuthTokenResponse>('/api/auth/refresh', { token })
			.pipe(
				tap((response) => this.setToken(response)),
				catchError((error) => {
					this.authTokenSubject.next(null);
					localStorage.removeItem('authToken');
					localStorage.removeItem('authTokenExpirationTime');
					localStorage.removeItem('authTokenTimeoutId');
					return of();
				})
			);
	}

	public getAuthToken(): string {
		return this.authTokenSubject.value as string;
	}

	public isLoggedIn(): boolean {
		const token = this.getAuthToken();
		if (!token) {
			return false;
		}
		const expirationTime = localStorage.getItem('authTokenExpirationTime');
		if (!expirationTime) {
			return false;
		}
		const now = new Date().getTime();
		return now < parseInt(expirationTime, 10);
	}

	public logout(): void {
		this.authTokenSubject.next(null);
		localStorage.removeItem('authToken');
		localStorage.removeItem('authTokenExpirationTime');
		const timeoutId = localStorage.getItem('authTokenTimeoutId');
		if (timeoutId) {
			clearTimeout(parseInt(timeoutId, 10));
			localStorage.removeItem('authTokenTimeoutId');
		}
	}
}
