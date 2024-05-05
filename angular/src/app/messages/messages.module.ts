import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { MessagesConversationModule } from './components/messages-conversation/messages-conversation.module';
import { MessagesRoutingModule } from './messages-routing.module';
import { MessagesComponent } from './messages.component';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
	declarations: [MessagesComponent],
	imports: [
		CommonModule,
		MessagesRoutingModule,
		MessagesConversationModule,
		MatIconModule,
		MatButtonModule,
		TranslateModule,
	],
})
export class MessagesModule {}
