import {
	NgxMatDatetimePickerModule,
	NgxMatNativeDateModule,
	NgxMatTimepickerModule,
} from '@angular-material-components/datetime-picker';
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
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { BaseComponent } from '@shared/components/base/base.component';

import { RentsService } from '../../services/rents.service';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { IOwner, ITenant } from '../../models/rents.models';

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
		TranslateModule,
		NgxMatDatetimePickerModule,
		NgxMatTimepickerModule,
		NgxMatNativeDateModule,
	],
	providers: [RentsService],
	templateUrl: './meeting-add.component.html',
	styleUrls: ['./meeting-add.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MeetingAddComponent extends BaseComponent {
	protected baseUrl = environment.apiUrl.replace('/api', '');

	public owner$!: Observable<IOwner>;
	public tenant$!: Observable<ITenant>;

	public currentDate: Date = new Date();
	public minDate = new Date(
		this.currentDate.setDate(this.currentDate.getDate() + 1)
	);

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
		private translate: TranslateService,
		@Inject(MAT_DIALOG_DATA) public data: number
	) {
		super();
		this.meetingForm.controls['offerId'].setValue(data);
	}

	public onAdd(): void {
		this.meetingForm.markAllAsTouched();
		if (!this.meetingForm.valid) {
			return;
		}
		this.rentsService
			.addMeeting(this.meetingForm.value)
			.pipe(this.untilDestroyed())
			.subscribe({
				next: () => {
					this.meetingForm.controls['offerId'].setValue(this.data);
					this.snackBar.open(
						this.translate.instant('Meeting-add.info2'),
						this.translate.instant('close'),
						{
							duration: 10000,
						}
					);
					this.dialogRef.close();
				},
				error: () => {
					this.snackBar.open(
						this.translate.instant('Meeting-add.info1'),
						this.translate.instant('close'),
						{ duration: 10000 }
					);
					this.dialogRef.close();
				},
			});
	}
}
