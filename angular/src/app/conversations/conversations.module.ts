import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';

import { MessagesConversationModule } from './components/messages-conversation/messages-conversation.module';
import { ConversationsRoutingModule } from './conversations-routing.module';
import { ConversationsComponent } from './conversations.component';
import { ConversationService } from './services/conversation.service';

@NgModule({
	declarations: [ConversationsComponent],
	imports: [
		CommonModule,
		ConversationsRoutingModule,
		MessagesConversationModule,
		MatIconModule,
		MatButtonModule,
		MatCardModule,
		MatListModule,
	],
	providers: [ConversationService],
})
export class ConversationsModule {}
