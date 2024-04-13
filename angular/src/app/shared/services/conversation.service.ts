import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { IConversations, IMessage } from '@shared/models/conversation.models';
import * as signalR from '@microsoft/signalr';

@Injectable()
export class ConversationService {
	protected apiRoute = `${environment.apiUrl}/chat`;
	public baseRoute = environment.apiUrl.replace('/api', '');
	private hubConnection!: signalR.HubConnection;
	private onReceivePrivateMessageCallbacks: ((
		user: number,
		message: string
	) => void)[] = [];

	constructor(private http: HttpClient) {}

	public startConnection = (token?: string) => {
		this.stopConnection();
		this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl(`${this.baseRoute}/chatHub`, {
				accessTokenFactory: () => (token ? token : ''),
			})
			.build();

		this.hubConnection.start().then(() => {
			this.registerEventHandlers();
		});
	};

	private registerEventHandlers() {
		this.onReceivePrivateMessageCallbacks.forEach(callback => {
			this.hubConnection.on('ReceivePrivateMessage', (user, message) => {
				callback(user, message);
			});
		});
	}

	public addReceivePrivateMessageHandler(
		callback: (user: number, message: string) => void
	) {
		this.onReceivePrivateMessageCallbacks.push(callback);
	}

	public sendPrivateMessage(receiverId: number, message: string) {
		this.hubConnection.invoke('SendPrivateMessage', receiverId, message);
	}

	public sendMessage = (user: string, message: string) => {
		this.hubConnection.invoke('SendMessage', user, message);
	};

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

	public getConversations() {
		return this.http.get<IConversations[]>(`${this.apiRoute}/user/chats`);
	}

	public getMessages(chatId: number) {
		return this.http.get<IMessage[]>(`${this.apiRoute}/history/${chatId}`);
	}

	public getParticipantId(chatId: number) {
		return this.http.get<number>(`${this.apiRoute}/participant/${chatId}`);
	}
}
