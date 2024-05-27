import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatTableModule } from '@angular/material/table';
import { MatTooltipModule } from '@angular/material/tooltip';
import { OfferDetailsComponent } from './offer-details.component';
import { MatChipsModule } from '@angular/material/chips';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { OfferService } from '../../services/offer.service';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { AccessControlDirective } from '@shared/directives/access-control.directive';
import { RentsService } from 'src/app/rents/services/rents.service';
import { StartService } from 'src/app/start/services/start.service';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
	declarations: [OfferDetailsComponent],
	imports: [
		CommonModule,
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
		MatMenuModule,
		MatChipsModule,
		FormsModule,
		ReactiveFormsModule,
		MatFormFieldModule,
		MatCardModule,
		AccessControlDirective,
		MatSnackBarModule,
		TranslateModule,
	],
	providers: [OfferService, RealEstateService, RentsService, StartService],
	exports: [OfferDetailsComponent],
})
export class OfferDetailsModule {}
