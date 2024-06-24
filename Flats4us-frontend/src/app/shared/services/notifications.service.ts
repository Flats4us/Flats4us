import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import * as signalR from '@microsoft/signalr';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject } from 'rxjs';

import { environment } from '../../../environments/environment';

@Injectable({ providedIn: 'root' })
export class NotificationsService {
	protected apiRoute = `${environment.apiUrl}/notifications`;
	public baseRoute = environment.apiUrl.replace('/api', '');

	private hubConnection!: signalR.HubConnection;
	private onReceiveNotificationCallbacks: ((
		user: number,
		message: string,
		timestamp: Date
	) => void)[] = [];

	public notifications$ = new BehaviorSubject<string[]>([]);

	constructor(
		private http: HttpClient,
		private snackbar: MatSnackBar,
		private translateService: TranslateService
	) {
		this.addReceiveNotificationHandler((_user, message) => {
			this.snackbar.open(this.translateService.instant(message), 'Close', {
				duration: 5000,
				verticalPosition: 'top',
				horizontalPosition: 'right',
			});
			const notification = this.notifications$.value;
			this.notifications$.next([...notification, message]);
		});
	}

	public startConnection = (token?: string) => {
		this.stopConnection();
		this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl(`${this.baseRoute}/${environment.notificationSocket}`, {
				accessTokenFactory: () => (token ? token : ''),
			})
			.build();

		this.hubConnection.start().then(() => this.registerEventHandlers());
	};

	private registerEventHandlers() {
		this.onReceiveNotificationCallbacks.forEach(callback => {
			this.hubConnection.on('ReceiveNotification', (user, message, timestamp) => {
				callback(user, message, timestamp);
			});
		});
	}

	public addReceiveNotificationHandler(
		callback: (user: number, message: string, timestamp: Date) => void
	) {
		this.onReceiveNotificationCallbacks.push(callback);
	}

	public isConnected(): boolean {
		return (
			this.hubConnection &&
			this.hubConnection.state === signalR.HubConnectionState.Connected
		);
	}

	public stopConnection = () => {
		if (this.isConnected()) {
			this.hubConnection.stop();
		}
	};

	public getNotifications() {
		return this.http.get<string[]>(`${this.apiRoute}/all`);
	}

	public markRead(notificationIds: string[]) {
		return this.http.post<void>(`${this.apiRoute}/read`, {
			notificationIds,
		});
	}
}
