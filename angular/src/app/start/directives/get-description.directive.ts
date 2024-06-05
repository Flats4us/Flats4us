import {
	Directive,
	ElementRef,
	Input,
	OnChanges,
	SimpleChanges,
} from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Directive({
	standalone: true,
	selector: '[appGetDescription]',
})
export class GetDescriptionDirective implements OnChanges {
	@Input('appGetDescription')
	public count = 0;

	constructor(private el: ElementRef, private translate: TranslateService) {}

	public ngOnChanges(changes: SimpleChanges) {
		this.el.nativeElement.textContent = this.getDescription(
			changes['count'].currentValue
		);
	}

	public getDescription(numberOfRecords: number) {
		switch (true) {
			case numberOfRecords == 1:
				return `${numberOfRecords} ${this.translate.instant('Start.o1')}`;
			case (numberOfRecords > 1 && numberOfRecords <= 4) ||
				(numberOfRecords > 20 &&
					numberOfRecords % 10 > 1 &&
					numberOfRecords % 10 <= 4):
				return `${numberOfRecords} ${this.translate.instant('Start.o2')}`;
			default:
				return `${numberOfRecords} ${this.translate.instant('Start.o3')}`;
		}
	}
}
