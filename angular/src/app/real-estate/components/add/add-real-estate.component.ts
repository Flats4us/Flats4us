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
import { ActivatedRoute, Router } from '@angular/router';
import { IGroup, IProperty, IRegionCity } from '../../models/real-estate.models';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, catchError, concatMap, filter, map, switchMap, throwError } from 'rxjs';
import { RealEstateService } from '../../services/real-estate.service';
import { MatStepper } from '@angular/material/stepper';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseComponent } from '@shared/components/base/base.component';
import { ModificationType } from '../../models/types';
import { environment } from 'src/environments/environment.prod';

@Component({
	selector: 'app-add-real-estate',
	templateUrl: './add-real-estate.component.html',
	styleUrls: ['./add-real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddRealEstateComponent extends BaseComponent implements OnInit {
	@ViewChild('stepper')
	public stepper: MatStepper | undefined;

	protected baseUrl = environment.apiUrl.replace('/api', '/');

	public addRealEstateFormAddressData: FormGroup = new FormGroup
	({
	province: new FormControl('', Validators.required),
	city: new FormControl('', Validators.required),
	district: new FormControl(''),
	postalCode: new FormControl('', [
		Validators.required,
		Validators.pattern('^[0-9]{2}-[0-9]{3}$'),
	]),
	street: new FormControl('', Validators.required),
	number: new FormControl('', [
		Validators.required,
		Validators.pattern('^[0-9]+[a-zA-Z]*$'),
	]),
	propertyType: new FormControl(null, Validators.required),
	flat: new FormControl(null,  [Validators.required, Validators.min(0)])
});
	public addRealEstateFormRemainingData: FormGroup = new FormGroup({
		area: new FormControl(null, [
			Validators.required,
			Validators.min(1),
			Validators.pattern('^[0-9]*$'),
		]),
		plotArea: new FormControl(null, [
			Validators.required,
			Validators.min(1),
			Validators.pattern('^[0-9]*$'),
		]),
		maxNumberOfInhabitants: new FormControl(null, [
			Validators.required,
			Validators.min(1),
			Validators.pattern('^[0-9]*$'),
		]),
		equipmentIds: new FormControl([]),
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
	public photos: {path: string, name: string}[] = [];
	public addRealEstateFormPhotos: FormGroup = new FormGroup({
		photos: new FormControl(null, Validators.required),
	});

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;

	private regionCityArray: IRegionCity[] = [];

	public completed = false;
	public fileName = '';
	private filesArray: File[] = [];
	private formData = new FormData();
	public modificationType: ModificationType;
	public mType = ModificationType;
	public actualRealEstate: Observable<IProperty>;
	public actualId = 0;
	public actualPhotosNames: string[] = [];
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
		equipmentIds: new FormControl([]),
	});

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private http: HttpClient,
		public realEstateService: RealEstateService,
		private changeDetectorRef: ChangeDetectorRef,
		private snackBar: MatSnackBar,
		private activatedRoute: ActivatedRoute
	) {
		super();

		this.realEstateService
			.readCitiesForRegions(
				this.regionCityArray,
				this.realEstateService.citiesGroups
			)
			.pipe(this.untilDestroyed())
			.subscribe();

		this.modificationType = this.activatedRoute.snapshot.url[0].path.toUpperCase() as ModificationType;
		this.actualRealEstate = this.activatedRoute.paramMap.pipe(
			map(params => params.get('id')),
			filter(Boolean),
			switchMap(id => {
				return this.realEstateService.getRealEstateById(parseInt(id) ?? 0)
			}));
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
			if(this.filesArray.length < 11 && 
				!this.filesArray.find(image => image.name === file.name)){
				this.filesArray.push(file);
			}
			this.fileName = file.name;
			const reader = new FileReader();
			reader.readAsDataURL(file);
			reader.onload = () => {
				if (this.photos.length > 9 || 
					this.photos.find(image => image.name === file.name)) {
					return;
				}
				this.photos.push({path: reader.result as string, name: file.name});
				this.changeDetectorRef.markForCheck();
			};
		}
		// this.formData.append('TitleDeed', this.filesArray[0]);
		// for (let i = 0; i < this.filesArray.length; i++) {
		// 	this.formData.append('Images', this.filesArray[i]);
		// }
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

	public ngOnInit() {
		this.realEstateService
			.readAllEquipment()
			.pipe(this.untilDestroyed())
			.subscribe();

		this.citiesGroupOptions$ = this.addRealEstateFormAddressData
			.get('city')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterCitiesGroup(value))
			);

		this.districtGroupOptions$ = this.addRealEstateFormAddressData
			.get('district')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterDistrictsGroup(value))
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
		if(this.modificationType === ModificationType.EDIT){
			this.actualRealEstate.pipe(this.untilDestroyed()).subscribe(property => {
				this.addRealEstateFormAddressData.patchValue({
					province: property.province.toLowerCase(), 
					city: property.city,
					district: property.district,
					postalCode: property.postalCode,
					street: property.street,
					number: property.number,
					propertyType: property.propertyType,
					flat: property.flat
				});
				this.addRealEstateFormRemainingData.patchValue({
					area: property.area,
					plotArea: property.plotArea,
					maxNumberOfInhabitants: property.maxNumberOfInhabitants,
					equipmentIds: property.equipment.map(equipment => {return equipment.equipmentId}),
					numberOfRooms: property.numberOfRooms,
					floor: property.floor,
					numberOfFloors: property.numberOfFloors,
					constructionYear: property.constructionYear
				});
				this.photos = property.images.map(image => {return {path: this.baseUrl + image.path, name: image.name}});
				this.fileName = property.images[0].name;
				this.addRealEstateFormPhotos = new FormGroup({
					photos: new FormControl(null),
				});
				this.actualId = property.propertyId;
				this.actualPhotosNames = property.images.map(image => {return image.name});
			});
			this.addRealEstateFormAddressData.get('propertyType')?.disable();
		}
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
					group.whole === this.addRealEstateFormAddressData.get('province')?.value
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
					group.whole === this.addRealEstateFormAddressData.get('city')?.value
			);
	}

	public formReset() {
		this.photos = [];
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

	public onPhotoDelete(name: string){
		this.photos.forEach( (item, index) => {
			if(item.name === name) {
				this.photos.splice(index, 1)
			}
		});
		this.filesArray.forEach( (item, index) => {
			if(item.name === name) {
				this.filesArray.splice(index, 1)
			}
		});
		this.fileName = this.photos[this.photos.length - 1].name;
		if(this.modificationType === ModificationType.EDIT && 
			this.actualPhotosNames.find(value => value === name)){
		this.realEstateService.deletePhoto(this.actualId, name)
		.pipe(
			this.untilDestroyed(),
			catchError(this.handleError)
		)
		.subscribe({
			next: () => {
				this.snackBar.open('Zdjęcie zostało pomyślnie usunięte.', 'Zamknij', {
					duration: 2000,
				});
			},
			error: () => {
				this.snackBar.open(
					'Nie udało się usunąć zdjęcia. Spróbuj ponownie.',
					'Zamknij',
					{ duration: 2000 }
				);
			},
		});
	}
	}

	public onAdd(): void {
		this.formData.append('TitleDeed', this.filesArray[0]);
		for (let i = 0; i < this.filesArray.length; i++) {
			this.formData.append('Images', this.filesArray[i]);
		}
		this.addRealEstateForm.patchValue(this.addRealEstateFormAddressData.value);
		this.addRealEstateForm.patchValue(this.addRealEstateFormRemainingData.value);
		if(this.modificationType === ModificationType.ADD){
		this.realEstateService
			.addRealEstate(this.addRealEstateForm.value)
			.pipe(
				this.untilDestroyed(),
				catchError(this.handleError),
				concatMap(id =>
					this.realEstateService.addRealEstateFiles(id, this.formData)
				)
			)
			.subscribe({
				next: () => {
					this.snackBar.open('Nieruchomość została pomyślnie zapisana.', 'Zamknij', {
						duration: 2000,
					});
					this.router.navigate(['/']);
				},
				error: () => {
					this.snackBar.open(
						'Nie udało się dodać nieruchomości. Spróbuj ponownie.',
						'Zamknij',
						{ duration: 2000 }
					);
				},
			});
		}
		if(this.modificationType === ModificationType.EDIT){
			this.realEstateService
				.editRealEstate(this.addRealEstateForm.value, this.actualId)
				.pipe(
					this.untilDestroyed(),
					catchError(this.handleError),
					concatMap(() =>
						this.realEstateService.addRealEstateFiles(this.actualId, this.formData)
					)
				)
				.subscribe({
					next: () => {
						this.snackBar.open('Zmiany zostały pomyślnie zapisane', 'Zamknij', {
							duration: 2000,
						});
						this.router.navigate(['/']);
					},
					error: () => {
						this.snackBar.open(
							'Nie udało się zmodyfikować nieruchomości. Spróbuj ponownie.',
							'Zamknij',
							{ duration: 2000 }
						);
					},
				});
			}
	}

	private handleError(error: HttpErrorResponse) {
		return throwError(
			() => new Error('Błąd. Spróbuj ponownie.')
		);
	}
}
