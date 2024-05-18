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
import { RentsService } from '../../services/rents.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

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
		@Inject(MAT_DIALOG_DATA) public data: number
	) {
		super();
		this.meetingForm.controls['offerId'].setValue(data);
		this.minDate.setDate(this.minDate.getDate() + 1);
	}

	public onAdd(): void {
		this.meetingForm.markAllAsTouched();
		if (this.meetingForm.valid) {
			this.rentsService
				.addMeeting(this.meetingForm.value)
				.pipe(this.untilDestroyed(), catchError(this.handleError))
				.subscribe({
					next: () => {
						this.meetingForm.controls['offerId'].setValue(this.data);
						this.snackBar.open(
							'Pomyślnie dodano spotkanie.',
							'Zamknij',
							{ duration: 2000 }
						);
						this.dialogRef.close();
					},
					error: () => {
						this.snackBar.open(
							'Nie udało się dodać spotkania. Spróbuj ponownie.',
							'Zamknij',
							{ duration: 2000 }
						);
						this.dialogRef.close();
					},
				});
		}
	}

	private handleError(error: HttpErrorResponse) {
		return throwError(
			() => new Error('Nie udało się dodać spotkania. Spróbuj później')
		);
	}
}
