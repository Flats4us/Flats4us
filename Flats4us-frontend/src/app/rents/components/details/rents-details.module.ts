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
import { MeetingAddComponent } from '../meeting-add/meeting-add.component';
import { RentRateComponent } from '../rent-rate/rent-rate.component';
import { RentsDetailsComponent } from './rents-details.component';
import { MatChipsModule } from '@angular/material/chips';
import { AccessControlDirective } from '@shared/directives/access-control.directive';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { TranslateModule } from '@ngx-translate/core';
import { StarRatingComponent } from '@shared/components/star-rating/star-rating.component';

@NgModule({
	declarations: [RentsDetailsComponent],
	imports: [
		CommonModule,
		MatButtonModule,
		MatInputModule,
		MatListModule,
		MatDividerModule,
		MatTableModule,
		MatMenuModule,
		MatTooltipModule,
		ScrollingModule,
		MatDialogModule,
		MeetingAddComponent,
		RentRateComponent,
		MatIconModule,
		MatMenuModule,
		MatCardModule,
		CdkTableModule,
		MatChipsModule,
		FormsModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		AccessControlDirective,
		TranslateModule,
		StarRatingComponent,
	],
	exports: [RentsDetailsComponent],
})
export class RentsDetailsModule {}
