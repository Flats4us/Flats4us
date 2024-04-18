import {
	CUSTOM_ELEMENTS_SCHEMA,
	ChangeDetectionStrategy,
	Component,
	Inject,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
	MAT_DIALOG_DATA,
	MatDialogModule,
	MatDialogRef,
} from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { OfferService } from 'src/app/offer/services/offer.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { Observable, catchError, throwError } from 'rxjs';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpErrorResponse } from '@angular/common/http';
import { UserService } from '@shared/services/user.service';
import { IUser } from '@shared/models/user.models';
import { MatChipsModule } from '@angular/material/chips';
import { environment } from 'src/environments/environment.prod';
import { MatCardModule } from '@angular/material/card';
import { Router } from '@angular/router';
import { MatTooltipModule } from '@angular/material/tooltip';
import { TranslateModule } from '@ngx-translate/core';

@Component({
	selector: 'app-rent-approval-dialog',
	standalone: true,
	imports: [
		CommonModule,
		MatDialogModule,
		MatButtonModule,
		MatSnackBarModule,
		MatChipsModule,
		MatCardModule,
		MatTooltipModule,
		TranslateModule,
	],
	schemas: [CUSTOM_ELEMENTS_SCHEMA],
	providers: [OfferService, UserService],
	templateUrl: './rent-approval-dialog.component.html',
	styleUrls: ['./rent-approval-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentApprovalDialogComponent extends BaseComponent {
	public userProfile$: Observable<IUser> = this.userService.getUserById(6);
	protected baseUrl = environment.apiUrl.replace('/api', '');

	constructor(
		public dialogRef: MatDialogRef<number>,
		public offerService: OfferService,
		public userService: UserService,
		private snackBar: MatSnackBar,
		private router: Router,
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

	public showProfile(id: number) {
		this.router.navigate(['profile', 'details', 'student', id]);
	}

	private handleError(error: HttpErrorResponse) {
		return throwError(() => {
			new Error('Nie udało się dodać najmu. Spróbuj ponownie');
		});
	}
}
