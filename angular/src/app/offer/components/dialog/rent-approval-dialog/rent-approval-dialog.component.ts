import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import {
	MAT_DIALOG_DATA,
	MatDialogModule,
	MatDialogRef,
} from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { BaseComponent } from '@shared/components/base/base.component';
import { IUser } from '@shared/models/user.models';
import { UserService } from '@shared/services/user.service';
import { Observable } from 'rxjs';
import { OfferService } from 'src/app/offer/services/offer.service';
import { environment } from 'src/environments/environment.prod';
import { Router } from '@angular/router';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatIconModule } from '@angular/material/icon';
import { RentsService } from 'src/app/rents/services/rents.service';
import { IRentProposition } from 'src/app/rents/models/rents.models';

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
		MatIconModule,
	],
	providers: [OfferService, UserService, RentsService],
	templateUrl: './rent-approval-dialog.component.html',
	styleUrls: ['./rent-approval-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentApprovalDialogComponent extends BaseComponent {
	public userProfile$: Observable<IUser> = this.userService.getUserById('6');
	protected baseUrl = environment.apiUrl.replace('/api', '');
	public rentProposition$: Observable<IRentProposition>;

	constructor(
		public dialogRef: MatDialogRef<number>,
		public offerService: OfferService,
		public userService: UserService,
		public rentsService: RentsService,
		private snackBar: MatSnackBar,
		private router: Router,
		@Inject(MAT_DIALOG_DATA) public data: { rentId: number; offerId: number }
	) {
		super();
		this.rentProposition$ = this.rentsService.getRentProposition(data.rentId);
	}

	public onYesClick() {
		this.offerService
			.addRentApproval(this.data.offerId, { decision: true })
			.pipe(this.untilDestroyed())
			.subscribe({
				next: () => {
					this.snackBar.open('Propozycja najmu została zaakceptowana', 'Zamknij', {
						duration: 10000,
					});
					this.dialogRef.close(this.data.offerId);
					this.router.navigate(['rents', 'owner', this.data.rentId]);
				},
				error: () => {
					this.snackBar.open('Błąd. Spróbuj ponownie', 'Zamknij', {
						duration: 10000,
					});
					this.dialogRef.close(this.data.offerId);
				},
			});
	}

	public onClose() {
		this.offerService
			.addRentApproval(this.data.offerId, { decision: false })
			.pipe(this.untilDestroyed())
			.subscribe({
				next: () => {
					this.snackBar.open('Propozycja najmu została odrzucona', 'Zamknij', {
						duration: 10000,
					});
					this.dialogRef.close(this.data.offerId);
				},
				error: () => {
					this.snackBar.open('Błąd. Spróbuj ponownie', 'Zamknij', {
						duration: 10000,
					});
					this.dialogRef.close(this.data.offerId);
				},
			});
	}

	public showProfile(id: number) {
		this.router.navigate(['profile', 'details', 'student', id]);
		this.dialogRef.close();
	}
}
