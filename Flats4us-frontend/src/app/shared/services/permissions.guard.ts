import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router } from '@angular/router';
import { AuthModels } from '@shared/models/auth.models';
import { AuthService } from './auth.service';

@Injectable({
	providedIn: 'root',
})
export class PermissionsGuard implements CanActivate {
	constructor(private router: Router, private authService: AuthService) {}

	public canActivate(next: ActivatedRouteSnapshot): boolean {
		const { requiredPermissions } = next.data;
		const params = next.paramMap.keys;
		const routes = next.pathFromRoot
			.flatMap(route => route.url)
			.map(url => url.path);
		return this.checkPermissions(requiredPermissions, params, next, routes);
	}

	public checkPermissions(
		permissionsArray: AuthModels[],
		params: string[],
		route: ActivatedRouteSnapshot,
		routes: string[]
	): boolean {
		const role = this.authService.getUserType() ?? '';
		const status = this.authService.getUserStatus() ?? '';
		const actualUserPermission = this.getPermission(role, status);
		const checkPermissions = permissionsArray.some(
			permission => permission === actualUserPermission
		);
		if (checkPermissions && !role) {
			return true;
		} else if (
			checkPermissions &&
			this.checkParams(params, role, route, routes)
		) {
			return true;
		} else {
			this.router.navigate(['/']);
			return false;
		}
	}

	private checkParams(
		params: string[],
		role: string,
		route: ActivatedRouteSnapshot,
		routes: string[]
	) {
		const modificationType = route.paramMap.get('modificationType');
		const surveyType = route.paramMap.get('survey-type');
		if (routes.includes('profile') && modificationType?.includes('create')) {
			return true;
		} else if (routes.includes('profile') && modificationType?.includes('edit')) {
			let editProfileTest = false;
			role ? (editProfileTest = true) : (editProfileTest = false);
			return editProfileTest;
		} else if (routes.includes('profile') && surveyType) {
			return role === surveyType?.toUpperCase();
		} else if (params.includes('user')) {
			const user = route.paramMap.get('user');
			let paramsTest = false;
			user === 'details'
				? (paramsTest = true)
				: (paramsTest = role === user?.toUpperCase());
			return paramsTest;
		} else {
			return true;
		}
	}

	private getPermission(role: string, status: string): AuthModels {
		switch (true) {
			case role === 'OWNER' && status === '0': {
				return AuthModels.VERIFIED_OWNER;
			}
			case role === 'OWNER' && status === '1': {
				return AuthModels.UNVERIFIED_OWNER;
			}
			case role === 'OWNER' && status === '2': {
				return AuthModels.UNVERIFIED_OWNER;
			}
			case role === 'STUDENT' && status === '0': {
				return AuthModels.VERIFIED_STUDENT;
			}
			case role === 'STUDENT' && status === '1': {
				return AuthModels.UNVERIFIED_STUDENT;
			}
			case role === 'STUDENT' && status === '2': {
				return AuthModels.UNVERIFIED_STUDENT;
			}
			case role === 'MODERATOR' && status === '0': {
				return AuthModels.MODERATOR;
			}
			default: {
				return AuthModels.NOT_LOGGED_IN;
			}
		}
	}
}
