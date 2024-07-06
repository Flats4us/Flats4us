import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef,
	Output,
	EventEmitter,
	AfterViewInit,
} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { ISortOption } from './models/start-site.models';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatPaginator } from '@angular/material/paginator';
import {
	IGroup,
	IProperty,
	IRegionCity,
} from '../real-estate/models/real-estate.models';
import { RealEstateService } from '../real-estate/services/real-estate.service';
import { StartService } from './services/start.service';
import { environment } from 'src/environments/environment';
import { ISendOffers } from '../offer/models/offer.models';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '@shared/services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { PropertyRatingComponent } from '../offer/components/property-rating/property-rating.component';
import { LangChangeEvent, TranslateService } from '@ngx-translate/core';
import { AuthModels } from '@shared/models/auth.models';

@Component({
	selector: 'app-start',
	templateUrl: './start.component.html',
	styleUrls: ['./start.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StartComponent
	extends BaseComponent
	implements OnInit, AfterViewInit
{
	protected baseUrl = environment.apiUrl.replace('/api', '');

	public showMoreFilters = false;

	public startSiteForm: FormGroup;

	public isLoading$: Observable<boolean> = of(false);

	public isMapMode = false;

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;
	private regionCityArray: IRegionCity[] = [];
	public pageEvent = new PageEvent();
	public pageSize = 6;
	public pageIndex = 0;
	public flatOptions$: Observable<ISendOffers>;
	private sortState: ISortOption = {
		type: 'Price ASC',
		direction: 'asc',
		description: 'Start.sort-option1',
	};
	private paginatorDescriptionA = 'z';
	private paginatorDescriptionB = 'ofert';
	public languageChange: string = this.translate.currentLang;
	public authModels = AuthModels;

	@ViewChild(MatPaginator, { static: true })
	private paginator: MatPaginator = new MatPaginator(
		this.matPaginatorIntl,
		this.changeDetectorRef
	);

	@Output()
	public page: EventEmitter<PageEvent> = new EventEmitter();

	constructor(
		private router: Router,
		private matPaginatorIntl: MatPaginatorIntl,
		public realEstateService: RealEstateService,
		public startService: StartService,
		private changeDetectorRef: ChangeDetectorRef,
		private formBuilder: FormBuilder,
		private snackBar: MatSnackBar,
		public authService: AuthService,
		public dialog: MatDialog,
		private translate: TranslateService
	) {
		super();
		this.startSiteForm = this.formBuilder.group({
			regionsGroup: new FormControl(''),
			citiesGroup: new FormControl(''),
			distance: new FormControl(0),
			property: new FormControl([]),
			minPrice: new FormControl(null, [Validators.min(0)]),
			maxPrice: new FormControl(null, [Validators.min(0)]),
			districtsGroup: new FormControl(''),
			minArea: new FormControl(null, [Validators.min(0)]),
			maxArea: new FormControl(null, [Validators.min(0)]),
			year: new FormControl([]),
			rooms: new FormControl(null, [Validators.min(1)]),
			floors: new FormControl(null, [Validators.min(0)]),
			equipment: new FormControl([]),
			sorting: new FormControl(this.sortState),
		});
		this.startService.mapOffersForm = this.startSiteForm;
		this.realEstateService
			.readCitiesForRegions(
				this.regionCityArray,
				this.realEstateService.citiesGroups
			)
			.pipe(this.untilDestroyed())
			.subscribe();
		this.realEstateService
			.readDistrictsForCities()
			.pipe(this.untilDestroyed())
			.subscribe();
		this.flatOptions$ = this.startService.getFilteredOffers(
			this.startSiteForm.value,
			this.pageIndex,
			this.pageSize
		);
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
		this.translate.onLangChange
			.pipe(this.untilDestroyed())
			.subscribe((event: LangChangeEvent) => {
				this.languageChange = event.lang;
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

		this.realEstateService
			.readAllEquipment()
			.pipe(this.untilDestroyed())
			.subscribe();

		this.citiesGroupOptions$ = this.startSiteForm
			.get('citiesGroup')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterCitiesGroup(value))
			);

		this.districtGroupOptions$ = this.startSiteForm
			.get('districtsGroup')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterDistrictsGroup(value))
			);

		this.startSiteForm.get('districtsGroup')?.disable();

		this.startSiteForm
			.get('citiesGroup')
			?.valueChanges.pipe(this.untilDestroyed())
			.subscribe(value => {
				if (
					this.realEstateService.districtGroups.find(distr => distr.whole === value)
				) {
					this.startSiteForm.get('districtsGroup')?.enable();
				} else {
					this.startSiteForm.get('districtsGroup')?.reset();
					this.startSiteForm.get('districtsGroup')?.disable();
				}
			});
		this.startSiteForm
			.get('regionsGroup')
			?.valueChanges.pipe(this.untilDestroyed())
			.subscribe(() => {
				this.startSiteForm.get('citiesGroup')?.reset();
			});
	}

	public filter(opt: string[], value: string): string[] {
		const filterValue = value.toLowerCase();
		return opt.filter(item => item.toLowerCase().includes(filterValue));
	}

	public showFilters() {
		this.showMoreFilters = !this.showMoreFilters;
	}

	public showMap() {
		this.isMapMode = this.isMapMode ? false : true;
	}

	public addToWatched(id: number) {
		this.startService
			.addToWatched(id)
			.pipe(this.untilDestroyed())
			.subscribe({
				next: () =>
					this.snackBar.open(
						this.translate.instant('Start.info1'),
						this.translate.instant('close'),
						{
							duration: 2000,
						}
					),
				error: () => {
					this.snackBar.open(
						this.translate.instant('Start.info2'),
						this.translate.instant('close'),
						{ duration: 2000 }
					);
				},
			});
		this.flatOptions$ = this.startService.getFilteredOffers(
			this.startSiteForm.value,
			this.pageIndex,
			this.pageSize
		);
	}

	public onSubmit() {
		if (this.startSiteForm.valid) {
			this.filterOffers(true);
		}
	}

	public onSelect(sortByOption: ISortOption) {
		this.startSiteForm.patchValue({ sorting: sortByOption });
		this.filterOffers(true);
	}

	private filterCitiesGroup(value: string): IGroup[] {
		return this.realEstateService.citiesGroups
			.map(group => ({
				whole: group.whole,
				parts: this.filter(group.parts, value),
			}))
			.filter(
				group =>
					group.parts.length > 0 &&
					group.whole === this.startSiteForm.get('regionsGroup')?.value
			);
	}
	private filterDistrictsGroup(value: string): IGroup[] {
		return this.realEstateService.districtGroups
			.map(group => ({
				whole: group.whole,
				parts: this.filter(group.parts, value),
			}))
			.filter(
				group =>
					group.parts.length > 0 &&
					group.whole === this.startSiteForm.get('citiesGroup')?.value
			);
	}

	public navigateToFlat(id: number) {
		this.router.navigate(['offer', 'details', id]);
	}
	public validateForm() {
		return this.startSiteForm.valid;
	}

	public changePage(e: PageEvent) {
		this.pageEvent = e;
		this.pageSize = e.pageSize;
		this.pageIndex = e.pageIndex;
		this.filterOffers(false);
	}

	public async filterOffers(onSumbit: boolean) {
		this.isLoading$ = of(true);
		if (onSumbit) {
			this.pageIndex = 0;
			this.pageSize = 6;
		}
		this.startService
			.getFilteredOffers(this.startSiteForm.value, this.pageIndex, this.pageSize)
			.pipe(this.untilDestroyed())
			.subscribe(result => {
				this.flatOptions$ = of(result);
				this.isLoading$ = of(false);
				this.changeDetectorRef.markForCheck();
			});
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
