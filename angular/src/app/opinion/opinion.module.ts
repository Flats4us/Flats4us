import { NgModule } from '@angular/core';
import { CommonModule, NgOptimizedImage } from '@angular/common';

import { OpinionRoutingModule } from './opinion-routing.module';
import { AddOpinionComponent } from './add-opinion/add-opinion.component';
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
		OpinionRoutingModule,
		MatCardModule,
		MatIconModule,
		FormsModule,
		MatButtonModule,
		MatCheckboxModule,
		MatInputModule,
		MatListModule,
		NgOptimizedImage,
		ReactiveFormsModule,
		MatSnackBarModule,
	],
})
export class OpinionModule {}
