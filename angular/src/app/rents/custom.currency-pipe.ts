import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'customCurrencyPipe' })
export class CustomCurrencyPipe implements PipeTransform {
	public transform(value: number, locale?: string): string {
		return new Intl.NumberFormat(locale, {
			minimumFractionDigits: 2,
			useGrouping: true,
		}).format(Number(value));
	}
}
