import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MessagesConversationComponent } from './components/messages-conversation/messages-conversation.component';
import { ConversationsComponent } from './conversations.component';

const routes: Routes = [
	{
		path: '',
		component: ConversationsComponent,
		children: [
			{ path: ':conversationId', component: MessagesConversationComponent },
			{
				path: 'new/receiver/:receiverId',
				component: MessagesConversationComponent,
			},
		],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ConversationsRoutingModule {}
