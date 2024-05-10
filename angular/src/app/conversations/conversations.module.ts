import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

import { MessagesConversationModule } from './components/messages-conversation/messages-conversation.module';
import { ConversationsRoutingModule } from './conversations-routing.module';
import { MatCardModule } from '@angular/material/card';
import { ConversationService } from '@shared/services/conversation.service';
import { ConversationsComponent } from './conversations.component';
import { MatListModule } from '@angular/material/list';

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
