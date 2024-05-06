import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AuthService } from '@shared/services/auth.service';
import { UserService } from '@shared/services/user.service';
import { environment } from '../../../../environments/environment.prod';
import { TranslateService } from '@ngx-translate/core';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
	public isUserLoggedInAsStudent = true;

	public showSidenav = false;

	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected user$ = this.userService.getMyProfile();

	constructor(
		public authService: AuthService,
		public userService: UserService,
		public translate: TranslateService
	) {}

	public changeLanguage(value: MatSlideToggleChange) {
		switch (value.checked) {
			case false: {
				this.translate.use('pl');
				break;
			}
			case true: {
				this.translate.use('en');
				break;
			}
		}
	}
}
