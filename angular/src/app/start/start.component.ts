import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	AfterViewInit,
	ViewChild,
	ChangeDetectorRef,
	OnDestroy,
} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { IFlatOffer, ISortOption } from './models/start-site.models';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { IGroup, IRegionCity } from '../real-estate/models/real-estate.models';
import { RealEstateService } from '../real-estate/services/real-estate.service';
import { StartService } from './services/start.service';
import { MatSort, Sort, SortDirection } from '@angular/material/sort';

@Component({
	selector: 'app-start',
	templateUrl: './start.component.html',
	styleUrls: ['./start.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StartComponent implements AfterViewInit, OnInit, OnDestroy {
	private readonly unsubscribe$: Subject<void> = new Subject();

	public showMoreFilters = false;

	public isSubmitted: boolean;

	public mainSiteForm: FormGroup = new FormGroup({});

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;
	public flatOptions$?: Observable<IFlatOffer[]>;

	private regionCityArray: IRegionCity[] = [];

	public allFlatOffers: IFlatOffer[] = [];

	private dataSource: MatTableDataSource<IFlatOffer>;

	private sortState: Sort = { active: 'price', direction: 'desc' };

	@ViewChild(MatPaginator)
	public paginator: MatPaginator = new MatPaginator(
		this.matPaginatorIntl,
		ChangeDetectorRef.prototype
	);

	@ViewChild(MatSort, { static: false })
	public matSort: MatSort = new MatSort();

	constructor(
		private formBuilder: FormBuilder,
		private http: HttpClient,
		private router: Router,
		private route: ActivatedRoute,
		private matPaginatorIntl: MatPaginatorIntl,
		public realEstateService: RealEstateService,
		public startService: StartService
	) {
		this.mainSiteForm = formBuilder.group({
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
		this.isSubmitted = false;
		this.startService
			.getOffers(this.allFlatOffers)
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe();
		this.dataSource = new MatTableDataSource<IFlatOffer>(
			this.startService.allFlatOffers
		);
	}

	public filter = (opt: string[], value: string): string[] => {
		const filterValue = value.toLowerCase();

		return opt.filter(item => item.toLowerCase().includes(filterValue));
	};

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
		if (this.mainSiteForm.valid) {
			this.isSubmitted = true;
		}
	}

	public ngAfterViewInit() {
		this.dataSource.paginator = this.paginator;
		this.flatOptions$ = this.dataSource.connect();
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

		this.citiesGroupOptions$ = this.mainSiteForm
			.get('citiesGroup')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterCitiesGroup(value))
			);

		this.districtGroupOptions$ = this.mainSiteForm
			.get('districtsGroup')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterDistrictsGroup(value))
			);

		this.mainSiteForm.get('districtsGroup')?.disable();

		this.mainSiteForm
			.get('citiesGroup')
			?.valueChanges.pipe(takeUntil(this.unsubscribe$))
			.subscribe(value => {
				if (
					this.realEstateService.districtGroups.find(distr => distr.whole === value)
				) {
					this.mainSiteForm.get('districtsGroup')?.enable();
				} else {
					this.mainSiteForm.get('districtsGroup')?.reset();
					this.mainSiteForm.get('districtsGroup')?.disable();
				}
			});
		this.mainSiteForm
			.get('regionsGroup')
			?.valueChanges.pipe(takeUntil(this.unsubscribe$))
			.subscribe(() => {
				this.mainSiteForm.get('citiesGroup')?.reset();
			});
	}

	public onSelect(sortByOption: ISortOption) {
		this.dataSource.sort = this.matSort;
		this.sortState = {
			active: sortByOption.type,
			direction: <SortDirection>sortByOption.direction,
		};
		this.matSort.active = this.sortState.active;
		this.matSort.direction = this.sortState.direction;
		this.matSort.sortChange
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(() => (this.paginator.pageIndex = 0));
		this.matSort.sortChange.emit(this.sortState);
	}

	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
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
					group.whole === this.mainSiteForm.get('regionsGroup')?.value
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
					group.whole === this.mainSiteForm.get('citiesGroup')?.value
			);
	}

	public navigateToFlat(url: string) {
		this.router.navigate([url]);
	}
	public validateForm() {
		return this.mainSiteForm.valid;
	}
}
