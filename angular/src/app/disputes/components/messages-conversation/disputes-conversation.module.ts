import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

import { DisputesConversationComponent } from './disputes-conversation.component';

@NgModule({
	declarations: [DisputesConversationComponent],
	imports: [CommonModule, MatInputModule, MatIconModule, ReactiveFormsModule],
})
export class DisputesConversationModule {}
