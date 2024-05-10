import { ChangeDetectionStrategy, Component } from '@angular/core';
import { IConversations } from '@shared/models/conversation.models';
import { Observable } from 'rxjs';

import { ConversationService } from './services/conversation.service';

@Component({
	selector: 'app-conversations',
	templateUrl: './conversations.component.html',
	styleUrls: ['./conversations.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ConversationsComponent {
	protected conversations$: Observable<IConversations[]> =
		this.conversationService.getConversations();

	constructor(public conversationService: ConversationService) {}
}
