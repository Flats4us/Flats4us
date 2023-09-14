import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FindRoommateComponent } from './find-roommate.component';
import { MatCardModule } from '@angular/material/card';
import { FindRoommateRoutingModule } from './find-roommate-routing.module';
import { ConversationsComponent } from './components/conversations/conversations.component';
import { RoommateCandidateComponent } from './components/roommate-candidate/roommate-candidate.component';
import { MatChipsModule } from '@angular/material/chips';

@NgModule({
	declarations: [
		FindRoommateComponent,
		ConversationsComponent,
		RoommateCandidateComponent,
	],
	imports: [
		CommonModule,
		FindRoommateRoutingModule,
		MatCardModule,
		MatChipsModule,
	],
	exports: [FindRoommateComponent],
})
export class FindRoommateModule {}
