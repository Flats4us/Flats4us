import {
	AfterViewInit,
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
import { environment } from 'src/environments/environment';
import { OfferService } from '../../services/offer.service';
import { ISendOffers } from '../../models/offer.models';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { IProperty } from 'src/app/real-estate/models/real-estate.models';
import { PropertyRatingComponent } from '../property-rating/property-rating.component';
import { MatDialog } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-watched-offers',
	templateUrl: './watched-offers.component.html',
	styleUrls: ['./watched-offers.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class WatchedOffersComponent
	extends BaseComponent
	implements OnInit, AfterViewInit
{
	public watchedOffers$: Observable<ISendOffers> =
		this.offerService.getWatchedOffers(0, 3);

	public pageSize = 3;
	public pageIndex = 0;

	private paginatorDescriptionA = 'z';
	private paginatorDescriptionB = 'ofert';

	@ViewChild(MatPaginator, { static: true }) private paginator: MatPaginator =
		new MatPaginator(this.matPaginatorIntl, this.cdr);

	@Output()
	public page: EventEmitter<PageEvent> = new EventEmitter();

	constructor(
		public offerService: OfferService,
		private router: Router,
		private matPaginatorIntl: MatPaginatorIntl,
		private cdr: ChangeDetectorRef,
		public realEstateService: RealEstateService,
		private dialog: MatDialog,
		private translate: TranslateService
	) {
		super();
	}
	public ngAfterViewInit(): void {
		this.paginatorDescriptionA = this.translate.instant('Paginator.of');
		this.paginatorDescriptionB = this.translate.instant('Paginator.offer-info');
		this.matPaginatorIntl.firstPageLabel = this.translate.instant(
			'Paginator.first-page'
		);
		this.matPaginatorIntl.itemsPerPageLabel = this.translate.instant(
			'Paginator.offers-page'
		);
		this.matPaginatorIntl.lastPageLabel = this.matPaginatorIntl.lastPageLabel =
			this.translate.instant('Paginator.last-page');
		this.matPaginatorIntl.nextPageLabel = this.translate.instant(
			'Paginator.next-page'
		);
		this.matPaginatorIntl.previousPageLabel = this.translate.instant(
			'Paginator.previous-page'
		);
		this.translate.onLangChange.pipe(this.untilDestroyed()).subscribe(() => {
			this.matPaginatorIntl.firstPageLabel = this.translate.instant(
				'Paginator.first-page'
			);
			this.matPaginatorIntl.itemsPerPageLabel = this.translate.instant(
				'Paginator.offers-page'
			);
			this.matPaginatorIntl.lastPageLabel = this.translate.instant(
				'Paginator.last-page'
			);
			this.matPaginatorIntl.nextPageLabel = this.translate.instant(
				'Paginator.next-page'
			);
			this.matPaginatorIntl.previousPageLabel = this.translate.instant(
				'Paginator.previous-page'
			);
			this.paginatorDescriptionA = this.translate.instant('Paginator.of');
			this.paginatorDescriptionB = this.translate.instant('Paginator.offer-info');
			this.matPaginatorIntl.changes.next();
		});
		this.matPaginatorIntl.getRangeLabel = (
			page: number,
			pageSize: number,
			length: number
		) => {
			if (length == 0 || pageSize == 0) {
				return `0 ${this.paginatorDescriptionA} ${length} ${this.paginatorDescriptionB}`;
			}
			length = Math.max(length, 0);
			const startIndex = page * pageSize;
			const endIndex =
				startIndex < length
					? Math.min(startIndex + pageSize, length)
					: startIndex + pageSize;
			return `${startIndex + 1} - ${endIndex} ${
				this.paginatorDescriptionA
			} ${length} ${this.paginatorDescriptionB}`;
		};
	}

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
		this.router.navigate(['offer', 'details', url]);
	}

	public deleteInterest(id: number) {
		this.offerService
			.deleteInterest(id)
			.pipe(this.untilDestroyed())
			.subscribe(() => this.filterOffers());
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
	public showRating(property: IProperty) {
		if (!property.avgRating) {
			return;
		}
		this.dialog.open(PropertyRatingComponent, {
			disableClose: false,
			closeOnNavigation: true,
			data: property,
		});
	}
}
