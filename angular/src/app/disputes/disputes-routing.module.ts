import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DisputesConversationComponent } from '@shared/components/disputes-conversation/disputes-conversation.component';
import { DisputesComponent } from './disputes.component';

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
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class DisputesRoutingModule {}
