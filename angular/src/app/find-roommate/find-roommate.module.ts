import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FindRoommateComponent } from './find-roommate.component';
import { MatCardModule } from '@angular/material/card';
import { FindRoommateRoutingModule } from './find-roommate-routing.module';
import { ConversationsComponent } from './components/conversations/conversations.component';
import { RoommateCandidateComponent } from './components/roommate-candidate/roommate-candidate.component';

@NgModule({
	declarations: [
		FindRoommateComponent,
		ConversationsComponent,
		RoommateCandidateComponent,
	],
	imports: [CommonModule, FindRoommateRoutingModule, MatCardModule],
	exports: [FindRoommateComponent],
})
export class FindRoommateModule {}
