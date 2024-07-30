import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { IConversations, IMessage } from '@shared/models/conversation.models';
import * as signalR from '@microsoft/signalr';

@Injectable()
export class ConversationService {
	protected apiRoute = `${environment.apiUrl}/chats`;
	public baseRoute = environment.apiUrl.replace('/api', '');
	private hubConnection!: signalR.HubConnection;
	private onReceivePrivateMessageCallbacks: ((
		user: number,
		message: string,
		timestamp: Date
	) => void)[] = [];
	private onReceiveGroupMessageCallbacks: ((
		groupId: number,
		user: number,
		message: string,
		timestamp: Date
	) => void)[] = [];

	constructor(private http: HttpClient) {}

	public startConnection = (token?: string) => {
		this.stopConnection();
		this.hubConnection = new signalR.HubConnectionBuilder()
			.withUrl(`${this.baseRoute}/${environment.chatSocket}`, {
				accessTokenFactory: () => (token ? token : ''),
			})
			.build();

		this.hubConnection.start().then(() => {
			this.registerEventHandlers();
		});
	};

	private registerEventHandlers() {
		this.onReceivePrivateMessageCallbacks.forEach(callback => {
			this.hubConnection.on(
				'ReceivePrivateMessage',
				(user, message, timestamp) => {
					callback(user, message, timestamp);
				}
			);
		});

		this.onReceiveGroupMessageCallbacks.forEach(callback => {
			this.hubConnection.on(
				'ReceiveGroupMessage',
				(groupId, user, message, timestamp) => {
					callback(groupId, user, message, timestamp);
				}
			);
		});
	}

	public addReceivePrivateMessageHandler(
		callback: (user: number, message: string, timestamp: Date) => void
	) {
		this.onReceivePrivateMessageCallbacks.push(callback);
	}

	public addReceiveGroupMessageHandler(
		callback: (
			groupId: number,
			user: number,
			message: string,
			timestamp: Date
		) => void
	) {
		this.onReceiveGroupMessageCallbacks.push(callback);
	}

	public sendPrivateMessage(receiverId: number, message: string) {
		this.hubConnection.invoke('SendPrivateMessage', receiverId, message);
	}

	public sendGroupMessage(groupChatId: number, message: string): void {
		this.hubConnection.invoke('SendGroupChatMessage', groupChatId, message);
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
		return this.http.get<IConversations[]>(`${this.apiRoute}/user`);
	}

	public getMessages(chatId: string) {
		return this.http.get<IMessage[]>(`${this.apiRoute}/${chatId}/history`);
	}

	public getParticipantId(chatId: string) {
		return this.http.get<string>(`${this.apiRoute}/${chatId}participant`);
	}
}
