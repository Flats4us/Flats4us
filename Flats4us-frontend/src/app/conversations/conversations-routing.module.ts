import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MessagesConversationComponent } from './components/messages-conversation/messages-conversation.component';
import { ConversationsComponent } from './conversations.component';
import { PermissionsGuard } from '@shared/services/permissions.guard';
import { AuthModels } from '@shared/models/auth.models';

const routes: Routes = [
	{
		path: '',
		component: ConversationsComponent,
		children: [
			{
				path: 'receiver/:receiverId/conversation/:conversationId',
				component: MessagesConversationComponent,
			},
			{
				path: 'receiver/:receiverId/conversation/new',
				component: MessagesConversationComponent,
			},
		],
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [
				AuthModels.UNVERIFIED_STUDENT,
				AuthModels.UNVERIFIED_OWNER,
				AuthModels.VERIFIED_OWNER,
				AuthModels.VERIFIED_STUDENT,
				AuthModels.MODERATOR,
			],
		},
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class ConversationsRoutingModule {}
