import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	EventEmitter,
	Input,
	OnChanges,
	OnInit,
	Output,
	SimpleChanges,
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
import { IInterest, IOwner, IStudent } from '../models/profile.models';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { ActivatedRoute, Router } from '@angular/router';
import { ProfileService } from '../services/profile.service';
import { MatChipEditedEvent, MatChipInputEvent } from '@angular/material/chips';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { ModificationType, StatusType, UserType } from '../models/types';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-profile-edit',
	templateUrl: './edit-profile.component.html',
	styleUrls: ['./edit-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditProfileComponent implements OnInit, OnChanges {
	@Output()
	public newPhotoEvent = new EventEmitter<File>();

	@Output()
	public newScanEvent = new EventEmitter<File>();

	@Input()
	public name = '';

	@Input()
	public surname = '';

	@Input()
	public email = '';

	@Input()
	public modificationType = ModificationType.EDIT;

	private readonly unsubscribe$: Subject<void> = new Subject();

	public separatorKeysCodes: number[] = [ENTER, COMMA];
	public hobbyCtrl = new FormControl(null);
	public socialMediaCtrl = new FormControl('');
	public filteredHobbies$: Observable<IInterest[]>;
	private userId$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('id') ?? '')
	);
	private user = '';
	public user$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);
	public myHobbies: IInterest[] = [];
	public socialMedias: string[] = [];
	public actualOwner$: Observable<IOwner> | undefined;
	public actualStudent$: Observable<IStudent> | undefined;
	public actualStudent: IStudent | undefined;
	public actualOwner: IOwner | undefined;

	public createAccountForm?: FormGroup;

	public uType = UserType;
	public mType = ModificationType;
	public sType = StatusType;

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
			map(hobby =>
				hobby ? this.filter(hobby) : this.profileService.interests.slice()
			)
		);
		this.dataFormGroupStudent = this.formBuilder.group({
			studentNumber: new FormControl('', Validators.required),
			university: new FormControl('', Validators.required),
			address: new FormControl('', Validators.required),
			phoneNumber: new FormControl('', [
				Validators.required,
				Validators.pattern(
					'^[+]?[(]?[0-9]{2,3}[)]?[-s.]?[0-9]{2,3}[-s.]?[0-9]{2,6}$'
				),
			]),
			interests: new FormControl(this.myHobbies),
			links: new FormControl(this.socialMedias),
			documentType: new FormControl(''),
			documentExpireDate: new FormControl(null, [
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
			documentType: new FormControl('', Validators.required),
			documentExpireDate: new FormControl(null, [
				Validators.required,
				this.validityTillValidator(),
			]),
			bankAccount: new FormControl('', [
				Validators.required,
				Validators.pattern('^[0-9]{26}$'),
			]),
			documentNumber: new FormControl('placeholder', Validators.required),
		});
	}
	public ngOnInit(): void {
		this.createAccountForm = this.formDir.form;
		if (this.modificationType === ModificationType.CREATE) {
			this.dataFormGroupStudent.addControl(
				'birthDate',
				new FormControl(null, [Validators.required, this.validityAgeValidator()])
			);
		}

		this.user$
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(user => (this.user = user));

		if (this.user === UserType.STUDENT) {
			if (this.modificationType === ModificationType.EDIT) {
				this.actualStudent$ = this.userId$.pipe(
					switchMap(value => this.profileService.getStudent(value))
				);
				this.actualStudent$
					.pipe(takeUntil(this.unsubscribe$))
					.subscribe(student => {
						this.urlPhoto = student.photo ? student.photo : this.urlAvatar;
						this.dataFormGroupStudent.patchValue(student);
					});
			} else {
				this.urlPhoto = this.urlAvatar;
				this.createAccountForm.addControl(
					'userAdditionalData',
					this.dataFormGroupStudent
				);
			}
		}

		if (this.user === UserType.OWNER) {
			if (this.modificationType === ModificationType.EDIT) {
				this.actualOwner$ = this.userId$.pipe(
					switchMap(value => this.profileService.getOwner(value))
				);
				this.actualOwner$.pipe(takeUntil(this.unsubscribe$)).subscribe(owner => {
					this.urlPhoto = owner.photo ? owner.photo : this.urlAvatar;
					this.dataFormGroupOwner.patchValue(owner);
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

	public ngOnChanges(changes: SimpleChanges) {
		if (this.modificationType === ModificationType.CREATE) {
			if (changes['name'] || changes['surname'] || changes['email']) {
				if (this.user === UserType.STUDENT) {
					this.actualStudent$ = of({
						...(this.actualStudent as IStudent),
						name: this.name ?? '',
						surname: this.surname ?? '',
						email: this.email ?? '',
					});
				}
				if (this.user === UserType.OWNER) {
					this.actualOwner$ = of({
						...(this.actualOwner as IOwner),
						name: this.name ?? '',
						surname: this.surname ?? '',
						email: this.email ?? '',
					});
				}
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
		this.hobbyCtrl.setValue(null);
	}

	private filter(value: IInterest): IInterest[] {
		const valueLowerCase = value.name.toLowerCase();
		return this.profileService.interests.filter(hobby =>
			hobby.name.toLowerCase().includes(valueLowerCase)
		);
	}

	public changeEmail() {
		this.router.navigate(['/settings', 'email-change']);
	}
	public changePassword() {
		this.router.navigate(['/settings', 'password-change']);
	}

	public changeSurvey() {
		if (this.user === UserType.OWNER) {
			this.router.navigate(['/profile', 'survey', 'owner']);
		}
		if (this.user === UserType.STUDENT) {
			this.router.navigate(['/profile', 'survey', 'student']);
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

	public toLowerCase(type: StatusType): string {
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

	public toTypeOwner(value: Observable<IOwner>): Observable<IOwner> {
		return value as Observable<IOwner>;
	}

	public toTypeStudent(value: Observable<IStudent>): Observable<IStudent> {
		return value as Observable<IStudent>;
	}
}
