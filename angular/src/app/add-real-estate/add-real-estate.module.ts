import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddRealEstateRoutingModule } from './add-real-estate-routing.module';
import { AddRealEstateComponent } from './add-real-estate.component';
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
import { MatStepperModule } from '@angular/material/stepper';
import { STEPPER_GLOBAL_OPTIONS } from '@angular/cdk/stepper';

@NgModule({
	declarations: [AddRealEstateComponent],
	imports: [
		CommonModule,
		AddRealEstateRoutingModule,
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
		MatStepperModule,
	],
	exports: [AddRealEstateComponent],
	providers: [
		{ provide: STEPPER_GLOBAL_OPTIONS, useValue: { showError: true } },
	],
})
export class AddRealEstateModule {}
