import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AddRealEstateModule } from './components/add/add-real-estate.module';
import { RealEstateComponent } from './real-estate.component';
import { RealEstateRoutingModule } from './real-estate-routing.module';

@NgModule({
	declarations: [RealEstateComponent],
	imports: [CommonModule, RealEstateRoutingModule, AddRealEstateModule],
	exports: [RealEstateComponent],
})
export class RealEstateModule {}
