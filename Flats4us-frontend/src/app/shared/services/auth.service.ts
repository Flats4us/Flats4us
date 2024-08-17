import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef, Injectable, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { IAddOwner, IAddStudent } from 'src/app/profile/models/profile.models';
import { environment } from 'src/environments/environment';

import {
	AuthModels,
	IAuthTokenResponse,
	IPasswordChangeRequest,
	IPermission,
	IUser,
	LoggedUserType,
} from '../models/auth.models';
import { NotificationsService } from './notifications.service';
import { ProfileService } from 'src/app/profile/services/profile.service';

@Injectable({
	providedIn: 'root',
})
export class AuthService {
	public allPermissions: Map<AuthModels, IPermission> = new Map([
		[AuthModels.MODERATOR, { user: 'MODERATOR', status: '0' }],
		[AuthModels.VERIFIED_STUDENT, { user: 'STUDENT', status: '0' }],
		[AuthModels.VERIFIED_OWNER, { user: 'OWNER', status: '0' }],
		[AuthModels.UNVERIFIED_STUDENT, { user: 'STUDENT', status: '1' }],
		[AuthModels.UNVERIFIED_OWNER, { user: 'OWNER', status: '1' }],
		[AuthModels.ALL_LOGGED_IN, { allLoggedIn: true }],
		[AuthModels.NOT_LOGGED_IN, { notLoggedIn: true }],
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

	public getPermissions(
		permission: AuthModels | AuthModels[]
	): IPermission | IPermission[] {
		if (!(permission instanceof Array)) {
			return this.allPermissions.get(permission) ?? { allLoggedIn: true };
		} else {
			const permissions: IPermission[] = [];
			permission.forEach(perm =>
				permissions.push(this.allPermissions.get(perm) ?? { allLoggedIn: true })
			);
			return permissions ?? { allLoggedIn: true };
		}
	}

	constructor(
		private http: HttpClient,
		private router: Router,
		private notificationsService: NotificationsService,
		private profileService: ProfileService,
		private ngZone: NgZone
	) {
		this.init();
	}

	public init(): void {
		const isLoggedIn = this.isValidToken();

		this.isLoggedIn.next(isLoggedIn);

		setInterval(() => this.checkIfLoggedIn(), 1000);
		this.accessControl.next({
			user: this.getUserType(),
			status: this.getUserStatus(),
			isLoggedIn: this.isValidToken(),
		});

		this.getNotifications();
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
		this.setUserType(true, response.role, response.verificationStatus.toString());
		this.getNotifications();
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
		this.setUserType(false);
		this.profileService.setHeaderPhotoURL('');
		this.router.navigate(['/']);
		this.notificationsService.stopConnection();
	}

	public changePassword({ oldPassword, newPassword }: IPasswordChangeRequest) {
		return this.http.put(`${this.apiRoute}/change-password`, {
			oldPassword,
			newPassword,
		});
	}

	private setUserType(
		isLoggedIn: boolean,
		userType?: string,
		verificationStatus?: string
	) {
		this.accessControl.next({
			isLoggedIn: isLoggedIn,
			user: userType?.toUpperCase() as LoggedUserType,
			status: verificationStatus ?? '',
		});
	}

	public setVerificationStatusToUnverified() {
		this.ngZone.run(() => {
			localStorage.removeItem('authStatus');
			localStorage.setItem('authStatus', '1');
			this.setUserType(true, this.getUserType(), this.getUserStatus());
		});
	}

	public sendPasswordResetLink(email: string) {
		return this.http.post(
			`${this.apiRoute}/${email}/send-password-reset-link`,
			email
		);
	}

	public resetPassword(token: string, password: string) {
		return this.http.put(`${this.apiRoute}/reset-password`, {
			token,
			password,
		});
	}

	private checkIfLoggedIn(): void {
		if (!this.isValidToken() && this.notificationsService.isConnected()) {
			this.notificationsService.stopConnection();
		}

		this.isLoggedIn.next(this.isValidToken());
	}

	private getNotifications() {
		this.notificationsService.startConnection(this.getAuthToken());
	}
}
