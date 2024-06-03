import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { StartComponent } from './start.component';
import { StartRoutingModule } from './start-routing.module';
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
import { MatDividerModule } from '@angular/material/divider';

import {
	MatPaginatorIntl,
	MatPaginatorModule,
} from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { RealEstateService } from '../real-estate/services/real-estate.service';
import { StartService } from './services/start.service';
import { MatSortModule } from '@angular/material/sort';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { StartMapModule } from './start-map/start-map.module';
import { GetDescriptionDirective } from './directives/get-description.directive';
import { AccessControlDirective } from '@shared/directives/access-control.directive';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { StarRatingComponent } from '@shared/components/star-rating/star-rating.component';
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
	declarations: [StartComponent],
	exports: [StartComponent],
	providers: [MatPaginatorIntl, RealEstateService, StartService],
	imports: [
		CommonModule,
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
		MatDividerModule,
		StartRoutingModule,
		MatPaginatorModule,
		MatTableModule,
		MatSortModule,
		MatMenuModule,
		MatProgressSpinnerModule,
		StartMapModule,
		GetDescriptionDirective,
		AccessControlDirective,
		MatSnackBarModule,
		StarRatingComponent,
		MatDialogModule,
	],
})
export class StartModule {}
