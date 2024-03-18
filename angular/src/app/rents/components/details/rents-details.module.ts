import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MeetingAddComponent } from '../meeting-add/meeting-add.component';
import { RentsCancelDialogComponent } from '../dialog/rents-cancel-dialog/rents-cancel-dialog.component';
import { RentsDetailsComponent } from './rents-details.component';
import { MatCardModule } from '@angular/material/card';
import { CdkTableModule } from '@angular/cdk/table';

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
		MatIconModule,
		MatMenuModule,
		MatCardModule,
		CdkTableModule,
	],
	exports: [RentsDetailsComponent],
})
export class RentsDetailsModule {}
