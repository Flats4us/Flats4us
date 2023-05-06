import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainSiteComponent } from './main-site.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';

@NgModule({
	declarations: [MainSiteComponent],
	imports: [
		CommonModule,
		MatFormFieldModule,
		MatSelectModule,
		MatAutocompleteModule,
		MatIconModule,
		MatCardModule,
		MatButtonModule,
		MatTooltipModule,
		FormsModule,
		ReactiveFormsModule,
		MatInputModule,
		MatSlideToggleModule,
		MatChipsModule,
		MatDividerModule,
	],
	exports: [MainSiteComponent],
})
export class MainSiteModule {}
