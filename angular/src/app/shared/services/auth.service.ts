import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';

import {
	IAuthTokenResponse,
	IPasswordChangeRequest,
	IPermission,
	IUser,
	LoggedUserType,
} from '../models/auth.models';
import { environment } from 'src/environments/environment.prod';
import { IAddOwner, IAddStudent } from 'src/app/profile/models/profile.models';
import { Router } from '@angular/router';
import { Router } from '@angular/router';

@Injectable({
	providedIn: 'root',
})
export class AuthService {
	public allPermissions: Map<string, IPermission> = new Map([
		['verifiedStudent', { user: 'STUDENT', status: '0' }],
		['verifiedOwner', { user: 'OWNER', status: '0' }],
		['allLoggedIn', { allLoggedIn: true }],
		['notLoggedIn', { notLoggedIn: true }],
	]);

	protected apiRoute = `${environment.apiUrl}/auth`;

	private isLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
		false
	);
	public isLoggedIn$ = this.isLoggedIn.asObservable();

	private accessControl: BehaviorSubject<{
		user: string;
		status: string;
		isLoggedIn: boolean;
	}> = new BehaviorSubject<{
		user: string;
		status: string;
		isLoggedIn: boolean;
	}>({
		user: this.getUserType(),
		status: this.getUserStatus(),
		isLoggedIn: false,
	});
	public accessControl$ = this.accessControl.asObservable();

	constructor(private http: HttpClient, private router: Router) {
		this.isLoggedIn.next(this.isValidToken());
		this.accessControl.next({
			user: this.getUserType(),
			status: this.getUserStatus(),
			isLoggedIn: this.isValidToken(),
		});
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
		const { token, expiresAt, role, verificationStatus } = response;
		localStorage.setItem('authToken', token);
		const expirationTime = expiresAt * 1000;
		localStorage.setItem('authTokenExpirationTime', expirationTime.toString());
		localStorage.setItem('authRole', role.toUpperCase());
		localStorage.setItem('authStatus', verificationStatus.toString());
		this.isLoggedIn.next(true);
		this.setUserType(response.role, response.verificationStatus.toString(), true);
	}

	public getAuthToken(): string {
		return localStorage.getItem('authToken') as string;
	}

	public getUserType(): string {
		return localStorage.getItem('authRole')?.toUpperCase() as string;
	}

	public getUserStatus(): string {
		return localStorage.getItem('authStatus') as string;
	}

	public isValidToken(): boolean {
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
		localStorage.removeItem('authToken');
		localStorage.removeItem('authTokenExpirationTime');
		localStorage.removeItem('authRole');
		localStorage.removeItem('authStatus');
		this.isLoggedIn.next(false);
		this.setUserType('User', '1', false);
		this.router.navigate(['/']);
	}

	public changePassword({ oldPassword, newPassword }: IPasswordChangeRequest) {
		return this.http.put(`${this.apiRoute}/change-password`, {
			oldPassword,
			newPassword,
		});
	}

	private setUserType(
		userType: string,
		verificationStatus: string,
		isLoggedIn: boolean
	) {
		switch (userType.toUpperCase()) {
			case LoggedUserType.MODERATOR: {
				this.accessControl.next({
					user: LoggedUserType.MODERATOR,
					status: verificationStatus,
					isLoggedIn: isLoggedIn,
				});
				break;
			}
			case LoggedUserType.STUDENT: {
				this.accessControl.next({
					user: LoggedUserType.STUDENT,
					status: verificationStatus,
					isLoggedIn: isLoggedIn,
				});
				break;
			}
			case LoggedUserType.OWNER: {
				this.accessControl.next({
					user: LoggedUserType.OWNER,
					status: verificationStatus,
					isLoggedIn: isLoggedIn,
				});
				break;
			}
			default: {
				this.accessControl.next({
					user: LoggedUserType.USER,
					status: '1',
					isLoggedIn: false,
				});
				break;
			}
		}
	}
}
