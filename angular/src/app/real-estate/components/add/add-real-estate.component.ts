import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	OnDestroy,
	OnInit,
	ViewChild,
} from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IGroup, IRegionCity } from '../../models/real-estate.models';
import { HttpClient } from '@angular/common/http';
import { Observable, Subject, map, takeUntil } from 'rxjs';
import { RealEstateService } from '../../services/real-estate.service';
import { MatStepper } from '@angular/material/stepper';

@Component({
	selector: 'app-add-real-estate',
	templateUrl: './add-real-estate.component.html',
	styleUrls: ['./add-real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddRealEstateComponent implements OnInit, OnDestroy {
	private readonly unsubscribe$: Subject<void> = new Subject();

	public addRealEstateFormAddressData;
	public addRealEstateFormRemainingData;
	public addRealEstateFormPhotos;

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;

	private regionCityArray: IRegionCity[] = [];

	public completed = false;
	public fileName = '';
	public urls: string[] = [];

	@ViewChild('stepper')
	public stepper: MatStepper | undefined;

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private http: HttpClient,
		public realEstateService: RealEstateService,
		private changeDetectorRef: ChangeDetectorRef
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
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			year: new FormControl('', Validators.required),
		});

		this.addRealEstateFormPhotos = formBuilder.group({
			photos: new FormControl(null, Validators.required),
		});

		this.realEstateService
			.readCitiesForRegions(
				this.regionCityArray,
				this.realEstateService.citiesGroups
			)
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe();
	}

	public filter = (opt: string[], value: string): string[] => {
		const filterValue = value.toLowerCase();

		return opt.filter(item => item.toLowerCase().includes(filterValue));
	};

	public onFileSelected(event: Event) {
		const formData = new FormData();

		const fileEvent = (event.target as HTMLInputElement).files;

		if (!fileEvent) {
			return;
		}

		for (let i = 0; i < fileEvent.length; i++) {
			const file: File = fileEvent[i];
			const fileType = file.type;
			if (fileType.match(/image\/*/) == null) {
				return;
			}
			this.fileName = file.name;
			formData.append(this.fileName, file);
			const reader = new FileReader();
			reader.readAsDataURL(file);
			reader.onload = () => {
				if (this.urls.length > 9) {
					return;
				}
				this.urls.push(<string>reader.result);
				this.changeDetectorRef.detectChanges();
			};
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
		this.router.navigate(['offer/add']);
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
			?.valueChanges.pipe(takeUntil(this.unsubscribe$))
			.subscribe(value => {
				if (
					this.realEstateService.districtGroups.find(distr => distr.whole === value)
				) {
					this.addRealEstateFormAddressData.get('districtsGroup')?.enable();
				} else {
					this.addRealEstateFormAddressData.get('districtsGroup')?.reset();
				}
			});
		this.addRealEstateFormAddressData
			.get('regionsGroup')
			?.valueChanges.pipe(takeUntil(this.unsubscribe$))
			.subscribe(() => {
				this.addRealEstateFormAddressData.get('citiesGroup')?.reset();
			});
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
		this.urls = [];
		this.fileName = '';
		this.addRealEstateFormAddressData.reset();
		this.addRealEstateFormRemainingData.reset();
		this.addRealEstateFormPhotos.reset();
		if (this.stepper) {
			this.stepper.selectedIndex = 0;
		}
	}
}
