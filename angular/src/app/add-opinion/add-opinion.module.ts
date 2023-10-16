import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddOpinionRoutingModule } from './add-opinion-routing.module';
import { AddOpinionComponent } from './add-opinion.component';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { FormsModule } from '@angular/forms';

@NgModule({
	declarations: [AddOpinionComponent],
	imports: [
		CommonModule,
		AddOpinionRoutingModule,
		MatCardModule,
		MatIconModule,
		FormsModule,
	],
	exports: [AddOpinionComponent],
})
export class AddOpinionModule {}
