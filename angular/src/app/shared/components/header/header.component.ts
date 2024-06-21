import {
	ChangeDetectionStrategy,
	Component,
	HostListener,
	OnInit,
	ViewChild,
} from '@angular/core';
import { AuthService } from '@shared/services/auth.service';
import { environment } from '../../../../environments/environment';
import { BreakpointObserver, BreakpointState } from '@angular/cdk/layout';
import { BaseComponent } from '../base/base.component';
import { MatMenuTrigger } from '@angular/material/menu';
import { LocaleService } from '@shared/services/locale.service';
import { ThemeService } from '@shared/services/theme.service';
import { AuthModels } from '@shared/models/auth.models';
import { ProfileService } from 'src/app/profile/services/profile.service';
import { Observable, filter, of, switchMap, takeUntil } from 'rxjs';
import { IUserProfile } from 'src/app/profile/models/profile.models';
import { NotificationsService } from '@shared/services/notifications.service';

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

	public isDarkMode = this.themeService.isDarkMode();

	protected baseUrl = environment.apiUrl.replace('/api', '');

	public languages = ['PL', 'EN'];

	public authModels = AuthModels;

	public avatarURL = './assets/avatar.png';
	public logoDarkURL = './assets/logoFlats4Us_dark.svg';
	public logoLightURL = './assets/logoFlats4Us.svg';

	public headerPhotoURL$ = this.profileService.getHeaderPhotoURL();

	public profile$: Observable<IUserProfile> | undefined;

	constructor(
		public authService: AuthService,
		private breakpointObserver: BreakpointObserver,
		private localeService: LocaleService,
		private themeService: ThemeService,
		private profileService: ProfileService,
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

	@HostListener('window:visibilitychange', ['$event'])
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

	public ngOnInit(): void {
		const theme = window.localStorage.getItem('isDarkMode');
		const locale = window.localStorage.getItem('currentLocale');
		if (theme && !!parseInt(theme) !== this.isDarkMode) {
			this.changeTheme();
		}
		if (locale && locale !== this.localeService.getCurrentLocale()) {
			this.changeLanguage(locale);
		}

		this.authService.isLoggedIn$
			.pipe(
				switchMap(isLoggedIn => {
					if (
						isLoggedIn &&
						(!this.profileService.isHeaderPhotoURL() ||
							this.profileService.isToEdit())
					) {
						this.profile$ = this.profileService.getActualProfile();
						return this.profile$;
					} else {
						return of(null);
					}
				}),
				filter(profile => !!profile),
				this.untilDestroyed()
			)
			.subscribe(profile => {
				this.profileService.setHeaderPhotoURL(profile?.profilePicture?.path);
			});
	}

	public readNotification(id: string) {
		//TODO: notifications have only title and body, no id
		this.notificationsService
			.markRead([id])
			.pipe(takeUntil(this.destroyed))
			.subscribe();
	}

	public changeLanguage(value: string) {
		this.localeService.setLocale(value.toLowerCase());
	}

	public changeTheme() {
		this.isDarkMode = !this.isDarkMode;
		this.themeService.setDarkMode(this.isDarkMode);
	}
}
