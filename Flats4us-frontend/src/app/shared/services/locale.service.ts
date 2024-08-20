import { Injectable, Optional, SkipSelf } from '@angular/core';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { noop } from 'rxjs';

type ShouldReuseRoute = (
	future: ActivatedRouteSnapshot,
	curr: ActivatedRouteSnapshot
) => boolean;

@Injectable({
	providedIn: 'root',
})
export class LocaleService {
	private initialized = false;

	constructor(
		private router: Router,
		private translate: TranslateService,
		@Optional()
		@SkipSelf()
		otherInstance: LocaleService
	) {
		if (otherInstance) {
			throw 'LocaleService should have only one instance.';
		}
	}

	private setRouteReuse(reuse: ShouldReuseRoute) {
		this.router.routeReuseStrategy.shouldReuseRoute = reuse;
	}

	private subscribeToLangChange() {
		this.translate.onLangChange.subscribe(async () => {
			const { shouldReuseRoute } = this.router.routeReuseStrategy;
			const currentUrl = this.router.url;
			const newUrl = this.router.serializeUrl(
				this.router.createUrlTree([currentUrl])
			);
			this.setRouteReuse(() => false);
			this.router.navigated = false;
			await this.router.navigateByUrl(newUrl).catch(noop);
			this.setRouteReuse(shouldReuseRoute);
		});
	}

	public getCurrentLocale(): string {
		return this.translate.currentLang;
	}

	public initLocale(localeId: string, defaultLocaleId = localeId) {
		if (this.initialized) {
			return;
		}

		this.setDefaultLocale(defaultLocaleId);
		this.setLocale(localeId);
		this.subscribeToLangChange();

		this.initialized = true;
	}

	public setDefaultLocale(localeId: string) {
		this.translate.setDefaultLang(localeId);
	}

	public setLocale(localeId: string) {
		this.translate.use(localeId);
	}
}
