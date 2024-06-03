import { ChangeDetectionStrategy, Component, ViewChild } from '@angular/core';
import { AuthService } from '@shared/services/auth.service';
import { UserService } from '@shared/services/user.service';
import { environment } from '../../../../environments/environment.prod';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { BaseComponent } from '../base/base.component';
import { MatMenuTrigger } from '@angular/material/menu';
import { LocaleService } from '@shared/services/locale.service';
import { Router } from '@angular/router';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent extends BaseComponent {
	@ViewChild('menuTrigger')
	public menuTrigger: MatMenuTrigger | undefined;

	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected user$ = this.userService.getMyProfile();

	public languages = ['PL', 'EN'];

	constructor(
		public authService: AuthService,
		public userService: UserService,
		private breakpointObserver: BreakpointObserver,
		private localeService: LocaleService,
		private router: Router
	) {
		super();
		this.breakpointObserver
			.observe(['(max-width: 1000px)'])
			.pipe(this.untilDestroyed())
			.subscribe((result: BreakpointState) => {
				if (!result.matches) {
					this.menuTrigger?.closeMenu();
				}
			});
	}

	public changeLanguage(value: string) {
		this.localeService.setLocale(value.toLowerCase());
		const url = this.router.url;
		this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
			this.router.navigate([url]);
		});
	}
}
