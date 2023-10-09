import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	AfterViewInit,
	ViewChild,
	ChangeDetectorRef,
} from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router } from '@angular/router';
import { IGroup, IRegionCity, IFlatOffer } from './models/start-site.models';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatPaginatorIntl } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import {
	areaFroms,
	areaTos,
	citiesGroups,
	distances,
	districtGroups,
	equipment,
	numberOfFloors,
	numberOfRooms,
	priceMaxs,
	properties,
	readCitiesForRegions,
	regions,
	yearOfBuilds,
} from '../shared/models/realEstate';

@Component({
	selector: 'app-start',
	templateUrl: './start.component.html',
	styleUrls: ['./start.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StartComponent implements AfterViewInit, OnInit {
	public showMoreFilters = false;

	public isSubmitted = false;

	public mainSiteForm: FormGroup = new FormGroup({});

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;
	public flatOptions$?: Observable<IFlatOffer[]>;

	public citiesGroups = citiesGroups;
	public districtGroups = districtGroups;
	public regions = regions;
	public numberOfFloors = numberOfFloors;
	public yearOfBuilds = yearOfBuilds;
	public properties = properties;
	public equipment = equipment;
	public distances = distances;
	public priceMaxs = priceMaxs;
	public areaFroms = areaFroms;
	public areaTos = areaTos;
	public numberOfRooms = numberOfRooms;

	public regionCityArray: IRegionCity[] = [];

	public allFlatOffers: IFlatOffer[] = [
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Bemowo',
			price: 2500,
			rent: 500,
			area: 35,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wola',
			price: 2000,
			rent: 700,
			area: 30,
			rooms: 2,
			url: '#',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Mokotów',
			price: 3000,
			rent: 900,
			area: 40,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Białołęka',
			price: 1200,
			rent: 300,
			area: 25,
			rooms: 1,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Pokój',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wawer',
			price: 1700,
			rent: 900,
			area: 30,
			rooms: 2,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Kawalerka',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Śródmieście',
			price: 2000,
			rent: 1000,
			area: 37,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Bemowo',
			price: 2500,
			rent: 500,
			area: 35,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wola',
			price: 2000,
			rent: 700,
			area: 30,
			rooms: 2,
			url: '#',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Mokotów',
			price: 3000,
			rent: 900,
			area: 40,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Białołęka',
			price: 1200,
			rent: 300,
			area: 25,
			rooms: 1,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Pokój',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wawer',
			price: 1700,
			rent: 900,
			area: 30,
			rooms: 2,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Kawalerka',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Śródmieście',
			price: 2000,
			rent: 1000,
			area: 37,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
	];

	public promotedFlatOffers: IFlatOffer[] = [
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Bemowo',
			price: 2500,
			rent: 500,
			area: 35,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wola',
			price: 2000,
			rent: 700,
			area: 30,
			rooms: 2,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Mokotów',
			price: 3000,
			rent: 900,
			area: 40,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Białołęka',
			price: 1800,
			rent: 800,
			area: 25,
			rooms: 1,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Wawer',
			price: 1700,
			rent: 900,
			area: 30,
			rooms: 2,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
		{
			regionCity: { region: 'mazowieckie', city: 'Warszawa' },
			district: 'Śródmieście',
			price: 2000,
			rent: 1000,
			area: 37,
			rooms: 3,
			url: '/',
			imgSource: './assets/mieszkanie.jpg',
			type: 'Mieszkanie',
		},
	];

	public numberOfRecords = this.allFlatOffers.length;

	public dataSource: MatTableDataSource<IFlatOffer> =
		new MatTableDataSource<IFlatOffer>(this.allFlatOffers);

	public chosenRegionString = '';
	public chosenCityString = '';
	public chosenDistrictString = '';

	@ViewChild(MatPaginator)
	public paginator: MatPaginator = new MatPaginator(
		this.matPaginatorIntl,
		ChangeDetectorRef.prototype
	);

	constructor(
		private formBuilder: FormBuilder,
		private http: HttpClient,
		private router: Router,
		private route: ActivatedRoute,
		private matPaginatorIntl: MatPaginatorIntl
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
		readCitiesForRegions(this.http, this.regionCityArray, this.citiesGroups);
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
		this.chosenRegionString = this.mainSiteForm
			.get('regionsGroup')
			?.value.toString();
		this.chosenCityString = this.mainSiteForm
			.get('citiesGroup')
			?.value.toString();
		this.chosenDistrictString =
			this.mainSiteForm.get('districtsGroup')?.value == null
				? ''
				: ', ' + this.mainSiteForm.get('districtsGroup')?.value.toString();
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

		this.mainSiteForm.get('citiesGroup')?.valueChanges.subscribe(value => {
			if (this.districtGroups.find(distr => distr.whole === value)) {
				this.mainSiteForm.get('districtsGroup')?.enable();
			} else {
				this.mainSiteForm.get('districtsGroup')?.reset();
				this.mainSiteForm.get('districtsGroup')?.disable();
			}
		});
		this.mainSiteForm.get('regionsGroup')?.valueChanges.subscribe(() => {
			this.mainSiteForm.get('citiesGroup')?.reset();
		});
	}

	private filterCitiesGroup(value: string): IGroup[] {
		return this.citiesGroups
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
		return this.districtGroups
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
