import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	OnDestroy,
	OnInit,
	ViewChild,
} from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { Router } from '@angular/router';
import { Observable, Subject, map, takeUntil } from 'rxjs';
import {
	IGroup,
	IRegionCity,
} from 'src/app/real-estate/models/real-estate.models';
import { ProfileService } from '../services/profile.service';

@Component({
	selector: 'app-profile-edit',
	templateUrl: './edit-profile.component.html',
	styleUrls: ['./edit-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditProfileComponent implements OnInit, OnDestroy {
	@ViewChild('stepper')
	public stepper: MatStepper | undefined;

	private readonly unsubscribe$: Subject<void> = new Subject();
	public changeAdress = false;

	public fileName = '';
	public url = '../../../assets/candidate.webp';

	public citiesGroupOptions$?: Observable<IGroup[]>;
	public districtGroupOptions$?: Observable<IGroup[]>;

	private regionCityArray: IRegionCity[] = [];

	public hobbies: string[] = [
		'Muzyka',
		'Sport',
		'Filmy',
		'Podróże',
		'Sztuka',
		'Nauka',
		'Książki',
		'Kulinaria',
		'Moda',
		'Gry',
		'Piwo',
		'Kulturystka',
	];

	public addressFormGroup;
	public phoneNumberFormGroup;

	public isLinear = false;

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private changeDetectorRef: ChangeDetectorRef,
		public profileService: ProfileService
	) {
		this.addressFormGroup = this.formBuilder.group({
			regionsGroup: new FormControl('', Validators.required),
			citiesGroup: new FormControl('', Validators.required),
			districtsGroup: new FormControl(''),
			street: new FormControl('', Validators.required),
			streetNumber: new FormControl('', Validators.required),
			property: new FormControl('', Validators.required),
		});
		this.phoneNumberFormGroup = this.formBuilder.group({
			phoneNumber: [
				null,
				[
					Validators.required,
					Validators.pattern(
						'^[+]?[(]?[0-9]{2,3}[)]?[-s.]?[0-9]{2,3}[-s.]?[0-9]{2,6}$'
					),
				],
			],
		});
		this.profileService
			.readCitiesForRegions(this.regionCityArray, this.profileService.citiesGroups)
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe();
	}

	public ngOnInit() {
		this.citiesGroupOptions$ = this.addressFormGroup
			.get('citiesGroup')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterCitiesGroup(value))
			);

		this.districtGroupOptions$ = this.addressFormGroup
			.get('districtsGroup')
			?.valueChanges.pipe(
				map(value => value ?? ''),
				map(value => this.filterDistrictsGroup(value))
			);

		this.addressFormGroup
			.get('citiesGroup')
			?.valueChanges.pipe(takeUntil(this.unsubscribe$))
			.subscribe(value => {
				if (
					this.profileService.districtGroups.find(distr => distr.whole === value)
				) {
					this.addressFormGroup.get('districtsGroup')?.enable();
				} else {
					this.addressFormGroup.get('districtsGroup')?.reset();
				}
			});
		this.addressFormGroup
			.get('regionsGroup')
			?.valueChanges.pipe(takeUntil(this.unsubscribe$))
			.subscribe(() => {
				this.addressFormGroup.get('citiesGroup')?.reset();
			});
	}

	public filter = (opt: string[], value: string): string[] => {
		const filterValue = value.toLowerCase();

		return opt.filter(item => item.toLowerCase().includes(filterValue));
	};

	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}

	private filterCitiesGroup(value: string): IGroup[] {
		return this.profileService.citiesGroups
			.map(group => ({
				whole: group.whole,
				parts: this.filter(group.parts, value),
			}))
			.filter(
				group =>
					group.parts.length > 0 &&
					group.whole === this.addressFormGroup.get('regionsGroup')?.value
			);
	}
	private filterDistrictsGroup(value: string): IGroup[] {
		return this.profileService.districtGroups
			.map(group => ({
				whole: group.whole,
				parts: this.filter(group.parts, value),
			}))
			.filter(
				group =>
					group.parts.length > 0 &&
					group.whole === this.addressFormGroup.get('citiesGroup')?.value
			);
	}

	public changeAddressData() {
		this.changeAdress ? (this.changeAdress = false) : (this.changeAdress = true);
	}
	public changeEmail() {
		this.router.navigate(['settings/email-change']);
	}
	public changePassword() {
		this.router.navigate(['settings/password-change']);
	}

	public changeHobbies($event: any) {
		return;
	}

	public submitForm() {
		return;
	}

	public onFileSelected(event: Event) {
		const formData = new FormData();
		const fileEvent = (event.target as HTMLInputElement).files;

		if (!fileEvent) {
			return;
		}
		const file: File = fileEvent[0];
		const fileType = file.type;
		if (fileType.match(/image\/*/) == null) {
			return;
		}
		this.fileName = file.name;
		formData.append(this.fileName, file);
		const reader = new FileReader();
		reader.readAsDataURL(file);
		reader.onload = () => {
			this.url = <string>reader.result;
			this.changeDetectorRef.detectChanges();
		};
	}
}
