import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import {
	FormControl,
	FormGroup,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import {
	MAT_DIALOG_DATA,
	MatDialogModule,
	MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { BaseComponent } from '@shared/components/base/base.component';
import { Observable, take } from 'rxjs';

import { RentsService } from '../../services/rents.service';

@Component({
	selector: 'app-meeting-add',
	standalone: true,
	imports: [
		CommonModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatNativeDateModule,
		MatDatepickerModule,
		MatInputModule,
		MatDialogModule,
		MatButtonModule,
		MatSnackBarModule,
	],
	providers: [RentsService],
	templateUrl: './meeting-add.component.html',
	styleUrls: ['./meeting-add.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MeetingAddComponent extends BaseComponent {
	public minDate: Date = new Date();

	public meetingForm: FormGroup = new FormGroup({
		date: new FormControl(null, Validators.required),
		place: new FormControl('', Validators.required),
		reason: new FormControl('', Validators.required),
		offerId: new FormControl(null),
	});
	constructor(
		public snackBar: MatSnackBar,
		public dialogRef: MatDialogRef<number>,
		private rentsService: RentsService,
		@Inject(MAT_DIALOG_DATA) public data: Observable<number>
	) {
		super();

		data
			.pipe(take(1))
			.subscribe(offerId =>
				this.meetingForm.controls['offerId'].setValue(offerId)
			);
		this.minDate.setDate(this.minDate.getDate() + 1);
	}

	public onAdd(): void {
		this.meetingForm.markAllAsTouched();

		if (this.meetingForm.invalid) {
			return;
		}

		this.rentsService
			.addMeeting(this.meetingForm.value)
			.pipe(this.untilDestroyed())
			.subscribe({
				next: () => {
					this.snackBar.open('Spotkanie zostało dodane.', 'Zamknij', {
						duration: 2000,
					});
					this.dialogRef.close();
				},
				error: () => {
					this.snackBar.open(
						'Nie udało się dodać spotkania. Spróbuj ponownie.',
						'Zamknij',
						{ duration: 2000 }
					);
				},
			});
	}
}
