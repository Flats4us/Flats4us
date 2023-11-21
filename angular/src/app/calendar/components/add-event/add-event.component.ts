import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
	FormControl,
	FormGroup,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

import { CalendarService } from '../../services/calendar.service';

@Component({
	selector: 'app-add-event',
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
	templateUrl: './add-event.component.html',
	styleUrls: ['./add-event.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddEventComponent {
	public minDate: Date = new Date();

	public form: FormGroup = new FormGroup({
		date: new FormControl(null, [Validators.required]),
		name: new FormControl(null, [Validators.required]),
	});
	constructor(private calendarService: CalendarService) {}

	public onAdd(): void {
		this.calendarService.addEvent(this.form.value).subscribe();
	}
}
