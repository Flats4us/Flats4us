import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DisputesConversationComponent } from '@shared/components/disputes-conversation/disputes-conversation.component';
import { DisputesComponent } from './disputes.component';
import { PermissionsGuard } from '@shared/services/permissions.guard';
import { AuthModels } from '@shared/models/auth.models';

const routes: Routes = [
	{
		path: '',
		component: DisputesComponent,
		children: [
			{
				path: 'conversation/:conversationId',
				component: DisputesConversationComponent,
			},
		],
		canActivate: [PermissionsGuard],
		data: {
			requiredPermissions: [
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
export class DisputesRoutingModule {}
