import { NgModule } from '@angular/core';
import { SettingsRoutingModule } from './settings-routing.module';
import { CommonModule } from '@angular/common';
import { StudentSurveyComponent } from './components/student-survey/student-survey.component';
import { MatCardModule } from '@angular/material/card';
import { MatSliderModule } from '@angular/material/slider';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatListModule } from '@angular/material/list';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { HttpClientModule } from "@angular/common/http";
import { JsonFormComponent } from './components/json-form/json-form.component';
import { MatButtonModule } from "@angular/material/button";
import { MatCheckboxModule } from "@angular/material/checkbox";

@NgModule({
	declarations: [StudentSurveyComponent, JsonFormComponent],
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
    MatCheckboxModule
  ]
})
export class SettingsModule {}
