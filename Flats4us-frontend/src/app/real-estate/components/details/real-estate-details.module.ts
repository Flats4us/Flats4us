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
import { RealEstateDialogComponent } from '../dialog/real-estate-dialog.component';
import { RealEstateDetailsComponent } from './real-estate-details.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { OfferService } from 'src/app/offer/services/offer.service';
import { TranslateModule } from '@ngx-translate/core';
import { StarRatingComponent } from '@shared/components/star-rating/star-rating.component';

@NgModule({
	declarations: [RealEstateDetailsComponent],
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
		MatIconModule,
		MatMenuModule,
		RealEstateDialogComponent,
		MatChipsModule,
		MatCardModule,
		TranslateModule,
		StarRatingComponent,
	],
	exports: [RealEstateDetailsComponent],
	providers: [OfferService],
})
export class RealEstateDetailsModule {}
