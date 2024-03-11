import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	EventEmitter,
	OnInit,
	Output,
	ViewChild,
} from '@angular/core';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';
import {
	MatPaginator,
	MatPaginatorIntl,
	PageEvent,
} from '@angular/material/paginator';
import { environment } from 'src/environments/environment.prod';
import { OfferService } from '../../services/offer.service';
import { ISendOffers } from '../../models/offer.models';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';

@Component({
	selector: 'app-watched-offers',
	templateUrl: './watched-offers.component.html',
	styleUrls: ['./watched-offers.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class WatchedOffersComponent implements OnInit {
	public watchedOffers$: Observable<ISendOffers> =
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
		private cdr: ChangeDetectorRef,
		public realEstateService: RealEstateService
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

	public navigateToFlat(url: number) {
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
