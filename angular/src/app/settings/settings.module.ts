import { NgModule } from '@angular/core';
import { SettingsRoutingModule } from './settings-routing.module';
import { EmailChangeModule } from './components/emailChange/emailChange.module';
import { PasswordChangeModule } from './components/passwordChange/passwordChange.module';
import { CommonModule } from '@angular/common';
import { StudentSurveyComponent } from './components/student-survey/student-survey.component';
import { MatCardModule } from '@angular/material/card';
import { MatSliderModule } from '@angular/material/slider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

@NgModule({
	declarations: [StudentSurveyComponent],
	imports: [
		CommonModule,
		SettingsRoutingModule,
		EmailChangeModule,
		PasswordChangeModule,
		MatCardModule,
		MatSliderModule,
		FormsModule,
		MatListModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatInputModule,
		MatSlideToggleModule,
	],
})
export class SettingsModule {}
