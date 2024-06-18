import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';

import { DisputesConversationModule } from '@shared/components/disputes-conversation/disputes-conversation.module';
import { DisputesRoutingModule } from './disputes-routing.module';
import { DisputesComponent } from './disputes.component';
import { TranslateModule } from '@ngx-translate/core';
import { MatCardModule } from '@angular/material/card';
import { MatListModule } from '@angular/material/list';

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
		MatListModule,
	],
})
export class DisputesModule {}
