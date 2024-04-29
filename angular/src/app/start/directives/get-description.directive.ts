import {
	Directive,
	ElementRef,
	Input,
	OnChanges,
	SimpleChanges,
} from '@angular/core';

@Directive({
	standalone: true,
	selector: '[appGetDescription]',
})
export class GetDescriptionDirective implements OnChanges {
	@Input('appGetDescription')
	public count = 0;

	constructor(private el: ElementRef) {}

	public ngOnChanges(changes: SimpleChanges) {
		this.el.nativeElement.textContent = this.getDescription(
			changes['count'].currentValue
		);
	}

	public getDescription(numberOfRecords: number) {
		switch (true) {
			case numberOfRecords == 1:
				return `${numberOfRecords} ofertę`;
			case (numberOfRecords > 1 && numberOfRecords <= 4) ||
				(numberOfRecords > 20 &&
					numberOfRecords % 10 > 1 &&
					numberOfRecords % 10 <= 4):
				return `${numberOfRecords} oferty`;
			default:
				return `${numberOfRecords} ofert`;
		}
	}
}
