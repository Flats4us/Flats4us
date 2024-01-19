import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { StartMapComponent } from './start-map.component';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { StartService } from '../services/start.service';

@NgModule({
	declarations: [StartMapComponent],
	imports: [CommonModule, FormsModule, ReactiveFormsModule],
	providers: [RealEstateService, StartService, RealEstateService],
})
export class StartMapModule {}
