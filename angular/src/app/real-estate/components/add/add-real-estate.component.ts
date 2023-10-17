import {
	ChangeDetectionStrategy,
	Component,
	OnDestroy,
	OnInit,
} from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IGroup, IRegionCity } from '../../models/real-estate.models';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, Subscription, map, of, takeUntil } from 'rxjs';
import { RealEstateService } from '../../services/real-estate.service';

@Component({
	selector: 'app-add-real-estate',
	templateUrl: './add-real-estate.component.html',
	styleUrls: ['./add-real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddRealEstateComponent implements OnInit, OnDestroy {
	private regionCitiesSubscription: Subscription;

	private readonly unsubscribe$: Subject<void> = new Subject();

	public addRealEstateFormAddressData;
	public addRealEstateFormRemainingData;
	public addRealEstateFormPhotos;

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;
	public urlsOptions$?: Observable<string[]>;

	private regionCityArray: IRegionCity[] = [];

	public completed = false;
	public fileName = '';
	public urls: string[] = [];

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private http: HttpClient,
		public realEstateService: RealEstateService
	) {
		this.addRealEstateFormAddressData = formBuilder.group({
			regionsGroup: new FormControl('', Validators.required),
			citiesGroup: new FormControl('', Validators.required),
			districtsGroup: new FormControl(''),
			street: new FormControl('', Validators.required),
			streetNumber: new FormControl('', Validators.required),
			property: new FormControl('', Validators.required),
		});

		this.addRealEstateFormRemainingData = formBuilder.group({
			area: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			maxResidents: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			equipment: new FormControl(''),
			rooms: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			floors: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			year: new FormControl('', Validators.required),
		});

		this.addRealEstateFormPhotos = formBuilder.group({
			photos: new FormControl(null, Validators.required),
		});

		this.regionCitiesSubscription = this.realEstateService
			.readCitiesForRegions(
				this.regionCityArray,
				this.realEstateService.citiesGroups
			)
			.subscribe();

		this.urlsOptions$ = of(this.urls);
	}

	public filter = (opt: string[], value: string): string[] => {
		const filterValue = value.toLowerCase();

		return opt.filter(item => item.toLowerCase().includes(filterValue));
	};

	public onFileSelected(event: Event) {
		const formData = new FormData();

		const fileEvent = (event.target as HTMLInputElement).files;

		if (fileEvent) {
			for (let i = 0; i < fileEvent.length; i++) {
				const file: File = fileEvent[i];

				this.fileName = file.name;

				formData.append(this.fileName, file);

				const reader = new FileReader();

				reader.readAsDataURL(file);

				reader.onloadend = () => {
					if (this.urls.length < 10) {
						this.urls.push(<string>reader.result);
					}
				};
			}
		}
	}

	public showMap() {
		this.router.navigate(['start/map']);
	}

	public saveRealEstate() {
		if (
			this.addRealEstateFormAddressData.valid &&
			this.addRealEstateFormRemainingData.valid &&
			this.addRealEstateFormPhotos.valid
		) {
			this.completed = true;
		}
	}

	public addOffer() {
		this.router.navigate(['/']);
	}

	public ngOnInit() {
		this.citiesGroupOptions$ = this.addRealEstateFormAddressData
			.get('citiesGroup')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterCitiesGroup(value))
			);

		this.districtGroupOptions$ = this.addRealEstateFormAddressData
			.get('districtsGroup')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterDistrictsGroup(value))
			);

		this.addRealEstateFormAddressData
			.get('citiesGroup')
			?.valueChanges.subscribe(value => {
				if (
					this.realEstateService.districtGroups.find(distr => distr.whole === value)
				) {
					this.addRealEstateFormAddressData.get('districtsGroup')?.enable();
				} else {
					this.addRealEstateFormAddressData.get('districtsGroup')?.reset();
				}
			}, takeUntil(this.unsubscribe$));
		this.addRealEstateFormAddressData
			.get('regionsGroup')
			?.valueChanges.subscribe(() => {
				this.addRealEstateFormAddressData.get('citiesGroup')?.reset();
			}, takeUntil(this.unsubscribe$));
	}

	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
		this.regionCitiesSubscription.unsubscribe();
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
					group.whole ===
						this.addRealEstateFormAddressData.get('regionsGroup')?.value
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
					group.whole === this.addRealEstateFormAddressData.get('citiesGroup')?.value
			);
	}

	public formReset() {
		this.addRealEstateFormAddressData.reset();
		this.addRealEstateFormRemainingData.reset();
		this.addRealEstateFormPhotos.reset();
		this.urls = [];
	}
}
