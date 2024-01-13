import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import {
	FormBuilder,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { BaseComponent } from '@shared/components/base/base.component';
import { OfferService } from 'src/app/offer/services/offer.service';

@Component({
	selector: 'app-offer-promotion-dialog',
	templateUrl: './offer-promotion-dialog.component.html',
	styleUrls: ['./offer-promotion-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
	standalone: true,
	imports: [
		CommonModule,
		ReactiveFormsModule,
		FormsModule,
		MatButtonModule,
		MatFormFieldModule,
		MatInputModule,
	],
	providers: [OfferService],
})
export class OfferPromotionDialogComponent extends BaseComponent {
	public promotionForm: FormGroup = this.formBuilder.group({
		promotionDays: [
			null,
			[Validators.required, Validators.min(1), Validators.max(30)],
		],
	});

	constructor(
		private offerService: OfferService,
		private formBuilder: FormBuilder,
		public dialogRef: MatDialogRef<number>,
		@Inject(MAT_DIALOG_DATA) public data: number
	) {
		super();
	}

	public onYesClick() {
		this.offerService
			.addOfferPromotion(this.data, {
				duration: this.promotionForm.get('promotionDays')?.value,
			})
			.pipe(this.untilDestroyed())
			.subscribe(() => this.dialogRef.close());
	}
	public onClose() {
		this.dialogRef.close();
	}
}
