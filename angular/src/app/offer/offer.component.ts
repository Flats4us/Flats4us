import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { IOffer } from './models/offer.models';
import { OfferService } from './services/offer.service';
import { userType } from '../profile/models/types';

@Component({
	selector: 'app-offers',
	templateUrl: './offer.component.html',
	styleUrls: ['./offer.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferComponent {
	public offersOptions$: Observable<IOffer[]> = this.offerService.getOffers();

	public uType = userType;

	public user$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);

	constructor(
		public offerService: OfferService,
		private router: Router,
		private route: ActivatedRoute
	) {}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}
}
