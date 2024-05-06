import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { StartMapComponent } from './start-map.component';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { StartService } from '../services/start.service';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { OfferService } from 'src/app/offer/services/offer.service';

@NgModule({
	declarations: [StartMapComponent],
	imports: [
		CommonModule,
		FormsModule,
		ReactiveFormsModule,
		MatIconModule,
		MatTooltipModule,
	],
	exports: [StartMapComponent],
	providers: [RealEstateService, StartService, RealEstateService, OfferService],
})
export class StartMapModule {}
