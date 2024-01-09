import { CommonModule } from '@angular/common';
import {
	ChangeDetectionStrategy,
	Component,
	Inject,
	OnDestroy,
} from '@angular/core';
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
import { Observable, Subject, takeUntil } from 'rxjs';

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
	templateUrl: './meeting-add.component.html',
	styleUrls: ['./meeting-add.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MeetingAddComponent implements OnDestroy {
	public minDate: Date = new Date();
	public id = 0;
	private readonly unsubscribe$: Subject<void> = new Subject();

	public form: FormGroup = new FormGroup({
		date: new FormControl(null, [Validators.required]),
		place: new FormControl('', [Validators.required]),
		reason: new FormControl('', [Validators.required]),
		offerId: new FormControl(null, [Validators.required]),
	});
	constructor(
		private rentsService: RentsService,
		@Inject(MAT_DIALOG_DATA) public data: Observable<string>
	) {
		data
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(id => (this.id = parseInt(id)));
		this.form.controls['offerId'].setValue(this.id);
	}

	public onAdd(): void {
		this.rentsService
			.addMeeting(this.form.value)
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe();
	}

	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
