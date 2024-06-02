import { ScrollingModule } from '@angular/cdk/scrolling';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { AddOfferComponent } from './add-offer.component';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { OfferRoutingModule } from '../../offer-routing.module';
import { OfferService } from '../../services/offer.service';
import { SurveyService } from '@shared/services/survey.service';
import { SurveyModule } from '@shared/components/survey/survey.module';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
	declarations: [AddOfferComponent],
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
		OfferRoutingModule,
		MatDatepickerModule,
		MatCardModule,
		MatSelectModule,
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
		SurveyModule,
		TranslateModule,
	],
	exports: [AddOfferComponent],
	providers: [OfferService, SurveyService],
})
export class AddOfferModule {}
