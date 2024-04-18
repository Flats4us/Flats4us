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
import { RentsDetailsModule } from './components/details/rents-details.module';
import { RentsDetailsComponent } from './components/details/rents-details.component';
import { MatChipsModule } from '@angular/material/chips';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { RealEstateService } from '../real-estate/services/real-estate.service';
import { OfferService } from '../offer/services/offer.service';
import { MatCardModule } from '@angular/material/card';
import { TranslateModule } from '@ngx-translate/core';

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
		RentsDetailsModule,
		MatChipsModule,
		MatAutocompleteModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatCardModule,
		TranslateModule,
	],
	exports: [RentsComponent, RentsDetailsComponent],
	providers: [RentsService, RealEstateService, OfferService],
})
export class RentsModule {}
