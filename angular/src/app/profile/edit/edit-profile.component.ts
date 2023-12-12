import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	ElementRef,
	EventEmitter,
	Input,
	OnChanges,
	OnDestroy,
	OnInit,
	Output,
	ViewChild,
	inject,
} from '@angular/core';
import {
	Observable,
	Subject,
	map,
	of,
	startWith,
	switchMap,
	takeUntil,
} from 'rxjs';
import {
	AbstractControl,
	FormBuilder,
	FormControl,
	FormGroup,
	FormGroupDirective,
	ValidationErrors,
	ValidatorFn,
	Validators,
} from '@angular/forms';
import { IOwner, IStudent } from '../models/profile.models';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { ActivatedRoute, Router } from '@angular/router';
import { ProfileService } from '../services/profile.service';
import { MatChipEditedEvent, MatChipInputEvent } from '@angular/material/chips';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { modificationType, statusType, userType } from '../models/types';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-profile-edit',
	templateUrl: './edit-profile.component.html',
	styleUrls: ['./edit-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditProfileComponent implements OnInit, OnChanges, OnDestroy {
	@Output()
	public newPhotoEvent = new EventEmitter<string>();

	@Output()
	public newScanEvent = new EventEmitter<string>();

	@Input()
	public name: string | null | undefined = '';

	@Input()
	public surname: string | null | undefined = '';

	@Input()
	public email: string | null | undefined = '';

	@Input()
	public modificationType = modificationType.EDIT;

	@ViewChild('hobbiesInput')
	public hobbiesInput!: ElementRef<HTMLInputElement>;

	private readonly unsubscribe$: Subject<void> = new Subject();

	public separatorKeysCodes: number[] = [ENTER, COMMA];
	public hobbyCtrl = new FormControl('');
	public socialMediaCtrl = new FormControl('');
	public filteredHobbies$?: Observable<string[]>;
	private rentId$?: Observable<string>;
	public user$?: Observable<string>;
	private user = '';
	public myHobbies: string[] = [];
	public socialMedias: string[] = [];
	public actualStudent$?: Observable<IStudent>;
	public actualStudent = {} as IStudent;
	public actualOwner$?: Observable<IOwner>;
	public actualOwner = {} as IOwner;

	public createAccountForm: FormGroup = new FormGroup({});

	public yearOfBirth = new FormControl('', [
		Validators.required,
		Validators.pattern('^(19|20)[0-9]{2}$'),
		Validators.max(new Date().getDate()),
	]);

	public uType = userType;
	public mType = modificationType;
	public sType = statusType;

	private announcer = inject(LiveAnnouncer);

	public urlAvatar = '../../../assets/avatar.png';
	public fileScanName = '';
	public filePhotoName = '';
	public urlPhoto = '';
	public urlScan = '';
	public urlNewScan = '';
	public regexImage = /image\/*/;
	public regexScan = /image\/*/ || /application\/pdf/;
	public isValidDocument = true;

	private hobbies: string[] = [
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

	public documents: string[] = ['Legitymacja studencka', 'Dowód osobisty'];

	public dataFormGroupStudent: FormGroup;
	public dataFormGroupOwner: FormGroup;

	public addOnBlur = true;

	public startDate = new Date().getDate();

	constructor(
		private formDir: FormGroupDirective,
		private formBuilder: FormBuilder,
		private router: Router,
		private changeDetectorRef: ChangeDetectorRef,
		private route: ActivatedRoute,
		private profileService: ProfileService,
		private snackBar: MatSnackBar
	) {
		this.filteredHobbies$ = this.hobbyCtrl.valueChanges.pipe(
			startWith(null),
			map((hobby: string | null) =>
				hobby ? this.filter(hobby) : this.hobbies.slice()
			)
		);
		this.dataFormGroupStudent = this.formBuilder.group({
			indexNumber: new FormControl('', Validators.required),
			university: new FormControl('', Validators.required),
			address: new FormControl('', Validators.required),
			phoneNumber: new FormControl('', [
				Validators.required,
				Validators.pattern(
					'^[+]?[(]?[0-9]{2,3}[)]?[-s.]?[0-9]{2,3}[-s.]?[0-9]{2,6}$'
				),
			]),
			hobbies: this.hobbyCtrl,
			socialMedia: this.socialMediaCtrl,
			documentType: new FormControl('', Validators.required),
			validTill: new FormControl(null, [
				Validators.required,
				this.validityTillValidator(),
			]),
		});

		this.dataFormGroupOwner = this.formBuilder.group({
			address: new FormControl('', Validators.required),
			phoneNumber: new FormControl('', [
				Validators.required,
				Validators.pattern(
					'^[+]?[(]?[0-9]{2,3}[)]?[-s.]?[0-9]{2,3}[-s.]?[0-9]{2,6}$'
				),
			]),
			validTill: new FormControl(null, [
				Validators.required,
				this.validityTillValidator(),
			]),
			bankAccount: new FormControl('', [
				Validators.required,
				Validators.pattern('^[0-9]{26}$'),
			]),
		});
	}
	public ngOnInit(): void {
		this.createAccountForm = this.formDir.form;
		if (this.modificationType === modificationType.CREATE) {
			this.dataFormGroupStudent.addControl(
				'yearOfBirth',
				new FormControl('', [
					Validators.required,
					Validators.pattern('^(19|20)[0-9]{2}$'),
					Validators.max(new Date().getFullYear()),
				])
			);
		}

		this.rentId$ = this.route.paramMap.pipe(
			map(params => params.get('id') ?? '')
		);
		this.user$ = this.route.paramMap.pipe(
			map(params => params.get('user')?.toUpperCase() ?? '')
		);

		this.user$
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(user => (this.user = user));

		if (this.user === userType.STUDENT) {
			if (this.modificationType === modificationType.EDIT) {
				this.actualStudent$ = this.rentId$.pipe(
					switchMap(value => this.profileService.getStudent(value))
				);
				this.actualStudent$
					.pipe(takeUntil(this.unsubscribe$))
					.subscribe(student => {
						this.urlPhoto = student.photo ? student.photo : this.urlAvatar;
						this.dataFormGroupStudent
							.get('indexNumber')
							?.setValue(student.indexNumber);
						this.dataFormGroupStudent.get('university')?.setValue(student.university);
						this.dataFormGroupStudent.get('address')?.setValue(student.address);
						this.dataFormGroupStudent
							.get('phoneNumber')
							?.setValue(student.phoneNumber);
						this.myHobbies = student.hobbies;
						this.socialMedias = student.socialMedia;
						this.dataFormGroupStudent
							.get('documentType')
							?.setValue(student.documentType);
						this.urlScan = student.documentScan;
						this.dataFormGroupStudent
							.get('validTill')
							?.setValue(new Date(student.validTill));
					});
			} else {
				this.urlPhoto = this.urlAvatar;
				this.createAccountForm.addControl(
					'userAdditionalData',
					this.dataFormGroupStudent
				);
			}
		}

		if (this.user === userType.OWNER) {
			if (this.modificationType === modificationType.EDIT) {
				this.actualOwner$ = this.rentId$.pipe(
					switchMap(value => this.profileService.getOwner(value))
				);
				this.actualOwner$.pipe(takeUntil(this.unsubscribe$)).subscribe(owner => {
					this.urlPhoto = owner.photo ? owner.photo : this.urlAvatar;
					this.dataFormGroupOwner.get('address')?.setValue(owner.address);
					this.dataFormGroupOwner.get('phoneNumber')?.setValue(owner.phoneNumber);
					this.urlScan = owner.documentScan;
					this.dataFormGroupOwner
						.get('validTill')
						?.setValue(new Date(owner.validTill));
					this.dataFormGroupOwner.get('bankAccount')?.setValue(owner.bankAccount);
				});
			} else {
				this.urlPhoto = this.urlAvatar;
				this.createAccountForm.addControl(
					'userAdditionalData',
					this.dataFormGroupOwner
				);
			}
		}
	}

	public ngOnChanges() {
		if (this.modificationType === modificationType.CREATE) {
			if (this.user === userType.STUDENT) {
				this.actualStudent$ = of({
					...this.actualStudent,
					name: this.name ?? '',
					surname: this.surname ?? '',
					hobbies: [],
					socialMedia: [],
					email: this.email ?? '',
					status: statusType.UNVERIFIED,
				});
				this.createAccountForm.removeControl('userAdditionalData');
				this.createAccountForm.addControl(
					'userAdditionalData',
					this.dataFormGroupStudent
				);
			}
			if (this.user === userType.OWNER) {
				this.actualOwner$ = of({
					...this.actualOwner,
					name: this.name ?? '',
					surname: this.surname ?? '',
					email: this.email ?? '',
				});
				this.createAccountForm.removeControl('userAdditionalData');
				this.createAccountForm.addControl(
					'userAdditionalData',
					this.dataFormGroupOwner
				);
			}
		}
	}

	public add(
		event: MatChipInputEvent,
		items: string[],
		formControl: FormControl
	): void {
		const value = (event.value || '').trim();
		if (value && !items.includes(value.trim())) {
			items.push(value);
		}
		event.chipInput.clear();

		formControl.setValue(null);
	}

	public remove(item: string, items: string[]): void {
		const index = items.indexOf(item);

		if (index >= 0) {
			items.splice(index, 1);

			this.announcer.announce(`Removed ${item}`);
		}
	}

	public edit(socialMedia: string, event: MatChipEditedEvent) {
		const value = event.value.trim();
		if (!value) {
			this.remove(socialMedia, this.socialMedias);
			return;
		}
		const index = this.socialMedias.indexOf(socialMedia);
		if (index >= 0) {
			this.socialMedias[index] = value;
		}
	}

	public selected(event: MatAutocompleteSelectedEvent): void {
		if (!this.myHobbies.includes(event.option.viewValue)) {
			this.myHobbies.push(event.option.viewValue);
		}
		this.hobbiesInput.nativeElement.value = '';
		this.hobbyCtrl.setValue(null);
	}

	private filter(value: string): string[] {
		const filterValue = value.toLowerCase();

		return this.hobbies.filter(hobby =>
			hobby.toLowerCase().includes(filterValue)
		);
	}

	public changeEmail() {
		this.router.navigate(['settings/email-change']);
	}
	public changePassword() {
		this.router.navigate(['settings/password-change']);
	}

	public changeSurvey() {
		if (this.user === userType.OWNER) {
			this.router.navigate(['profile/survey/owner']);
		}
		if (this.user === userType.STUDENT) {
			this.router.navigate(['profile/survey/student']);
		}
	}

	public onSubmit() {
		if (this.dataFormGroupStudent.valid || this.dataFormGroupOwner.valid) {
			if (
				this.urlPhoto !== this.urlAvatar &&
				this.urlNewScan &&
				!this.isValidDocument
			) {
				this.snackBar.open('Pomyślnie zmieniono dane!', 'Zamknij', {
					duration: 2000,
				});
			}
		}
	}

	public getAge(year: string): string {
		const age = new Date().getFullYear() - Number(year);
		if (age > 150 || age < 0) {
			return '';
		}
		return age ? age.toString() : '';
	}

	public checkValidity(date: Date): boolean {
		const endDate = new Date(date);
		const actualDate = new Date();
		const days = Math.floor(
			(endDate.getTime() - actualDate.getTime()) / 1000 / 60 / 60 / 24
		);
		this.isValidDocument = days >= 14 ? true : false;
		return this.isValidDocument;
	}

	public onFileSelected(event: Event, regex: RegExp) {
		const formData = new FormData();
		const fileEvent = (event.target as HTMLInputElement).files;

		if (!fileEvent) {
			return;
		}
		const file: File = fileEvent[0];
		const fileType = file.type;
		if (fileType.match(regex) == null) {
			return;
		}
		const reader = new FileReader();
		reader.readAsDataURL(file);
		reader.onload = () => {
			if (regex === this.regexImage) {
				this.filePhotoName = file.name;
				formData.append(this.filePhotoName, file);
				this.urlPhoto = <string>reader.result;
				this.newPhotoEvent.emit(this.urlPhoto);
			}
			if (regex === this.regexScan) {
				this.fileScanName = file.name;
				formData.append(this.fileScanName, file);
				this.urlScan = <string>reader.result;
				this.urlNewScan = <string>reader.result;
				this.newScanEvent.emit(this.urlNewScan);
			}
			this.changeDetectorRef.detectChanges();
		};
	}

	public validityTillValidator(): ValidatorFn {
		return (control: AbstractControl): ValidationErrors | null => {
			const value = control.value;

			if (!value) {
				return null;
			}

			return !this.checkValidity(value) &&
				this.modificationType === modificationType.EDIT
				? { validityTill: true }
				: null;
		};
	}

	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
