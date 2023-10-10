import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MessagesConversationComponent } from './components/messages-conversation/messages-conversation.component';
import { MessagesComponent } from './messages.component';

const routes: Routes = [
	{
		path: '',
		component: MessagesComponent,
		children: [{ path: ':id', component: MessagesConversationComponent }],
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule],
})
export class MessagesRoutingModule {}
