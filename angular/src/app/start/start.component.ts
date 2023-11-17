import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	ViewChild,
	ChangeDetectorRef,
	OnDestroy,
	Output,
	EventEmitter,
} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { ISendOffers, ISortOption } from './models/start-site.models';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatPaginatorIntl, PageEvent } from '@angular/material/paginator';
import { MatPaginator } from '@angular/material/paginator';
import { IGroup, IRegionCity } from '../real-estate/models/real-estate.models';
import { RealEstateService } from '../real-estate/services/real-estate.service';
import { StartService } from './services/start.service';
import { Sort, SortDirection } from '@angular/material/sort';

@Component({
	selector: 'app-start',
	templateUrl: './start.component.html',
	styleUrls: ['./start.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StartComponent implements OnInit, OnDestroy {
	private readonly unsubscribe$: Subject<void> = new Subject();

	public showMoreFilters = false;

	public isSubmitted = false;

	public startSiteForm: FormGroup = new FormGroup({});

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;
	public flatOptions$?: Observable<ISendOffers>;
	private regionCityArray: IRegionCity[] = [];
	private formBuilder: FormBuilder = new FormBuilder();
	public pageEvent = new PageEvent();
	public pageSize = 6;
	public pageIndex = 0;
	private sortState: Sort = { active: 'price', direction: 'desc' };

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
		public startService: StartService
	) {
		this.startSiteForm = this.formBuilder.group({
			regionsGroup: new FormControl('', Validators.required),
			citiesGroup: new FormControl('', Validators.required),
			distance: new FormControl(0, Validators.required),
			property: new FormControl('', Validators.required),
			minPrice: new FormControl(null, [Validators.min(0)]),
			maxPrice: new FormControl(null, [Validators.min(0)]),
			districtsGroup: new FormControl(''),
			minArea: new FormControl(null, [Validators.min(0)]),
			maxArea: new FormControl(null, [Validators.min(0)]),
			year: new FormControl(''),
			rooms: new FormControl(null, [Validators.min(1)]),
			floors: new FormControl(null, [Validators.min(0)]),
			equipment: new FormControl(''),
		});
		this.realEstateService
			.readCitiesForRegions(
				this.regionCityArray,
				this.realEstateService.citiesGroups
			)
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe();
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
			?.valueChanges.pipe(takeUntil(this.unsubscribe$))
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
			?.valueChanges.pipe(takeUntil(this.unsubscribe$))
			.subscribe(() => {
				this.startSiteForm.get('citiesGroup')?.reset();
			});

		this.flatOptions$ = this.startService.getOffers(0, 6);
	}

	public filter(opt: string[], value: string): string[] {
		const filterValue = value.toLowerCase();
		return opt.filter(item => item.toLowerCase().includes(filterValue));
	}

	public showFilters() {
		this.showMoreFilters = !this.showMoreFilters;
	}

	public showMap() {
		this.router.navigate(['map'], { relativeTo: this.route });
	}

	public addToFavorite() {
		this.router.navigate(['/']);
	}

	public showDescription(url: string) {
		this.router.navigate([url]);
	}

	public onSubmit() {
		if (this.startSiteForm.valid) {
			this.isSubmitted = true;
		}
	}

	public onSelect(sortByOption: ISortOption) {
		this.sortState = {
			active: sortByOption.type,
			direction: <SortDirection>sortByOption.direction,
		};
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

	public navigateToFlat(url: string) {
		this.router.navigate([url]);
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

	public filterOffers() {
		this.flatOptions$ = this.startService.getOffers(
			this.pageSize * this.pageIndex,
			this.pageSize * this.pageIndex + this.pageSize
		);
	}

	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
