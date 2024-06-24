import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IDispute, IGroupChat } from '../models/disputes.models';
import { environment } from '../../../environments/environment.prod';
import { IMessage } from '@shared/models/conversation.models';

@Injectable({
	providedIn: 'root',
})
export class DisputesService {
	protected apiRoute = `${environment.apiUrl}`;
	constructor(private httpClient: HttpClient) {}

	public getDisputes(): Observable<IDispute[]> {
		return this.httpClient.get<IDispute[]>(`${this.apiRoute}/arguments`);
	}
	public getDisputeMessages(groupChatId: string): Observable<IMessage[]> {
		return this.httpClient.get<IMessage[]>(
			`${this.apiRoute}/group-chats/${groupChatId}/history`
		);
	}
	public getGroupChats(groupChatId: number) {
		return this.httpClient.get<IGroupChat>(
			`${this.apiRoute}/group-chats/${groupChatId}`
		);
	}

	public addModerator(argumentID: number) {
		return this.httpClient.put(
			`${this.apiRoute}/arguments/${argumentID}/asking-for-intervention`,
			{ argumentID }
		);
	}

	public ownerAcceptArgument(argumentID: number) {
		return this.httpClient.put(
			`${this.apiRoute}/arguments/${argumentID}/owner-accept`,
			{ argumentID }
		);
	}

	public studentAcceptArgument(argumentID: number) {
		return this.httpClient.put(
			`${this.apiRoute}/arguments/${argumentID}/student-accept`,
			{ argumentID }
		);
	}
}
