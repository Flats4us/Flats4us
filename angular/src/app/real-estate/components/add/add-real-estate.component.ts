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
import {
	IAddProperty,
	IGroup,
	IRegionCity,
} from '../../models/real-estate.models';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject, map, takeUntil } from 'rxjs';
import { RealEstateService } from '../../services/real-estate.service';
import { MatStepper } from '@angular/material/stepper';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-add-real-estate',
	templateUrl: './add-real-estate.component.html',
	styleUrls: ['./add-real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddRealEstateComponent implements OnInit, OnDestroy {
	@ViewChild('stepper')
	public stepper: MatStepper | undefined;

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
	private filesArray: File[] = [];
	private formData = new FormData();

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private http: HttpClient,
		public realEstateService: RealEstateService,
		private changeDetectorRef: ChangeDetectorRef,
		private snackBar: MatSnackBar
	) {
		this.addRealEstateFormAddressData = formBuilder.group({
			regionsGroup: new FormControl('', Validators.required),
			citiesGroup: new FormControl('', Validators.required),
			districtsGroup: new FormControl(''),
			postalCode: new FormControl('', [
				Validators.required,
				Validators.pattern('^[0-9]{2}-[0-9]{3}$'),
			]),
			street: new FormControl('', Validators.required),
			streetNumber: new FormControl('', Validators.required),
			property: new FormControl(null, Validators.required),
			flatNumber: new FormControl(null, [Validators.required, Validators.min(0)]),
		});

		this.addRealEstateFormRemainingData = formBuilder.group({
			area: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			plotArea: new FormControl(null, [
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			maxResidents: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			equipment: new FormControl([]),
			rooms: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			floor: new FormControl(null, [
				Validators.required,
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			floors: new FormControl(null, [
				Validators.required,
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			year: new FormControl(null, [
				Validators.required,
				Validators.min(0),
				Validators.max(new Date().getFullYear()),
				Validators.pattern('^[12][0-9]{3}$'),
			]),
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
			this.filesArray.push(file);
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
		this.formData.append('TitleDeed', this.filesArray[0]);
		for (let i = 0; i < this.filesArray.length; i++) {
			this.formData.append('Images', this.filesArray[i]);
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
			this.onAdd();
			this.snackBar
				.open('Pomyślnie dodano nieruchomość!', 'Dodaj ofertę', {
					duration: 4000,
				})
				.onAction()
				.pipe(takeUntil(this.unsubscribe$))
				.subscribe(() => {
					this.router.navigate(['offer/add']);
				});
		}
	}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}

	public ngOnInit() {
		this.realEstateService
			.readAllEquipment()
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe();

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

		this.addRealEstateFormAddressData.get('flatNumber')?.disable();

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

		this.addRealEstateFormAddressData
			.get('property')
			?.valueChanges.pipe(takeUntil(this.unsubscribe$))
			.subscribe(property => {
				if (property === 0 || property === 2) {
					this.addRealEstateFormAddressData.get('flatNumber')?.enable();
					this.addRealEstateFormRemainingData.get('plotArea')?.disable();
					this.addRealEstateFormRemainingData.get('floor')?.enable();
				} else {
					this.addRealEstateFormAddressData.get('flatNumber')?.disable();
					this.addRealEstateFormRemainingData.get('plotArea')?.enable();
					this.addRealEstateFormRemainingData.get('floor')?.disable();
				}
			});
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

	public onReturn() {
		this.router.navigate(['/']);
	}

	public onAdd(): void {
		const propertyToAdd: IAddProperty = {
			propertyType: this.addRealEstateFormAddressData.get('property')?.value ?? 0,
			province: this.addRealEstateFormAddressData.get('regionsGroup')?.value ?? '',
			district:
				this.addRealEstateFormAddressData.get('districtsGroup')?.value ?? '',
			street: this.addRealEstateFormAddressData.get('street')?.value ?? '',
			number: this.addRealEstateFormAddressData.get('streetNumber')?.value ?? '',
			flat: this.addRealEstateFormAddressData.get('flatNumber')?.value ?? 0,
			city: this.addRealEstateFormAddressData.get('citiesGroup')?.value ?? '',
			postalCode: this.addRealEstateFormAddressData.get('postalCode')?.value ?? '',
			area: this.addRealEstateFormRemainingData.get('area')?.value ?? 1,
			maxNumberOfInhabitants:
				this.addRealEstateFormRemainingData.get('maxResidents')?.value ?? 1,
			constructionYear:
				this.addRealEstateFormRemainingData.get('year')?.value ?? 0,
			numberOfRooms: this.addRealEstateFormRemainingData.get('rooms')?.value ?? 1,
			floor: this.addRealEstateFormRemainingData.get('floor')?.value ?? 0,
			numberOfFloors:
				this.addRealEstateFormRemainingData.get('floors')?.value ?? 1,
			plotArea: this.addRealEstateFormRemainingData.get('plotArea')?.value ?? 1,
			equipment: this.addRealEstateFormRemainingData.get('equipment')?.value ?? [],
		};
		const headers = new HttpHeaders();
		headers.append('enctype', 'multipart/form-data');
		this.realEstateService
			.addRealEstate(propertyToAdd)
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(id =>
				this.realEstateService
					.addRealEstateFiles(id, this.formData, headers)
					.pipe(takeUntil(this.unsubscribe$))
					.subscribe()
			);
	}
	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
