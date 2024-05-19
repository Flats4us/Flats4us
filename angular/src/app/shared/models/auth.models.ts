export interface IUser {
	email: string;
	firstName: string;
	lastName: string;
	password: string; // used only to login and register
}

export enum LoggedUserType {
	MODERATOR = 'MODERATOR',
	STUDENT = 'STUDENT',
	OWNER = 'OWNER',
}

export interface IAuthTokenResponse {
	token: string;
	expiresAt: number;
	role: string;
	verificationStatus: number;
}

export interface IPasswordChangeRequest {
	oldPassword: string;
	newPassword: string;
}

export interface IPermission {
	user?: string;
	status?: string;
	allLoggedIn?: boolean;
	notLoggedIn?: boolean;
}
