import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddOfferComponent } from './add-offer/add-offer.component';
import { OfferRoutingModule } from './offer-routing.module';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

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
		MatDatepickerModule,
		MatNativeDateModule,
		MatSelectModule,
		MatButtonModule,
		MatIconModule,
	],
})
export class OfferModule {}
