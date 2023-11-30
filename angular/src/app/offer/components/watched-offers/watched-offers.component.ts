import { ChangeDetectionStrategy, Component } from '@angular/core';
import { OfferService } from 'src/app/offer/services/offer-service';
import { Observable, of, switchMap } from 'rxjs';
import { Router } from '@angular/router';
import { IWatchedOffer } from '../../models/offer-models';

@Component({
	selector: 'app-watched-offers',
	templateUrl: './watched-offers.component.html',
	styleUrls: ['./watched-offers.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class WatchedOffersComponent {
	public watchedOffers$?: Observable<IWatchedOffer[]>;

	constructor(public offerService: OfferService, private router: Router) {
		this.watchedOffers$ = offerService.getOffers();
	}

	public navigateToFlat(url: string) {
		this.router.navigate([url]);
	}
	public deleteOffer(id: string) {
		this.watchedOffers$ = this.watchedOffers$?.pipe(
			switchMap(offers => of(offers.filter(offer => offer.id !== id)))
		);
	}
}
