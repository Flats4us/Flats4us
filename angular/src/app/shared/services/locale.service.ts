import { Injectable, OnDestroy, Optional, SkipSelf } from '@angular/core';
import { ActivatedRouteSnapshot, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { Subject, noop, takeUntil } from 'rxjs';

type ShouldReuseRoute = (
	future: ActivatedRouteSnapshot,
	curr: ActivatedRouteSnapshot
) => boolean;

@Injectable({
	providedIn: 'root',
})
export class LocaleService implements OnDestroy {
	protected destroyed: Subject<void> = new Subject<void>();
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
		this.translate.onLangChange
			.pipe(takeUntil(this.destroyed))
			.subscribe(async () => {
				const { shouldReuseRoute } = this.router.routeReuseStrategy;

				this.setRouteReuse(() => false);
				this.router.navigated = false;
				await this.router.navigateByUrl(this.router.url).catch(noop);
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

	public ngOnDestroy(): void {
		this.destroyed.next();
		this.destroyed.complete();
	}
}
