import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { OfferService } from './services/offer.service';
import { userType } from '../profile/models/types';
import { ISendOffers } from './models/offer.models';
import { RealEstateService } from '../real-estate/services/real-estate.service';

@Component({
	selector: 'app-offers',
	templateUrl: './offer.component.html',
	styleUrls: ['./offer.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferComponent implements OnInit {
	public offersOptions$?: Observable<ISendOffers>;

	public uType = userType;

	public user$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);

	constructor(
		public offerService: OfferService,
		private router: Router,
		private route: ActivatedRoute,
		public realEstateService: RealEstateService
	) {}
	public ngOnInit(): void {
		this.offersOptions$ = this.offerService.getOffers();
	}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}
}
