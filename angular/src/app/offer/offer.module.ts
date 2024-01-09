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
import { SurveyComponent } from '@shared/components/survey/survey.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatListModule } from '@angular/material/list';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { HttpClientModule } from '@angular/common/http';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { SurveyService } from '@shared/services/survey.service';
import { OfferService } from './services/offer.service';

@NgModule({
	exports: [AddOfferComponent, SurveyComponent],
	declarations: [AddOfferComponent, SurveyComponent],
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
		MatSliderModule,
		MatSnackBarModule,
		MatListModule,
		MatSlideToggleModule,
		HttpClientModule,
		MatCheckboxModule,
		MatRadioModule,
	],
	providers: [SurveyService, OfferService],
})
export class OfferModule {}
