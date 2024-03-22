import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfferService } from 'src/app/offer/services/offer.service';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

@Component({
	selector: 'app-offer-cancel-dialog',
	standalone: true,
	imports: [
		CommonModule,
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
	],
	providers: [OfferService],
	templateUrl: './offer-cancel-dialog.component.html',
	styleUrls: ['./offer-cancel-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferCancelDialogComponent {
	constructor(
		public offerService: OfferService,
		@Inject(MAT_DIALOG_DATA) public data: number
	) {}

	public onYesClick() {
		return;
	}
}
