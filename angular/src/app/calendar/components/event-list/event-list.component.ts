import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';

import { IEvent } from '../../models/calendar.models';
import { MatButtonModule } from '@angular/material/button';

@Component({
	selector: 'app-event-list',
	standalone: true,
	imports: [CommonModule, MatDialogModule, MatButtonModule],
	templateUrl: './event-list.component.html',
	styleUrls: ['./event-list.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EventListComponent {
	public date!: Date;
	public events: IEvent[] = [];
}
