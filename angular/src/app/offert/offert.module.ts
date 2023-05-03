import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddOffertComponent } from './add-offert/add-offert.component';
import { OffertRoutingModule } from '../offert-routing.module';
import { MatCardModule } from '@angular/material/card';

@NgModule({
	declarations: [AddOffertComponent],
	imports: [CommonModule, OffertRoutingModule, MatCardModule],
})
export class OffertModule {}
