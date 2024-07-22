import { CommonModule } from '@angular/common';
import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
} from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { DateAdapter, MatNativeDateModule } from '@angular/material/core';
import {
	MatCalendarCellClassFunction,
	MatDatepickerModule,
} from '@angular/material/datepicker';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { TranslateModule } from '@ngx-translate/core';
import { BaseComponent } from '@shared/components/base/base.component';
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
		TranslateModule,
	],
	templateUrl: './calendar.component.html',
	styleUrls: ['./calendar.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CalendarComponent extends BaseComponent {
	public selected: Date | null = null;
	public hasEventsMap: { [date: string]: boolean } = {};

	public calendar$: Observable<IEvent[]> = this.calendarService
		.getEvents()
		.pipe(
			tap(events =>
				events.forEach(
					event => (this.hasEventsMap[new Date(event.date).toDateString()] = true)
				)
			)
		);

	public calendarToAccept$: Observable<IEvent[]> =
		this.calendarService.getEventsToAccept();

	constructor(
		private dateAdapter: DateAdapter<Date>,
		public calendarService: CalendarService,
		private dialog: MatDialog,
		private cdr: ChangeDetectorRef
	) {
		super();

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
			const ref = this.dialog.open(EventListComponent, { width: '400px' });
			ref.componentInstance.date = event;
			ref.componentInstance.events = calendar.filter(
				e => new Date(e.date).toDateString() === new Date(event).toDateString()
			);

			ref.afterClosed().subscribe(() => {
				this.calendarToAccept$ = this.calendarService.getEventsToAccept();
				this.cdr.markForCheck();
			});
		}
	}

	public onEventClick(event: IEvent): void {
		const ref = this.dialog.open(EventListComponent, { width: '400px' });
		ref.componentInstance.date = event.date;
		ref.componentInstance.events = [event];

		ref.afterClosed().subscribe(() => {
			this.calendarToAccept$ = this.calendarService.getEventsToAccept();
			this.cdr.markForCheck();
		});
	}
}
