import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

import { DisputesConversationComponent } from './disputes-conversation.component';
import { MatMenuModule } from '@angular/material/menu';
import { TranslateModule } from '@ngx-translate/core';
import { MatCardModule } from '@angular/material/card';
import { ConversationService } from '../../../conversations/services/conversation.service';
import { MatButtonModule } from '@angular/material/button';

@NgModule({
	declarations: [DisputesConversationComponent],
	imports: [
		CommonModule,
		MatInputModule,
		MatIconModule,
		ReactiveFormsModule,
		MatMenuModule,
		TranslateModule,
		MatCardModule,
		MatButtonModule,
	],
	providers: [ConversationService],
})
export class DisputesConversationModule {}
