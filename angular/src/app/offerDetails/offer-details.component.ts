import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, Observable } from 'rxjs';

@Component({
	selector: 'app-offer-details',
	templateUrl: './offer-details.component.html',
	styleUrls: ['./offer-details.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferDetailsComponent {
	public id$: Observable<string>;

	constructor(private route: ActivatedRoute) {
		this.id$ = this.route.paramMap.pipe(
			map((params) => params.get('id')),
			filter(Boolean)
		);
	}
}