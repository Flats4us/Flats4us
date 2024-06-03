import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';

@Injectable({
	providedIn: 'root',
})
export class LocaleService {
	private initialized = false;

	constructor(private router: Router, private translate: TranslateService) {}

	public getCurrentLocale(): string {
		return this.translate.currentLang;
	}

	public initLocale(localeId: string, defaultLocaleId = localeId) {
		if (this.initialized) {
			return;
		}

		this.setDefaultLocale(defaultLocaleId);
		this.setLocale(localeId);

		this.initialized = true;
	}

	public setDefaultLocale(localeId: string) {
		this.translate.setDefaultLang(localeId);
	}

	public setLocale(localeId: string) {
		this.translate.use(localeId);
	}
}
