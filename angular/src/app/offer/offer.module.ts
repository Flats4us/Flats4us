import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { OfferComponent } from './offer.component';
import { MatDividerModule } from '@angular/material/divider';
import { MatListModule } from '@angular/material/list';
import { OfferRoutingModule } from './offer-routing.module';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RentsDetailsModule } from '../rents/components/details/rents-details.module';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { OfferService } from './services/offer.service';
import { RealEstateService } from '../real-estate/services/real-estate.service';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { HttpClientModule } from '@angular/common/http';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { SurveyService } from '@shared/services/survey.service';
import { AddOfferModule } from './components/add-offer/add-offer.module';
import { OfferDetailsModule } from './components/details/offer-details.module';
import { MeetingAddComponent } from '../rents/components/meeting-add/meeting-add.component';
import { OfferPromotionDialogComponent } from './components/dialog/offer-promotion-dialog/offer-promotion-dialog.component';
import { RentsService } from '../rents/services/rents.service';
import { OfferDetailsComponent } from './components/details/offer-details.component';
import { AddOfferComponent } from './components/add-offer/add-offer.component';
import { WatchedOffersComponent } from './components/watched-offers/watched-offers.component';
import { WatchedOffersModule } from './components/watched-offers/watched-offers.module';

@NgModule({
	declarations: [OfferComponent],
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
		AddOfferModule,
		OfferDetailsModule,
		MeetingAddComponent,
		OfferPromotionDialogComponent,
		AddOfferModule,
		OfferDetailsModule,
		WatchedOffersModule,
	],
	providers: [OfferService, RealEstateService, RentsService, SurveyService],
	exports: [
		OfferComponent,
		OfferDetailsComponent,
		AddOfferComponent,
		WatchedOffersComponent,
	],
})
export class OfferModule {}
