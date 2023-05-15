import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
	public isUserLoggedIn = true;
	public isUserLoggedInAsStudent = true;

	constructor(private router: Router) {}

	public profile(id: string) {
		this.router.navigate(['profile/', id]);
	}
}
