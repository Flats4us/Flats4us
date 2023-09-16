import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FindRoommateComponent } from './find-roommate.component';
import { MatCardModule } from '@angular/material/card';
import { FindRoommateRoutingModule } from './find-roommate-routing.module';
import { ConversationsComponent } from './components/conversations/conversations.component';
import { RoommateCandidateComponent } from './components/roommate-candidate/roommate-candidate.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatSliderModule } from '@angular/material/slider';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatInputModule } from '@angular/material/input';

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
		MatSliderModule,
		MatIconModule,
		MatButtonModule,
		MatTooltipModule,
		MatInputModule,
	],
	exports: [FindRoommateComponent],
})
export class FindRoommateModule {}
