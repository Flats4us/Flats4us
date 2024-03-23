import { Injectable } from '@angular/core';
import {
	CanActivate,
	ActivatedRouteSnapshot,
	RouterStateSnapshot,
	Router,
} from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
	providedIn: 'root',
})
export class AuthGuard implements CanActivate {
	constructor(
		private authService: AuthService,
		private router: Router,
	) {}

	public canActivate(
		next: ActivatedRouteSnapshot,
		state: RouterStateSnapshot,
	): boolean {
		return this.checkAuthentication(state.url);
	}

	public checkAuthentication(url: string): boolean {
		if (this.authService.isValidToken()) {
			return true;
		}

		this.router.navigate(['/auth/login'], { queryParams: { returnUrl: url } });
		return false;
	}
}
