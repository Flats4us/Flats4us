import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

import { MessagesConversationComponent } from './messages-conversation.component';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
	declarations: [MessagesConversationComponent],
	imports: [
		CommonModule,
		MatInputModule,
		MatIconModule,
		ReactiveFormsModule,
		MatCardModule,
		MatButtonModule,
		TranslateModule,
	],
})
export class MessagesConversationModule {}
