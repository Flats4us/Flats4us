import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FindRoommateComponent } from './find-roommate.component';
import { MatCardModule } from '@angular/material/card';
import { FindRoommateRoutingModule } from './find-roommate-routing.module';

@NgModule({
	declarations: [FindRoommateComponent],
	imports: [CommonModule, FindRoommateRoutingModule, MatCardModule],
	exports: [FindRoommateComponent],
})
export class FindRoommateModule {}
