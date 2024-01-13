import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import {
	FormsModule,
	ReactiveFormsModule,
	FormGroupDirective,
} from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatChipsModule } from '@angular/material/chips';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTooltipModule } from '@angular/material/tooltip';
import { SurveyService } from '@shared/services/survey.service';
import { RegisterModule } from 'src/app/auth/components/register/register.module';
import { OfferModule } from 'src/app/offer/offer.module';
import { EmailChangeModule } from 'src/app/settings/components/emailChange/emailChange.module';
import { PasswordChangeModule } from 'src/app/settings/components/password-change/password-change.module';
import { SettingsRoutingModule } from 'src/app/settings/settings-routing.module';
import { ProfileRoutingModule } from '../profile-routing.module';
import { ProfileService } from '../services/profile.service';
import { EditProfileComponent } from './edit-profile.component';

@NgModule({
	declarations: [EditProfileComponent],
	imports: [
		CommonModule,
		ProfileRoutingModule,
		MatCardModule,
		MatButtonModule,
		MatIconModule,
		MatChipsModule,
		SettingsRoutingModule,
		MatSliderModule,
		MatSnackBarModule,
		FormsModule,
		MatListModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatInputModule,
		MatSlideToggleModule,
		HttpClientModule,
		MatCheckboxModule,
		EmailChangeModule,
		PasswordChangeModule,
		MatRadioModule,
		MatSelectModule,
		MatAutocompleteModule,
		MatDatepickerModule,
		MatNativeDateModule,
		MatTooltipModule,
		MatDividerModule,
		MatStepperModule,
		RegisterModule,
		OfferModule,
	],
	exports: [EditProfileComponent],
	providers: [ProfileService, FormGroupDirective, SurveyService],
})
export class EditProfileModule {}
