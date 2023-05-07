import { Directive, ElementRef, Input, OnChanges } from '@angular/core';

@Directive({
	selector: '[appGetDescription]',
})
export class GetDescriptionDirective implements OnChanges {
	@Input('appGetDescription')
	public count = 0;

	constructor(private el: ElementRef) {}

	public ngOnChanges() {
		this.el.nativeElement.textContent = this.getDescription(this.count);
	}

	public getDescription(numberOfRecords: number) {
		if (numberOfRecords == 1) {
			return `${numberOfRecords} oferta`;
		} else if (
			(numberOfRecords > 1 && numberOfRecords <= 4) ||
			(numberOfRecords > 20 &&
				numberOfRecords % 10 > 1 &&
				numberOfRecords % 10 <= 4)
		) {
			return `${numberOfRecords} oferty`;
		} else {
			return `${numberOfRecords} ofert`;
		}
	}
}
