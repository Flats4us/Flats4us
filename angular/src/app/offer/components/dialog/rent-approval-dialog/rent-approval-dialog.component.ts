import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
	MAT_DIALOG_DATA,
	MatDialogModule,
	MatDialogRef,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { OfferService } from 'src/app/offer/services/offer.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { catchError, throwError } from 'rxjs';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
	selector: 'app-rent-approval-dialog',
	standalone: true,
	imports: [CommonModule, MatDialogModule, MatButtonModule, MatSnackBarModule],
	providers: [OfferService],
	templateUrl: './rent-approval-dialog.component.html',
	styleUrls: ['./rent-approval-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentApprovalDialogComponent extends BaseComponent {
	constructor(
		public dialogRef: MatDialogRef<number>,
		public offerService: OfferService,
		private snackBar: MatSnackBar,
		@Inject(MAT_DIALOG_DATA) public data: number
	) {
		super();
	}

	public onYesClick() {
		this.offerService
			.addRentApproval(this.data, { decision: true })
			.pipe(this.untilDestroyed(), catchError(this.handleError))
			.subscribe({
				next: () => {
					this.snackBar.open('Propozycja najmu została zaakceptowana', 'Zamknij', {
						duration: 2000,
					});
					this.dialogRef.close();
				},
				error: () => {
					this.snackBar.open('Błąd. Spróbuj ponownie', 'Zamknij', {
						duration: 2000,
					});
				},
			});
	}

	public onClose() {
		this.offerService
			.addRentApproval(this.data, { decision: false })
			.pipe(this.untilDestroyed(), catchError(this.handleError))
			.subscribe({
				next: () => {
					this.snackBar.open('Propozycja najmu została odrzucona', 'Zamknij', {
						duration: 2000,
					});
					this.dialogRef.close();
				},
				error: () => {
					this.snackBar.open('Błąd. Spróbuj ponownie', 'Zamknij', {
						duration: 2000,
					});
				},
			});
	}

	private handleError(error: HttpErrorResponse) {
		return throwError(() => {
			new Error('Nie udało się dodać najmu. Spróbuj ponownie');
		});
	}
}
