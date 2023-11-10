import {
	Directive,
	ElementRef,
	Input,
	OnChanges,
	SimpleChanges,
} from '@angular/core';

@Directive({
	selector: '[appGetPrice]',
})
export class GetPriceDirective implements OnChanges {
	@Input('appGetPrice')
	public price = 0;

	constructor(private el: ElementRef) {}

	public ngOnChanges(changes: SimpleChanges) {
		this.el.nativeElement.textContent = this.getPrice(
			changes['price'].currentValue
		);
	}

	public getPrice(price: number) {
		switch (true) {
			case price < 10:
				return `${price.toPrecision(3).replace('.', ',')}`;
			case price < 100:
				return `${price.toPrecision(4).replace('.', ',')}`;
			case price < 1000:
				return `${price.toPrecision(5).replace('.', ',')}`;
			case price < 10000: {
				switch (true) {
					case price % 1000 < 0.1:
						return `${
							Math.floor(price / 1000) +
							' ' +
							'00' +
							(price % 1000).toPrecision(1).replace('.', ',')
						}`;
					case price % 1000 < 1:
						return `${
							Math.floor(price / 1000) +
							' ' +
							'00' +
							(price % 1000).toPrecision(2).replace('.', ',')
						}`;
					case price % 1000 < 10:
						return `${
							Math.floor(price / 1000) +
							' ' +
							'00' +
							(price % 1000).toPrecision(3).replace('.', ',')
						}`;
					case price % 1000 < 100:
						return `${
							Math.floor(price / 1000) +
							' ' +
							'0' +
							(price % 1000).toPrecision(4).replace('.', ',')
						}`;
					default:
						return `${
							Math.floor(price / 1000) +
							' ' +
							(price % 1000).toPrecision(5).replace('.', ',')
						}`;
				}
			}
			default:
				return `${price.toLocaleString()}`;
		}
	}
}
