import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, Observable } from 'rxjs';
import { GalleryItem, ImageItem } from 'ng-gallery';

@Component({
	selector: 'app-offer-details',
	templateUrl: './offer-details.component.html',
	styleUrls: ['./offer-details.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferDetailsComponent {
	public id$: Observable<string>;
	images: GalleryItem[] = [];

	constructor(private route: ActivatedRoute) {
		this.id$ = this.route.paramMap.pipe(
			map((params) => params.get('id')),
			filter(Boolean)
		);
	}

	ngOnInit() {
		this.images = [
			new ImageItem({
				src: '../../assets/offer-details/example1.jpg',
				thumb: '../../assets/offer-details/example1.jpg',
			}),
			new ImageItem({
				src: '../../assets/offer-details/example2.jpg',
				thumb: '../../assets/offer-details/example2.jpg',
			}),
			new ImageItem({
				src: '../../assets/offer-details/example3.jpg',
				thumb: '../../assets/offer-details/example3.jpg',
			}),
			new ImageItem({
				src: '../../assets/offer-details/example4.jpg',
				thumb: '../../assets/offer-details/example4.jpg',
			}),
		];
	}
}
