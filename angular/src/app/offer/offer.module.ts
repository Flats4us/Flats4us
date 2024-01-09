import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

import { AddOfferComponent } from './components/add-offer/add-offer.component';
import { OfferComponent } from './offer.component';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { OfferDetailsComponent } from './components/details/offer-details.component';
import { OfferRoutingModule } from './offer-routing.module';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RentsDetailsModule } from '../rents/components/details/rents-details.module';
import { RentsCancelDialogComponent } from '../rents/components/dialog/rents-cancel-dialog.component';
import { RentsTenantsDialogComponent } from '../rents/components/dialog/rents-tenants-dialog.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { OfferService } from './services/offer.service';
import { RealEstateService } from '../real-estate/services/real-estate.service';
import { RentsService } from '../rents/services/rents.service';
import { SurveyComponent } from '@shared/components/survey/survey.component';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { HttpClientModule } from '@angular/common/http';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';

@NgModule({
	declarations: [
		AddOfferComponent,
		OfferComponent,
		OfferDetailsComponent,
		SurveyComponent,
	],
	imports: [
		CommonModule,
		OfferRoutingModule,
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
		MatDatepickerModule,
		MatCardModule,
		MatSelectModule,
		RentsCancelDialogComponent,
		RentsTenantsDialogComponent,
		RentsDetailsModule,
		MatChipsModule,
		MatAutocompleteModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatNativeDateModule,
		MatSliderModule,
		MatSnackBarModule,
		MatSlideToggleModule,
		HttpClientModule,
		MatCheckboxModule,
		MatRadioModule,
	],
	providers: [OfferService, RealEstateService, RentsService],
	exports: [
		OfferComponent,
		OfferDetailsComponent,
		AddOfferComponent,
		SurveyComponent,
	],
	providers: [OfferService, RealEstateService, RentsService],
})
export class OfferModule {}
