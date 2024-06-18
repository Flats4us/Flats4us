import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { ChangeDetectionStrategy, Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material/menu';
import { AuthService } from '@shared/services/auth.service';
import { LocaleService } from '@shared/services/locale.service';
import { NotificationsService } from '@shared/services/notifications.service';
import { ThemeService } from '@shared/services/theme.service';
import { UserService } from '@shared/services/user.service';

import { environment } from '../../../../environments/environment.prod';
import { BaseComponent } from '../base/base.component';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent extends BaseComponent implements OnInit {
	@ViewChild('menuTrigger')
	public menuTrigger: MatMenuTrigger | undefined;

	public notifications$ = this.notificationsService.notifications$;

	public isDarkMode: boolean = this.themeService.isDarkMode();

	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected user$ = this.userService.getMyProfile();

	public languages = ['PL', 'EN'];

	@HostListener('window:unload', ['$event'])
	public unloadHandler() {
		if (this.themeService.isDarkMode) {
			window.localStorage.setItem(
				'isDarkMode',
				this.themeService.isDarkMode() ? '1' : '0'
			);
		}
		if (this.localeService.getCurrentLocale()) {
			window.localStorage.setItem(
				'currentLocale',
				this.localeService.getCurrentLocale()
			);
		}
	}

	constructor(
		public authService: AuthService,
		public userService: UserService,
		private breakpointObserver: BreakpointObserver,
		private localeService: LocaleService,
		private themeService: ThemeService,
		private notificationsService: NotificationsService
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

	public ngOnInit(): void {
		const theme = window.localStorage.getItem('isDarkMode');
		const locale = window.localStorage.getItem('currentLocale');
		if (theme && !!parseInt(theme) !== this.isDarkMode) {
			this.changeTheme();
		}
		if (locale && locale !== this.localeService.getCurrentLocale()) {
			this.changeLanguage(locale);
		}

		this.notificationsService.startConnection(this.authService.getAuthToken());
	}

	public changeLanguage(value: string) {
		this.localeService.setLocale(value.toLowerCase());
	}

	public changeTheme() {
		this.isDarkMode = !this.isDarkMode;
		this.themeService.setDarkMode(this.isDarkMode);
	}
}
