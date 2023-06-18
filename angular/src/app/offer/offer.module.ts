import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddOfferComponent } from './add-offer/add-offer.component';
import { OfferRoutingModule } from './offer-routing.module';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

@NgModule({
	exports: [AddOfferComponent],
	declarations: [AddOfferComponent],
	imports: [
		CommonModule,
		OfferRoutingModule,
		MatCardModule,
		MatFormFieldModule,
		ReactiveFormsModule,
		MatInputModule,
	],
})
export class OfferModule {}
