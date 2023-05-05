import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AddingRealEstateComponent } from './adding-real-estate.component';
import { AddingRealEstateRoutingModule } from './adding-real-estate-routing.module';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
	declarations: [AddingRealEstateComponent],
	imports: [
		CommonModule,
		AddingRealEstateRoutingModule,
		MatCardModule,
		MatInputModule,
		MatSelectModule,
		MatIconModule,
	],
})
export class AddingRealEstateModule {}
