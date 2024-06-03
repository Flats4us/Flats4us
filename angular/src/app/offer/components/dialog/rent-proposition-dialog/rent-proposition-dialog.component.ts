import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import {
	FormControl,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
	MAT_DIALOG_DATA,
	MatDialogModule,
	MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {
	MatChipEditedEvent,
	MatChipInputEvent,
	MatChipsModule,
} from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { CommonModule } from '@angular/common';
import { OfferService } from 'src/app/offer/services/offer.service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { BaseComponent } from '@shared/components/base/base.component';
import { Observable, map, of, switchMap } from 'rxjs';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { UserService } from '@shared/services/user.service';
import { setLocalDate } from '@shared/utils/functions';

@Component({
	selector: 'app-rent-proposition-dialog',
	templateUrl: './rent-proposition-dialog.component.html',
	styleUrls: ['./rent-proposition-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
	standalone: true,
	imports: [
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		ReactiveFormsModule,
		MatChipsModule,
		MatIconModule,
		CommonModule,
		MatDatepickerModule,
		MatSnackBarModule,
	],
	providers: [OfferService],
})
export class RentPropositionDialogComponent extends BaseComponent {
	public separatorKeysCodes: number[] = [ENTER, COMMA];
	public addOnBlur = true;
	public tenantsCtrl = new FormControl('');
	public tenants: string[] = [];
	public minDate: Date = new Date();
	public invalidEmail$: Observable<boolean> = of(false);
	public tooManyTenants$: Observable<boolean> = of(false);
	public setLocalDate = setLocalDate;

	public rentPropositionForm: FormGroup = new FormGroup({
		roommatesEmails: new FormControl(this.tenants),
		startDate: new FormControl(null, Validators.required),
		duration: new FormControl(null, [Validators.required, Validators.min(1)]),
	});

	constructor(
		private snackBar: MatSnackBar,
		public dialogRef: MatDialogRef<{
			id: number;
			maxNumberOfInhabitants: number;
		}>,
		public offerService: OfferService,
		public userService: UserService,
		@Inject(MAT_DIALOG_DATA)
		public data: { id: number; maxNumberOfInhabitants: number }
	) {
		super();
	}

	public onClose() {
		this.dialogRef.close(this.data);
	}

	public onYesClick() {
		this.rentPropositionForm.markAllAsTouched();
		if (this.rentPropositionForm.valid) {
			this.offerService
				.addRentProposition(this.rentPropositionForm.value, this.data.id)
				.pipe(this.untilDestroyed())
				.subscribe({
					next: () => {
						this.snackBar.open(
							'Propozycja najmu została wysłana do Właściciela i czeka na akceptację!',
							'Zamknij',
							{
								duration: 10000,
							}
						);
						this.dialogRef.close(this.data);
					},
					error: () => {
						this.snackBar.open('Nie udało się dodać najmu.', 'Zamknij', {
							duration: 10000,
						});
						this.dialogRef.close(this.data);
					},
				});
		}
	}
	public add(
		event: MatChipInputEvent,
		items: string[],
		formControl: FormControl
	): void {
		this.invalidEmail$ = of(false);
		this.tooManyTenants$ = of(false);
		const value = (event.value || '').trim();
		if (value && !items.includes(value.trim())) {
			items.push(value);
			this.invalidEmail$ = this.userService
				.checkIfEmailExist(value)
				.pipe(map(result => !result.result));
			this.tooManyTenants$ = this.tooManyTenants$.pipe(
				switchMap(() =>
					items.length > this.data.maxNumberOfInhabitants - 1 ? of(true) : of(false)
				)
			);
		}
		event.chipInput.clear();

		formControl.setValue(null);
	}

	public remove(item: string, items: string[]): void {
		const index = items.indexOf(item);

		if (index >= 0) {
			items.splice(index, 1);
		}
	}

	public edit(tenant: string, event: MatChipEditedEvent) {
		const value = event.value.trim();
		if (!value) {
			this.remove(tenant, this.tenants);
			return;
		}
		const index = this.tenants.indexOf(tenant);
		if (index >= 0) {
			this.tenants[index] = value;
		}
	}
}
