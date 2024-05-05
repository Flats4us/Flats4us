import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WatchedOffersComponent } from './watched-offers.component';
import {
	MatPaginatorIntl,
	MatPaginatorModule,
} from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';
import { MatCardModule } from '@angular/material/card';
import { OfferRoutingModule } from '../../offer-routing.module';
import { OfferService } from '../../services/offer.service';
import { MatTooltipModule } from '@angular/material/tooltip';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
	declarations: [WatchedOffersComponent],
	imports: [
		CommonModule,
		MatPaginatorModule,
		MatButtonModule,
		MatIconModule,
		MatChipsModule,
		MatCardModule,
		OfferRoutingModule,
		MatTooltipModule,
		TranslateModule,
	],
	providers: [OfferService, RealEstateService, MatPaginatorIntl],
	exports: [WatchedOffersComponent],
})
export class WatchedOffersModule {}
