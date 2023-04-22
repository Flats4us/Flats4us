import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
	public isUserLoggedIn = false;
	public isUserLoggedInAsStudent = true;

	constructor(private router: Router) {}

	public logIn() {
		this.router.navigate(['auth/login']);
	}
	public signIn() {
		this.router.navigate(['auth/register']);
	}
	public profile() {
		this.router.navigate(['profile/profile']);
	}
}
