import { ScrollingModule } from '@angular/cdk/scrolling';
import { CdkTableModule } from '@angular/cdk/table';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';

import { RentsCancelDialogComponent } from '../dialog/rents-cancel-dialog/rents-cancel-dialog.component';
import { MeetingAddComponent } from '../meeting-add/meeting-add.component';
import { RentRateComponent } from '../rent-rate/rent-rate.component';
import { RentsDetailsComponent } from './rents-details.component';
import { MatChipsModule } from '@angular/material/chips';

@NgModule({
	declarations: [RentsDetailsComponent],
	imports: [
		CommonModule,
		MatButtonModule,
		MatIconModule,
		MatInputModule,
		MatListModule,
		MatDividerModule,
		MatTableModule,
		MatMenuModule,
		MatTooltipModule,
		ScrollingModule,
		MatDialogModule,
		RentsCancelDialogComponent,
		MeetingAddComponent,
		RentRateComponent,
		MatIconModule,
		MatMenuModule,
		MatCardModule,
		CdkTableModule,
		MatChipsModule,
	],
	exports: [RentsDetailsComponent],
})
export class RentsDetailsModule {}
