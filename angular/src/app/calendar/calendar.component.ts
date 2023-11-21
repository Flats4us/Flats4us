import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
	DateAdapter,
	MAT_DATE_LOCALE,
	MatNativeDateModule,
} from '@angular/material/core';
import {
	MatCalendarCellClassFunction,
	MatDatepickerModule,
} from '@angular/material/datepicker';
import { Observable, tap } from 'rxjs';

import { IEvent } from './models/calendar.models';
import { CalendarService } from './services/calendar.service';
import { MatDialog } from '@angular/material/dialog';
import { AddEventComponent } from './components/add-event/add-event.component';
import { MatButtonModule } from '@angular/material/button';

@Component({
	selector: 'app-calendar',
	standalone: true,
	imports: [
		CommonModule,
		MatDatepickerModule,
		MatNativeDateModule,
		MatButtonModule,
	],
	templateUrl: './calendar.component.html',
	styleUrls: ['./calendar.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CalendarComponent {
	public selected: Date | null = null;
	public hasEventsMap: { [date: string]: boolean } = {};

	public calendar$: Observable<IEvent[]> = this.calendaService
		.getEvents()
		.pipe(
			tap(events =>
				events.forEach(
					event => (this.hasEventsMap[new Date(event.date).toDateString()] = true)
				)
			)
		);

	constructor(
		private dateAdapter: DateAdapter<Date>,
		public calendaService: CalendarService,
		private dialog: MatDialog
	) {
		this.dateAdapter.getFirstDayOfWeek = () => 1;
	}

	public onAddEvent(): void {
		this.dialog.open(AddEventComponent, { disableClose: true });
	}

	public dateClass: MatCalendarCellClassFunction<Date> = (
		date: Date
	): string | string[] =>
		this.hasEventsMap[date.toDateString()] ? 'has-event' : '';
}
