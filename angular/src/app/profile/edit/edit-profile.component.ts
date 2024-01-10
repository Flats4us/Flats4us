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
import { IInterest, IOwner, IStudent, IUser } from '../models/profile.models';
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
	public newPhotoEvent = new EventEmitter<File>();

	@Output()
	public newScanEvent = new EventEmitter<File>();

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
	public hobbyCtrl = new FormControl(null);
	public socialMediaCtrl = new FormControl('');
	public filteredHobbies$?: Observable<IInterest[]>;
	private rentId$?: Observable<string>;
	public user$?: Observable<string>;
	private user = '';
	public myHobbies: IInterest[] = [];
	public socialMedias: string[] = [];
	public actualStudent$?: Observable<IStudent>;
	public actualStudent = {} as IStudent;
	public actualOwner$?: Observable<IOwner>;
	public actualOwner = {} as IOwner;

	public createAccountForm: FormGroup = new FormGroup({});

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
		public profileService: ProfileService,
		private snackBar: MatSnackBar
	) {
		this.filteredHobbies$ = this.hobbyCtrl.valueChanges.pipe(
			startWith(null),
			map((hobby: IInterest | string | null) =>
				hobby
					? this.filter(typeof hobby === 'string' ? hobby : hobby.name)
					: this.profileService.interests.slice()
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
			hobbies: new FormControl(this.myHobbies),
			socialMedia: new FormControl(this.socialMedias),
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
				'birthDate',
				new FormControl(null, [Validators.required, this.validityAgeValidator()])
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

	public addSocialMedia(
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

	public removeSocialMedia(item: string, items: string[]): void {
		const index = items.indexOf(item);

		if (index >= 0) {
			items.splice(index, 1);

			this.announcer.announce(`Removed ${item}`);
		}
	}

	public addHobby(
		event: MatChipInputEvent,
		items: IInterest[],
		formControl: FormControl
	): void {
		const value = event.value;
		if (!items.map(item => item.name).includes(value)) {
			items.push(
				this.profileService.interests.find(hobby => hobby.name === value) ??
					({} as IInterest)
			);
		}
		event.chipInput.clear();

		formControl.setValue(null);
	}

	public removeHobby(item: IInterest, items: IInterest[]): void {
		const index = items.indexOf(item);

		if (index >= 0) {
			items.splice(index, 1);

			this.announcer.announce(`Removed ${item}`);
		}
	}

	public editSocialMedia(socialMedia: string, event: MatChipEditedEvent) {
		const value = event.value.trim();
		if (!value) {
			this.removeSocialMedia(socialMedia, this.socialMedias);
			return;
		}
		const index = this.socialMedias.indexOf(socialMedia);
		if (index >= 0) {
			this.socialMedias[index] = value;
		}
	}

	public selected(event: MatAutocompleteSelectedEvent): void {
		if (
			!this.myHobbies.map(item => item.name).includes(event.option.value.name)
		) {
			this.myHobbies.push(event.option.value);
		}
		this.hobbiesInput.nativeElement.value = '';
		this.hobbyCtrl.setValue(null);
	}

	private filter(value: string): IInterest[] {
		const valueLowerCase = value.toLowerCase();
		return this.profileService.interests.filter(hobby =>
			hobby.name.toLowerCase().includes(valueLowerCase)
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
				this.snackBar
					.open('Pomyślnie zmieniono dane!', 'Zamknij', {
						duration: 2000,
					})
					.afterDismissed()
					.pipe(takeUntil(this.unsubscribe$))
					.subscribe(() => {
						this.router.navigate(['/']);
					});
			}
		}
	}

	public getAge(date: Date): string {
		const age = new Date().getFullYear() - new Date(date).getFullYear();
		if (age > 150 || age < 0) {
			return '';
		}
		return age ? age.toString() : '';
	}

	public toLowerCase(type: statusType): string {
		return type.toLowerCase();
	}

	public checkValidityTill(date: Date): boolean {
		const endDate = new Date(date);
		const actualDate = new Date();
		const days = Math.floor(
			(endDate.getTime() - actualDate.getTime()) / 1000 / 60 / 60 / 24
		);
		this.isValidDocument = days > 0 ? true : false;
		return this.isValidDocument;
	}

	public checkValidityAge(date: Date): boolean {
		const endDate = new Date(date);
		const actualDate = new Date();
		const years = actualDate.getFullYear() - endDate.getFullYear();
		const isValidAge = years >= 18 && years <= 150 ? true : false;
		return isValidAge;
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
				this.newPhotoEvent.emit(file);
			}
			if (regex === this.regexScan) {
				this.fileScanName = file.name;
				formData.append(this.fileScanName, file);
				this.urlScan = <string>reader.result;
				this.urlNewScan = <string>reader.result;
				this.newScanEvent.emit(file);
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

			return !this.checkValidityTill(value) ? { validityTill: true } : null;
		};
	}

	public validityAgeValidator(): ValidatorFn {
		return (control: AbstractControl): ValidationErrors | null => {
			const value = control.value;

			if (!value) {
				return null;
			}

			return !this.checkValidityAge(value) ? { validityAge: true } : null;
		};
	}

	public toTypeOwner(value: Observable<IUser>): Observable<IOwner> {
		return value as Observable<IOwner>;
	}

	public toTypeStudent(value: Observable<IUser>): Observable<IStudent> {
		return value as Observable<IStudent>;
	}

	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
