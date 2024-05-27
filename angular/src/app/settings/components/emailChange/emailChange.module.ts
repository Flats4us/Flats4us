import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EmailChangeComponent } from './emailChange.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatStepperModule } from '@angular/material/stepper';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { RouterLink } from '@angular/router';
import { TranslateModule } from '@ngx-translate/core';
@NgModule({
	declarations: [EmailChangeComponent],
	imports: [
		CommonModule,
		MatFormFieldModule,
		MatDividerModule,
		MatButtonModule,
		MatListModule,
		MatIconModule,
		MatInputModule,
		MatCardModule,
		MatStepperModule,
		ReactiveFormsModule,
		MatSnackBarModule,
		RouterLink,
		TranslateModule,
	],
	exports: [EmailChangeComponent],
})
export class EmailChangeModule {}
