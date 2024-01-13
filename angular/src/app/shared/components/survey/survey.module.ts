import { SurveyService } from '@shared/services/survey.service';
import { SurveyComponent } from './survey.component';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { OfferRoutingModule } from 'src/app/offer/offer-routing.module';
import { RentsDetailsModule } from 'src/app/rents/components/details/rents-details.module';
import { RentsCancelDialogComponent } from 'src/app/rents/components/dialog/rents-cancel-dialog/rents-cancel-dialog.component';
import { RentsTenantsDialogComponent } from 'src/app/rents/components/dialog/rents-tenants-dialog/rents-tenants-dialog.component';

@NgModule({
	declarations: [SurveyComponent],
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
	providers: [SurveyService],
	exports: [SurveyComponent],
})
export class SurveyModule {}
