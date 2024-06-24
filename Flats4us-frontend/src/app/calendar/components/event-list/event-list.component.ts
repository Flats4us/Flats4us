import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { RouterLink } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
import { BaseComponent } from '@shared/components/base/base.component';
import { takeUntil } from 'rxjs';
import { environment } from 'src/environments/environment';

import { IEvent } from '../../models/calendar.models';
import { CalendarService } from '../../services/calendar.service';

@Component({
	selector: 'app-event-list',
	standalone: true,
	imports: [
		CommonModule,
		MatDialogModule,
		MatButtonModule,
		TranslateModule,
		RouterLink,
	],
	templateUrl: './event-list.component.html',
	styleUrls: ['./event-list.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EventListComponent extends BaseComponent {
	protected baseUrl = environment.apiUrl.replace('/api', '');

	public date!: Date;
	public events: IEvent[] = [];

	protected calendarService = inject(CalendarService);

	public onAccept(meetingId: number): void {
		this.calendarService
			.acceptEvent(meetingId, true)
			.pipe(takeUntil(this.destroyed))
			.subscribe();
	}

	public onReject(meetingId: number): void {
		this.calendarService
			.acceptEvent(meetingId, false)
			.pipe(takeUntil(this.destroyed))
			.subscribe();
	}
}
