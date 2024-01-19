import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef,
	Output,
	EventEmitter,
} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { ISortOption } from './models/start-site.models';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatPaginator } from '@angular/material/paginator';
import { IGroup, IRegionCity } from '../real-estate/models/real-estate.models';
import { RealEstateService } from '../real-estate/services/real-estate.service';
import { StartService } from './services/start.service';
import { environment } from 'src/environments/environment.prod';
import { ISendOffers } from '../offer/models/offer.models';
import { BaseComponent } from '@shared/components/base/base.component';

@Component({
	selector: 'app-start',
	templateUrl: './start.component.html',
	styleUrls: ['./start.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StartComponent extends BaseComponent implements OnInit {
	protected baseUrl = environment.apiUrl.replace('/api', '');

	public showMoreFilters = false;

	public isSubmitted = false;

	public startSiteForm: FormGroup;

	public isLoading$: Observable<boolean> = of(false);

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;
	private regionCityArray: IRegionCity[] = [];
	public pageEvent = new PageEvent();
	public pageSize = 6;
	public pageIndex = 0;
	public flatOptions$: Observable<ISendOffers>;
	public flatOptionsPromoted$: Observable<ISendOffers>;
	private sortState: ISortOption = {
		type: 'Price ASC',
		direction: 'asc',
		description: 'ceny: od najniższej',
	};

	@ViewChild(MatPaginator)
	private paginator: MatPaginator = new MatPaginator(
		this.matPaginatorIntl,
		ChangeDetectorRef.prototype
	);

	@Output()
	public page: EventEmitter<PageEvent> = new EventEmitter();

	constructor(
		private router: Router,
		private route: ActivatedRoute,
		private matPaginatorIntl: MatPaginatorIntl,
		public realEstateService: RealEstateService,
		public startService: StartService,
		private changeDetectorRef: ChangeDetectorRef,
		private formBuilder: FormBuilder
	) {
		super();
		this.startSiteForm = this.formBuilder.group({
			regionsGroup: new FormControl('', Validators.required),
			citiesGroup: new FormControl('', Validators.required),
			distance: new FormControl(0, Validators.required),
			property: new FormControl([], Validators.required),
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
			pageIndex: new FormControl(this.pageIndex),
			pageSize: new FormControl(this.pageSize),
		});
		this.startService.mapOffersForm = this.startSiteForm;
		this.realEstateService
			.readCitiesForRegions(
				this.regionCityArray,
				this.realEstateService.citiesGroups
			)
			.pipe(this.untilDestroyed())
			.subscribe();
		this.flatOptions$ = this.startService.getFilteredOffers(
			this.startSiteForm.value
		);
		this.flatOptionsPromoted$ = this.startService
			.getFilteredOffers(this.startSiteForm.value)
			.pipe(
				map(offers => ({
					totalCount: offers.result.filter(offer => offer.isPromoted).length,
					result: offers.result.filter(offer => offer.isPromoted === true),
				}))
			);
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

		this.filterOffers();
	}

	public filter(opt: string[], value: string): string[] {
		const filterValue = value.toLowerCase();
		return opt.filter(item => item.toLowerCase().includes(filterValue));
	}

	public showFilters() {
		this.showMoreFilters = !this.showMoreFilters;
	}

	public showMap() {
		this.realEstateService.addAddress(
			this.startSiteForm.get('citiesGroup')?.value,
			this.startSiteForm.get('districtsGroup')?.value
		);
		this.startService.mapOffersForm = this.startSiteForm;
		this.router.navigate(['map'], { relativeTo: this.route });
	}

	public addToWatched(id: number) {
		this.startService.addToWatched(id).pipe(this.untilDestroyed()).subscribe();
	}

	public onSubmit() {
		if (this.startSiteForm.valid) {
			this.isSubmitted = true;
			this.filterOffers();
		}
	}

	public onSelect(sortByOption: ISortOption) {
		this.sortState = sortByOption;
		this.filterOffers();
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
		this.router.navigate(['/offer', 'details', id]);
	}
	public validateForm() {
		return this.startSiteForm.valid;
	}

	public changePage(e: PageEvent) {
		this.pageEvent = e;
		this.pageSize = e.pageSize;
		this.pageIndex = e.pageIndex;
		this.filterOffers();
	}

	public async filterOffers() {
		this.isLoading$ = of(true);
		this.startService
			.getFilteredOffers(this.startSiteForm.value)
			.pipe(this.untilDestroyed())
			.subscribe(result => {
				this.flatOptions$ = of(result);
				if (!this.isSubmitted) {
					this.flatOptionsPromoted$ = of({
						totalCount: result.result.filter(offer => offer.isPromoted).length,
						result: result.result.filter(offer => offer.isPromoted),
					});
				}
				this.isLoading$ = of(false);
				this.changeDetectorRef.markForCheck();
			});
	}
}
