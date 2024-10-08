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
import { PasswordChangeModule } from 'src/app/settings/components/password-change/password-change.module';
import { SettingsRoutingModule } from 'src/app/settings/settings-routing.module';
import { ProfileRoutingModule } from '../profile-routing.module';
import { EditProfileComponent } from './edit-profile.component';
import { MatMenuModule } from '@angular/material/menu';
import { TranslateModule } from '@ngx-translate/core';
import { UserService } from '@shared/services/user.service';
import { EmailChangeModule } from 'src/app/settings/components/email-change/email-change.module';
import { AuthService } from '@shared/services/auth.service';

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
		MatMenuModule,
		TranslateModule,
	],
	exports: [EditProfileComponent],
	providers: [FormGroupDirective, SurveyService, UserService, AuthService],
})
export class EditProfileModule {}
