import { ChangeDetectionStrategy, Component } from '@angular/core';
import { AuthService } from '@shared/services/auth.service';
import { UserService } from '@shared/services/user.service';
import { environment } from '../../../../environments/environment.prod';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { BaseComponent } from '../base/base.component';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent extends BaseComponent {
	public isUserLoggedInAsStudent = true;

	public showSidenav = false;

	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected user$ = this.userService.getMyProfile();

	constructor(
		public authService: AuthService,
		public userService: UserService,
		private breakpointObserver: BreakpointObserver
	) {
		super();
		this.breakpointObserver
			.observe(['(max-width: 1000px)'])
			.pipe(this.untilDestroyed())
			.subscribe((result: BreakpointState) => {
				if (!result.matches) {
					this.showSidenav = false;
				}
			});
	}
}
