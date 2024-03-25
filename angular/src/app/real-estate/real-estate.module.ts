import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AddRealEstateModule } from './components/add/add-real-estate.module';
import { RealEstateComponent } from './real-estate.component';
import { RealEstateRoutingModule } from './real-estate-routing.module';
import { RealEstateService } from './services/real-estate.service';

@NgModule({
	declarations: [RealEstateComponent],
	imports: [CommonModule, RealEstateRoutingModule, AddRealEstateModule],
	exports: [RealEstateComponent],
	providers: [RealEstateService],
})
export class RealEstateModule {}
