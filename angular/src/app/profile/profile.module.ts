import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';

import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileComponent } from './profile.component';
import { SurveyComponent } from '../shared/components/survey/survey.component';
import { SettingsRoutingModule } from '../settings/settings-routing.module';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import {
	FormGroupDirective,
	FormsModule,
	ReactiveFormsModule,
} from '@angular/forms';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { HttpClientModule } from '@angular/common/http';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { EmailChangeModule } from '../settings/components/emailChange/emailChange.module';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { ProfileService } from './services/profile.service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDividerModule } from '@angular/material/divider';
import { EditProfileComponent } from './edit/edit-profile.component';
import { CreateProfileComponent } from './create/create-profile.component';
import { MatStepperModule } from '@angular/material/stepper';
import { PasswordChangeModule } from '../settings/components/password-change/password-change.module';
import { RegisterComponent } from '../auth/components/register/register.component';
import { SurveyService } from '@shared/services/survey.service';

@NgModule({
	declarations: [
		ProfileComponent,
		SurveyComponent,
		EditProfileComponent,
		CreateProfileComponent,
		RegisterComponent,
	],
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
	],
	exports: [ProfileComponent],
	providers: [ProfileService, FormGroupDirective, SurveyService],
})
export class ProfileModule {}
