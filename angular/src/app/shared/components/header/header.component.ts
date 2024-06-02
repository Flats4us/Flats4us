import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AuthService } from '@shared/services/auth.service';
import { UserService } from '@shared/services/user.service';
import { environment } from '../../../../environments/environment.prod';
import { BaseComponent } from '../base/base.component';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent extends BaseComponent {
	public isUserLoggedInAsStudent = true;

	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected user$ = this.userService.getMyProfile();

	public languages = ['PL', 'EN'];

	constructor(
		public authService: AuthService,
		public userService: UserService,
		private translate: TranslateService
	) {
		super();
	}

	public changeLanguage(value: string) {
		this.translate.use(value.toLowerCase());
	}
}
