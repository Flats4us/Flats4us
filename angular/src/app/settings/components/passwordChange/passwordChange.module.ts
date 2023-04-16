import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PasswordChangeComponent } from './passwordChange.component';
import { PasswordChangeRoutingModule } from './passwordChange-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDividerModule } from '@angular/material/divider';
import { MatButtonModule } from '@angular/material/button';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatStepperModule } from '@angular/material/stepper';
import { ReactiveFormsModule } from '@angular/forms';
@NgModule({
	declarations: [PasswordChangeComponent],
	imports: [
		CommonModule,
		PasswordChangeRoutingModule,
		MatFormFieldModule,
		MatDividerModule,
		MatButtonModule,
		MatListModule,
		MatIconModule,
		MatInputModule,
		MatCardModule,
		MatStepperModule,
		ReactiveFormsModule,
	],
	exports: [PasswordChangeComponent],
})
export class PasswordChangeModule {}
