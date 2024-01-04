import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AuthService } from '@shared/services/auth.service';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
	public isUserLoggedInAsStudent = true;

	public showSidenav = false;

	constructor(public authService: AuthService) {}
}
