import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, map, tap } from 'rxjs';

@Component({
	selector: 'app-messages-conversation',
	templateUrl: './messages-conversation.component.html',
	styleUrls: ['./messages-conversation.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MessagesConversationComponent {
	public conversationId$: Observable<string>;
	public messageControl = new FormControl();

	public messages: string[] = [];

	constructor(public route: ActivatedRoute) {
		this.conversationId$ = route.paramMap.pipe(
			map(params => params.get('id') ?? ''),
			tap(() => (this.messages.length = 0))
		);
	}

	public onSend(): void {
		if (!this.messageControl.value) {
			return;
		}

		this.messages.push(this.messageControl.value);
		this.messageControl.reset();
	}
}
