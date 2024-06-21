import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { OfferService } from './services/offer.service';
import { UserType } from '../profile/models/types';
import { ISendOffers } from './models/offer.models';
import { RealEstateService } from '../real-estate/services/real-estate.service';
import { AuthService } from '@shared/services/auth.service';
import { AuthModels } from '@shared/models/auth.models';

@Component({
	selector: 'app-offers',
	templateUrl: './offer.component.html',
	styleUrls: ['./offer.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferComponent {
	public offersOptions$: Observable<ISendOffers> | undefined;

	public uType = UserType;

	public user$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);

	public authModels = AuthModels;

	constructor(
		public offerService: OfferService,
		private router: Router,
		private route: ActivatedRoute,
		public realEstateService: RealEstateService,
		public authService: AuthService
	) {
		if (this.authService.getUserType() !== AuthModels.MODERATOR) {
			this.offersOptions$ = this.offerService.getOffers();
		}
	}

	public addOffer() {
		this.router.navigate(['offer', 'add']);
	}

	public onDeactivate() {
		this.offersOptions$ = this.offerService.getOffers();
	}
}
