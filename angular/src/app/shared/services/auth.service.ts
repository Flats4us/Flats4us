import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, tap, throwError } from 'rxjs';

import {
	IAuthTokenResponse,
	IPasswordChangeRequest,
	IUser,
} from '../models/auth.models';
import { environment } from 'src/environments/environment.prod';
import { IAddOwner, IAddStudent } from 'src/app/profile/models/profile.models';

@Injectable({
	providedIn: 'root',
})
export class AuthService {
	protected apiRoute = `${environment.apiUrl}/auth`;

	private isLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
		false
	);
	public isLoggedIn$ = this.isLoggedIn.asObservable();

	constructor(private http: HttpClient) {
		setInterval(() => this.isLoggedIn.next(this.isValidToken()), 1000);
	}

	public login({ email, password }: IUser): Observable<IAuthTokenResponse> {
		return this.http
			.post<IAuthTokenResponse>(`${this.apiRoute}/login`, { email, password })
			.pipe(tap(response => this.setToken(response)));
	}

	public register({ email, password }: IUser): Observable<IAuthTokenResponse> {
		return this.http
			.post<IAuthTokenResponse>(`${this.apiRoute}/register`, { email, password })
			.pipe(tap(response => this.setToken(response)));
	}

	public addProfileStudent(
		profile: IAddStudent
	): Observable<IAuthTokenResponse> {
		return this.http
			.post<IAuthTokenResponse>(`${this.apiRoute}/register/students`, profile)
			.pipe(tap(response => this.setToken(response)));
	}

	public addProfileOwner(profile: IAddOwner): Observable<IAuthTokenResponse> {
		return this.http
			.post<IAuthTokenResponse>(`${this.apiRoute}/register/owners`, profile)
			.pipe(tap(response => this.setToken(response)));
	}

	private setToken(response: IAuthTokenResponse): void {
		const { token, expiresAt } = response;
		localStorage.setItem('authToken', token);
		const expirationTime = expiresAt * 1000;
		localStorage.setItem('authTokenExpirationTime', expirationTime.toString());
		this.isLoggedIn.next(true);
	}

	public getAuthToken(): string {
		return localStorage.getItem('authToken') as string;
	}

	public isValidToken(): boolean {
		return true;
		
	}

	public logout(): void {
		localStorage.removeItem('authToken');
		localStorage.removeItem('authTokenExpirationTime');
		this.isLoggedIn.next(false);
	}

	public changePassword({ oldPassword, newPassword }: IPasswordChangeRequest) {
		return this.http.put(`${this.apiRoute}/change-password`, {
			oldPassword,
			newPassword,
		});
	}
}
