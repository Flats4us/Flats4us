import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { DateAdapter, MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';

@Component({
	selector: 'app-calendar',
	standalone: true,
	imports: [CommonModule, MatDatepickerModule, MatNativeDateModule],
	templateUrl: './calendar.component.html',
	styleUrls: ['./calendar.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CalendarComponent {
	public selected: Date | null = null;

	constructor(private dateAdapter: DateAdapter<Date>) {
		this.dateAdapter.getFirstDayOfWeek = () => 1;
	}
}
