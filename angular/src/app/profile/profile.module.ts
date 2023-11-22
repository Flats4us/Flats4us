import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatChipsModule } from '@angular/material/chips';
import { MatIconModule } from '@angular/material/icon';

import { ProfileRoutingModule } from './profile-routing.module';
import { ProfileComponent } from './profile.component';
import { SurveyComponent } from './survey/survey.component';
import { SettingsRoutingModule } from '../settings/settings-routing.module';
import { MatSliderModule } from '@angular/material/slider';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { HttpClientModule } from '@angular/common/http';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { EmailChangeModule } from '../settings/components/emailChange/emailChange.module';
import { PasswordChangeModule } from '../settings/components/passwordChange/passwordChange.module';
import { MatRadioModule } from '@angular/material/radio';
import { ReusableProfileComponent } from './reusable/reusable.component';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { ProfileService } from './services/profile.service';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDividerModule } from '@angular/material/divider';
import { EditOwnerProfileComponent } from './owner/edit/edit-profile.component';
import { CreateOwnerProfileComponent } from './owner/create/create-profile.component';
import { EditStudentProfileComponent } from './student/edit/edit-profile.component';
import { CreateStudentProfileComponent } from './student/create/create-profile.component';

@NgModule({
	declarations: [
		ProfileComponent,
		SurveyComponent,
		ReusableProfileComponent,
		EditOwnerProfileComponent,
		CreateOwnerProfileComponent,
		EditStudentProfileComponent,
		CreateStudentProfileComponent,
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
	],
	exports: [ProfileComponent],
	providers: [ProfileService],
})
export class ProfileModule {}
