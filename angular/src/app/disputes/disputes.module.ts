import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';

import { DisputesConversationModule } from '@shared/components/disputes-conversation/disputes-conversation.module';
import { DisputesRoutingModule } from './disputes-routing.module';
import { DisputesComponent } from './disputes.component';

@NgModule({
	declarations: [DisputesComponent],
	imports: [
		CommonModule,
		DisputesRoutingModule,
		DisputesConversationModule,
		MatIconModule,
		MatButtonModule,
		MatTabsModule,
	],
})
export class DisputesModule {}
