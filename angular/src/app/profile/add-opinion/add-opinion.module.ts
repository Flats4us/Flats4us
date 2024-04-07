import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddOpinionComponent } from './add-opinion.component';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
	exports: [AddOpinionComponent],
	declarations: [AddOpinionComponent],
	imports: [
		CommonModule,
		MatCardModule,
		MatIconModule,
		FormsModule,
		MatButtonModule,
		MatCheckboxModule,
		MatInputModule,
		MatListModule,
		ReactiveFormsModule,
		MatSnackBarModule,
	],
})
export class AddOpinionModule {}
