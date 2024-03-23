import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	OnInit,
	ViewChild,
} from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import { IGroup, IRegionCity } from '../../models/real-estate.models';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, concatMap, map, throwError } from 'rxjs';
import { RealEstateService } from '../../services/real-estate.service';
import { MatStepper } from '@angular/material/stepper';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseComponent } from '@shared/components/base/base.component';

@Component({
	selector: 'app-add-real-estate',
	templateUrl: './add-real-estate.component.html',
	styleUrls: ['./add-real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddRealEstateComponent extends BaseComponent implements OnInit {
	@ViewChild('stepper')
	public stepper: MatStepper | undefined;

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
	private addRealEstateForm: FormGroup = new FormGroup({
		propertyType: new FormControl(null),
		province: new FormControl(''),
		district: new FormControl(''),
		street: new FormControl(''),
		number: new FormControl(''),
		flat: new FormControl(null),
		city: new FormControl(''),
		postalCode: new FormControl(''),
		area: new FormControl(null),
		maxNumberOfInhabitants: new FormControl(null),
		constructionYear: new FormControl(null),
		numberOfRooms: new FormControl(null),
		floor: new FormControl(null),
		numberOfFloors: new FormControl(null),
		plotArea: new FormControl(null),
		equipment: new FormControl([]),
	});

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private http: HttpClient,
		public realEstateService: RealEstateService,
		private changeDetectorRef: ChangeDetectorRef,
		private snackBar: MatSnackBar,
	) {
		super();
		this.addRealEstateFormAddressData = formBuilder.group({
			province: new FormControl('', Validators.required),
			city: new FormControl('', Validators.required),
			district: new FormControl(''),
			postalCode: new FormControl('', [
				Validators.required,
				Validators.pattern('^[0-9]{2}-[0-9]{3}$'),
			]),
			street: new FormControl('', Validators.required),
			number: new FormControl('', Validators.required),
			propertyType: new FormControl(null, Validators.required),
			flat: new FormControl(null, [Validators.required, Validators.min(0)]),
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
			maxNumberOfInhabitants: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			equipment: new FormControl([]),
			numberOfRooms: new FormControl(null, [
				Validators.required,
				Validators.min(1),
				Validators.pattern('^[0-9]*$'),
			]),
			floor: new FormControl(null, [
				Validators.required,
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			numberOfFloors: new FormControl(null, [
				Validators.required,
				Validators.min(0),
				Validators.pattern('^[0-9]*$'),
			]),
			constructionYear: new FormControl(null, [
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
				this.realEstateService.citiesGroups,
			)
			.pipe(this.untilDestroyed())
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
		this.router.navigate(['start', 'map']);
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
				.pipe(this.untilDestroyed())
				.subscribe(() => {
					this.router.navigate(['offer', 'add']);
				});
		}
	}

	public addOffer() {
		this.router.navigate(['offer', 'add']);
	}

	public ngOnInit() {
		this.realEstateService
			.readAllEquipment()
			.pipe(this.untilDestroyed())
			.subscribe();

		this.citiesGroupOptions$ = this.addRealEstateFormAddressData
			.get('city')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterCitiesGroup(value)),
			);

		this.districtGroupOptions$ = this.addRealEstateFormAddressData
			.get('district')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterDistrictsGroup(value)),
			);

		this.addRealEstateFormAddressData.get('flat')?.disable();

		this.addRealEstateFormAddressData
			.get('city')
			?.valueChanges.pipe(this.untilDestroyed())
			.subscribe(value => {
				if (
					this.realEstateService.districtGroups.find(distr => distr.whole === value)
				) {
					this.addRealEstateFormAddressData.get('district')?.enable();
				} else {
					this.addRealEstateFormAddressData.get('district')?.reset();
				}
			});
		this.addRealEstateFormAddressData
			.get('province')
			?.valueChanges.pipe(this.untilDestroyed())
			.subscribe(() => {
				this.addRealEstateFormAddressData.get('city')?.reset();
			});

		this.addRealEstateFormAddressData
			.get('propertyType')
			?.valueChanges.pipe(this.untilDestroyed())
			.subscribe(propertyType => {
				if (propertyType === 0 || propertyType === 2) {
					this.addRealEstateFormAddressData.get('flat')?.enable();
					this.addRealEstateFormRemainingData.get('plotArea')?.disable();
					this.addRealEstateFormRemainingData.get('floor')?.enable();
				} else {
					this.addRealEstateFormAddressData.get('flat')?.disable();
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
					group.whole === this.addRealEstateFormAddressData.get('province')?.value,
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
					group.whole === this.addRealEstateFormAddressData.get('city')?.value,
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
		this.addRealEstateForm.patchValue(this.addRealEstateFormAddressData.value);
		this.addRealEstateForm.patchValue(this.addRealEstateFormRemainingData.value);
		this.realEstateService
			.addRealEstate(this.addRealEstateForm.value)
			.pipe(
				this.untilDestroyed(),
				catchError(this.handleError),
				concatMap(id =>
					this.realEstateService.addRealEstateFiles(id, this.formData),
				),
			)
			.subscribe({
				error: () => {
					this.snackBar.open(
						'Nie udało się dodać nieruchomości. Spróbuj ponownie.',
						'Zamknij',
						{ duration: 2000 },
					);
				},
			});
	}

	private handleError(error: HttpErrorResponse) {
		return throwError(
			() => new Error('Nie udało się dodać nieruchomości. Spróbuj ponownie.'),
		);
	}
}
