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
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RentsService } from '../../services/rents.service';
import { BaseComponent } from '@shared/components/base/base.component';

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
	],
	providers: [RentsService],
	templateUrl: './meeting-add.component.html',
	styleUrls: ['./meeting-add.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MeetingAddComponent extends BaseComponent {
	public minDate: Date = new Date();

	public meetingForm: FormGroup = new FormGroup({
		date: new FormControl(null, [Validators.required]),
		place: new FormControl('', [Validators.required]),
		reason: new FormControl('', [Validators.required]),
		offerId: new FormControl(null),
	});
	constructor(
		private rentsService: RentsService,
		@Inject(MAT_DIALOG_DATA) public data: number
	) {
		super();
		this.meetingForm.controls['offerId'].setValue(data);
	}

	public onAdd(): void {
		this.rentsService
			.addMeeting(this.meetingForm.value)
			.pipe(this.untilDestroyed())
			.subscribe(() => this.meetingForm.controls['offerId'].setValue(this.data));
	}
}
