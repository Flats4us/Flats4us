import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { UserService } from '@shared/services/user.service';

import { RegisterModule } from '../auth/components/register/register.module';
import { PasswordChangeModule } from '../settings/components/password-change/password-change.module';
import { SettingsRoutingModule } from '../settings/settings-routing.module';
import { AddOpinionComponent } from './add-opinion/add-opinion.component';
import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileComponent } from './profile.component';
import { StarRatingComponent } from '@shared/components/star-rating/star-rating.component';
import { TranslateModule } from '@ngx-translate/core';
import { EmailChangeModule } from '../settings/components/email-change/email-change.module';
import { EditProfileModule } from './edit/edit-profile.module';
import { CreateProfileModule } from './create/create-profile.module';
import { AccessControlDirective } from '@shared/directives/access-control.directive';
import { AuthService } from '@shared/services/auth.service';

@NgModule({
	declarations: [ProfileComponent],
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
		EditProfileModule,
		CreateProfileModule,
		AddOpinionComponent,
		MatChipsModule,
		StarRatingComponent,
		TranslateModule,
		AccessControlDirective,
	],
	exports: [ProfileComponent],
	providers: [SurveyService, UserService, AuthService],
})
export class ProfileModule {}
