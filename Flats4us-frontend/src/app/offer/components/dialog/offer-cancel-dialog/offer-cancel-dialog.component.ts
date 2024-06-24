import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfferService } from 'src/app/offer/services/offer.service';
import {
	MAT_DIALOG_DATA,
	MatDialogModule,
	MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

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
		MatSnackBarModule,
		TranslateModule,
	],
	providers: [OfferService],
	templateUrl: './offer-cancel-dialog.component.html',
	styleUrls: ['./offer-cancel-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferCancelDialogComponent extends BaseComponent {
	constructor(
		public offerService: OfferService,
		private snackBar: MatSnackBar,
		public dialogRef: MatDialogRef<number>,
		private router: Router,
		private translate: TranslateService,
		@Inject(MAT_DIALOG_DATA) public data: number
	) {
		super();
	}

	public onYesClick() {
		this.offerService
			.cancelOffer(this.data)
			.pipe(this.untilDestroyed())
			.subscribe({
				next: () => {
					this.snackBar.open(
						this.translate.instant('Offer.cancel-dialog1'),
						this.translate.instant('close'),
						{
							duration: 10000,
						}
					);
					this.router.navigate(['/offer', 'owner']);
					this.dialogRef.close(this.data);
				},
				error: () => {
					this.snackBar.open(
						this.translate.instant('Offer.cancel-dialog2'),
						this.translate.instant('close'),
						{
							duration: 10000,
						}
					);
					this.dialogRef.close(this.data);
				},
			});
	}
	public onClose() {
		this.dialogRef.close(this.data);
	}
}
