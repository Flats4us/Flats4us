import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StartComponent } from './start.component';
import { StartRoutingModule } from './start-routing.module';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { MatDividerModule } from '@angular/material/divider';
import { GetDescriptionDirective } from './directives/get-description.directive';
import {
	MatPaginatorIntl,
	MatPaginatorModule,
} from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';

@NgModule({
	declarations: [StartComponent, GetDescriptionDirective],
	imports: [
		CommonModule,
		MatFormFieldModule,
		MatSelectModule,
		MatAutocompleteModule,
		MatIconModule,
		MatCardModule,
		MatButtonModule,
		MatTooltipModule,
		ReactiveFormsModule,
		MatInputModule,
		MatSlideToggleModule,
		MatChipsModule,
		MatDividerModule,
		StartRoutingModule,
		MatPaginatorModule,
		MatTableModule,
	],
	exports: [StartComponent],
	providers: [MatPaginatorIntl],
})
export class StartModule {}
