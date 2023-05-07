import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';
import { GalleryModule } from 'ng-gallery';
import { MatSliderModule } from '@angular/material/slider';

import { OfferDetailsRoutingModule } from './offer-details-routing.module';
import { OfferDetailsComponent } from './offer-details.component';

@NgModule({
	declarations: [OfferDetailsComponent],
	imports: [
		CommonModule,
		OfferDetailsRoutingModule,
		MatCardModule,
		MatButtonModule,
		MatIconModule,
		MatChipsModule,
		GalleryModule,
		MatSliderModule,
	],
	exports: [OfferDetailsComponent],
})
export class OfferDetailsModule {}
