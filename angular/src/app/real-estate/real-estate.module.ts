import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AddRealEstateModule } from './components/add/add-real-estate.module';
import { RealEstateComponent } from './real-estate.component';
import { RealEstateRoutingModule } from './real-estate-routing.module';
import { RealEstateService } from './services/real-estate.service';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { ReactiveFormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RentsDetailsModule } from '../rents/components/details/rents-details.module';
import { RentsCancelDialogComponent } from '../rents/components/dialog/rents-cancel-dialog.component';
import { RentsTenantsDialogComponent } from '../rents/components/dialog/rents-tenants-dialog.component';
import { RealEstateDetailsComponent } from './components/details/real-estate-details.component';
import { MatSnackBarModule } from '@angular/material/snack-bar';

@NgModule({
	declarations: [RealEstateComponent, RealEstateDetailsComponent],
	imports: [
		CommonModule,
		RealEstateRoutingModule,
		AddRealEstateModule,
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
		RentsCancelDialogComponent,
		RentsTenantsDialogComponent,
		RentsDetailsModule,
		MatChipsModule,
		MatAutocompleteModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatSnackBarModule,
	],
	exports: [RealEstateComponent, RealEstateDetailsComponent],
	providers: [RealEstateService],
})
export class RealEstateModule {}
