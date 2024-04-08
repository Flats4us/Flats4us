import { ChangeDetectionStrategy, Component } from '@angular/core';
import { IConversations } from '@shared/models/conversation.models';
import { Observable } from 'rxjs';
import { ConversationService } from '@shared/services/conversation.service';

@Component({
	selector: 'app-messages',
	templateUrl: './conversations.component.html',
	styleUrls: ['./conversations.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ConversationsComponent {
	protected conversations$: Observable<IConversations[]> =
		this.conversationService.getConversations();

	constructor(public conversationService: ConversationService) {}
}
