import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { MatIconModule } from '@angular/material/icon';
import { OfferService } from 'src/app/offer/services/offer.service';
import { OfferMapComponent } from './offer-map.component';

@NgModule({
	declarations: [OfferMapComponent],
	imports: [CommonModule, MatIconModule],
	exports: [OfferMapComponent],
	providers: [RealEstateService, OfferService],
})
export class OfferMapModule {}
