import { Directive, ElementRef, Input, OnInit } from '@angular/core';

@Directive({
	selector: '[appGetDescription]',
})
export class GetDescriptionDirective implements OnInit {
	@Input('appGetDescription')
	public count!: number;

	constructor(private el: ElementRef) {}

	public ngOnInit() {
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
