import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent  {
	isUserLoggedIn = true;
	isUserLoggedInAsStudent = true;

	showOptions1 = false;
	showOptions2 = false;
	showOptions3 = false;
	showOptions4 = false;
	showOptions5 = false;
  selectedLanguage: string = 'ENGLISH';

	constructor(private router: Router) {}

	logIn() {
		this.router.navigate(['/login']);
	}
	signIn() {
		this.router.navigate(['/register']);
	}
}
