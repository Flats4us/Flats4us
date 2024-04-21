import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { DateAdapter, MatNativeDateModule } from '@angular/material/core';
import {
	MatCalendarCellClassFunction,
	MatDatepickerModule,
} from '@angular/material/datepicker';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { Observable, tap } from 'rxjs';

import { EventListComponent } from './components/event-list/event-list.component';
import { IEvent } from './models/calendar.models';
import { CalendarService } from './services/calendar.service';

@Component({
	selector: 'app-calendar',
	standalone: true,
	imports: [
		CommonModule,
		MatDatepickerModule,
		MatNativeDateModule,
		MatButtonModule,
		MatDialogModule,
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

	public dateClass: MatCalendarCellClassFunction<Date> = (
		date: Date
	): string | string[] =>
		this.hasEventsMap[date.toDateString()] ? 'has-event' : '';

	public onSelectedChange(event: Date | null, calendar: IEvent[]): void {
		if (
			event &&
			calendar.find(
				e => new Date(e.date).toDateString() === new Date(event).toDateString()
			)
		) {
			const ref = this.dialog.open(EventListComponent);
			ref.componentInstance.date = event;
			ref.componentInstance.events = calendar.filter(
				e => new Date(e.date).toDateString() === new Date(event).toDateString()
			);
		}
	}
}
