import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';

import { DisputesConversationModule } from './components/messages-conversation/disputes-conversation.module';
import { DisputesRoutingModule } from './disputes-routing.module';
import { DisputesComponent } from './disputes.component';
import { TranslateModule } from '@ngx-translate/core';
import { MatCardModule } from '@angular/material/card';

@NgModule({
	declarations: [DisputesComponent],
	imports: [
		CommonModule,
		DisputesRoutingModule,
		DisputesConversationModule,
		MatIconModule,
		MatButtonModule,
		MatTabsModule,
		TranslateModule,
		MatCardModule,
	],
})
export class DisputesModule {}
