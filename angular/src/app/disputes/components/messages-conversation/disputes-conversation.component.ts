import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { IConversation } from '@shared/models/conversation.models';
import { map, Observable } from 'rxjs';

@Component({
	selector: 'app-disputes-conversation',
	templateUrl: './disputes-conversation.component.html',
	styleUrls: ['./disputes-conversation.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputesConversationComponent {
	public conversationId$: Observable<string>;
	public messageControl = new FormControl();
	public currentUser = 'User1';

	public messages: IConversation[] = [
		{ sender: 'User1', message: 'Msg' },
		{ sender: 'User2', message: 'Msg..' },
	];

	constructor(public route: ActivatedRoute) {
		this.conversationId$ = route.paramMap.pipe(
			map(params => params.get('id') ?? '')
		);
	}

	public onSend(): void {
		if (!this.messageControl.value) {
			return;
		}

		this.messages.push({
			sender: this.currentUser,
			message: this.messageControl.value,
		});
		this.messageControl.reset();
	}
}
