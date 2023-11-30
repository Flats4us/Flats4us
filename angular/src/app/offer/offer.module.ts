import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';

import { AddOfferComponent } from './components/add-offer/add-offer.component';
import { OfferRoutingModule } from './offer-routing.module';
import { WatchedOffersComponent } from './components/watched-offers/watched-offers.component';
import { MatChipsModule } from '@angular/material/chips';
import { OfferService } from './services/offer-service';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
	exports: [AddOfferComponent, WatchedOffersComponent],
	declarations: [AddOfferComponent, WatchedOffersComponent],
	imports: [
		CommonModule,
		OfferRoutingModule,
		MatCardModule,
		MatFormFieldModule,
		ReactiveFormsModule,
		MatInputModule,
		MatDatepickerModule,
		MatNativeDateModule,
		MatSelectModule,
		MatButtonModule,
		MatIconModule,
		MatChipsModule,
		MatTooltipModule,
	],
	providers: [OfferService],
})
export class OfferModule {}
