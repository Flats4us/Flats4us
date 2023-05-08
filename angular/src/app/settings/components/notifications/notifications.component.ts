import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-notifications',
	templateUrl: './notifications.component.html',
	styleUrls: ['./notifications.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class NotificationsComponent {
	public emailMessages = false;
	public pushMessages = false;
	public emailBookings = false;
	public pushBookings = false;
}
