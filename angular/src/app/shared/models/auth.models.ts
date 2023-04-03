export interface IUser {
	email: string;
	firstName: string;
	lastName: string;
	password: string; // used only to login and register
}

export interface IAuthTokenResponse {
	token: string;
	expiresAt: number;
}
