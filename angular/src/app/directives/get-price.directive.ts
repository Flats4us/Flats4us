import { CurrencyPipe } from '@angular/common';
import {
	Directive,
	ElementRef,
	Input,
	OnChanges,
	SimpleChanges,
} from '@angular/core';

@Directive({
	selector: '[appGetPrice]',
	providers: [CurrencyPipe],
})
export class GetPriceDirective implements OnChanges {
	@Input('appGetPrice')
	public price = 0;

	constructor(private el: ElementRef, private currencyPipe: CurrencyPipe) {}

	public ngOnChanges(changes: SimpleChanges) {
		this.el.nativeElement.textContent = this.getPrice(
			changes['price'].currentValue
		);
	}

	public getPrice(price: number) {
		return this.currencyPipe
			.transform(price, '', '', '1.2-2')
			?.toString()
			.replace(',', ' ')
			.replace('.', ',');
	}
}
