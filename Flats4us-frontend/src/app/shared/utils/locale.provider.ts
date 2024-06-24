import { LOCALE_ID, Provider } from '@angular/core';
import { LocaleService } from '@shared/services/locale.service';

export class LocaleId extends String {
	constructor(private localeService: LocaleService) {
		super();
	}

	public override toString(): string {
		return this.localeService.getCurrentLocale();
	}

	public override valueOf(): string {
		return this.toString();
	}
}

export const LocaleProvider: Provider = {
	provide: LOCALE_ID,
	useClass: LocaleId,
	deps: [LocaleService],
};
