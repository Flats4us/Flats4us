import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentsComponent } from './rents.component';
import { RentsRoutingModule } from './rents-routing.module';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { NgImageSliderModule } from 'ng-image-slider';
import { RentsService } from './services/rents.service';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { MatTableModule } from '@angular/material/table';

@NgModule({
	declarations: [RentsComponent],
	imports: [
		CommonModule,
		RentsRoutingModule,
		MatButtonModule,
		MatIconModule,
		MatInputModule,
		MatListModule,
		MatDividerModule,
		MatTableModule,
		NgImageSliderModule,
	],
	exports: [RentsComponent],
	providers: [RentsService],
})
export class RentsModule {}
