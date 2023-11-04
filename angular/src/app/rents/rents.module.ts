import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RentsComponent } from './rents.component';
import { RentsRoutingModule } from './rents-routing.module';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { RentsService } from './services/rents.service';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatDividerModule } from '@angular/material/divider';
import { MatTableModule } from '@angular/material/table';
import { MatMenuModule } from '@angular/material/menu';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatDialogModule } from '@angular/material/dialog';
import { RentsDialogComponent } from './components/dialog/rents-dialog.component';
import { RentsDetailsModule } from './components/details/rents-details.module';

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
		MatMenuModule,
		MatTooltipModule,
		ScrollingModule,
		MatDialogModule,
		RentsDialogComponent,
		RentsDetailsModule,
	],
	exports: [RentsComponent],
	providers: [RentsService],
})
export class RentsModule {}
