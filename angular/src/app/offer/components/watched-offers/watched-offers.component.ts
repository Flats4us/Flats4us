import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	EventEmitter,
	OnInit,
	Output,
	ViewChild,
} from '@angular/core';
import { OfferService } from 'src/app/offer/services/offer-service';
import { Observable, of, switchMap } from 'rxjs';
import { Router } from '@angular/router';
import { ISendOffers } from '../../models/offer-models';
import {
	MatPaginator,
	MatPaginatorIntl,
	PageEvent,
} from '@angular/material/paginator';

@Component({
	selector: 'app-watched-offers',
	templateUrl: './watched-offers.component.html',
	styleUrls: ['./watched-offers.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class WatchedOffersComponent implements OnInit {
	public watchedOffers$: Observable<ISendOffers> = this.offerService.getOffers(
		0,
		6
	);

	public pageSize = 6;
	public pageIndex = 0;

	@ViewChild(MatPaginator) private paginator: MatPaginator = new MatPaginator(
		this.matPaginatorIntl,
		ChangeDetectorRef.prototype
	);

	@Output()
	public page: EventEmitter<PageEvent> = new EventEmitter();

	constructor(
		public offerService: OfferService,
		private router: Router,
		private matPaginatorIntl: MatPaginatorIntl
	) {}

	public ngOnInit() {
		this.matPaginatorIntl.firstPageLabel = 'pierwsza strona';
		this.matPaginatorIntl.itemsPerPageLabel = 'Oferty na stronie';
		this.matPaginatorIntl.lastPageLabel = 'ostatnia strona';
		this.matPaginatorIntl.nextPageLabel = 'następna strona';
		this.matPaginatorIntl.previousPageLabel = 'poprzednia strona';
		this.matPaginatorIntl.getRangeLabel = (
			page: number,
			pageSize: number,
			length: number
		) => {
			if (length == 0 || pageSize == 0) {
				return `0 z ${length} ofert`;
			}
			length = Math.max(length, 0);
			const startIndex = page * pageSize;
			const endIndex =
				startIndex < length
					? Math.min(startIndex + pageSize, length)
					: startIndex + pageSize;
			return `${startIndex + 1} - ${endIndex} z ${length} ofert`;
		};
	}

	public navigateToFlat(url: string) {
		this.router.navigate([url]);
	}
	public deleteOffer(id: string) {
		this.watchedOffers$ = this.watchedOffers$?.pipe(
			switchMap(offers =>
				of({
					data: offers.data.filter(offer => offer.id !== id),
					total: offers.data.filter(offer => offer.id !== id).length,
				})
			)
		);
	}

	public changePage(e: PageEvent) {
		this.pageSize = e.pageSize;
		this.pageIndex = e.pageIndex;
		this.filterOffers();
	}

	public filterOffers() {
		this.watchedOffers$ = this.offerService.getOffers(
			this.pageSize * this.pageIndex,
			this.pageSize * this.pageIndex + this.pageSize
		);
	}
}
