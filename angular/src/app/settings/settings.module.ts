import { NgModule } from '@angular/core';
import { SettingsRoutingModule } from './settings-routing.module';
import { StudentSurveyComponent } from './components/student-survey/student-survey.component';
import { MatCardModule } from '@angular/material/card';
import { MatSliderModule } from '@angular/material/slider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpClientModule } from '@angular/common/http';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { EmailChangeModule } from './components/emailChange/emailChange.module';
import { PasswordChangeModule } from './components/passwordChange/passwordChange.module';
import { CommonModule } from '@angular/common';

@NgModule({
	declarations: [StudentSurveyComponent],
	imports: [
		CommonModule,
		SettingsRoutingModule,
		MatCardModule,
		MatSliderModule,
		MatSnackBarModule,
		FormsModule,
		MatListModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatInputModule,
		MatSlideToggleModule,
		HttpClientModule,
		MatButtonModule,
		MatCheckboxModule,
		EmailChangeModule,
		PasswordChangeModule,
	],
})
export class SettingsModule {}
