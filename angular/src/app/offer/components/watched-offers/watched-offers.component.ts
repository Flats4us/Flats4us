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
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import {
	MatPaginator,
	MatPaginatorIntl,
	PageEvent,
} from '@angular/material/paginator';
import { IWatchedOffer } from '../../models/offer-models';
import { environment } from 'src/environments/environment.prod';

@Component({
	selector: 'app-watched-offers',
	templateUrl: './watched-offers.component.html',
	styleUrls: ['./watched-offers.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class WatchedOffersComponent implements OnInit {
	public watchedOffers$: Observable<IWatchedOffer> =
		this.offerService.getWatchedOffers(0, 3);

	public pageSize = 3;
	public pageIndex = 0;

	@ViewChild(MatPaginator, { static: true }) private paginator: MatPaginator =
		new MatPaginator(this.matPaginatorIntl, ChangeDetectorRef.prototype);

	@Output()
	public page: EventEmitter<PageEvent> = new EventEmitter();

	constructor(
		public offerService: OfferService,
		private router: Router,
		private matPaginatorIntl: MatPaginatorIntl,
		private cdr: ChangeDetectorRef
	) {}

	protected baseUrl = environment.apiUrl.replace('/api', '');

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
		this.filterOffers();
	}

	public navigateToFlat(url: string) {
		this.router.navigate([url]);
	}

	public deleteInterest(id: number) {
		this.offerService.deleteInterest(id).subscribe(() => this.filterOffers());
	}

	public changePage(e: PageEvent) {
		this.pageSize = e.pageSize;
		this.pageIndex = e.pageIndex;
		this.filterOffers();
	}

	public filterOffers() {
		this.watchedOffers$ = this.offerService.getWatchedOffers(
			this.pageIndex,
			this.pageSize
		);
		this.cdr.detectChanges();
	}
}
