import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-add-offer',
	templateUrl: './add-offer.component.html',
	styleUrls: ['./add-offer.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddOfferComponent {}
