import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	EventEmitter,
	Input,
	OnInit,
	Output,
} from '@angular/core';
import { Observable } from 'rxjs';
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
import { IInterest, IUserProfile } from '../models/profile.models';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { Router } from '@angular/router';
import { ProfileService } from '../services/profile.service';
import { MatChipEditedEvent, MatChipInputEvent } from '@angular/material/chips';
import { ModificationType, StatusType, UserType } from '../models/types';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseComponent } from '@shared/components/base/base.component';
import { environment } from 'src/environments/environment.prod';
import { IMenuOptions } from 'src/app/rents/models/rents.models';
import { validityAgeValidator } from '@shared/utils/validators';
import { setLocalDate } from '@shared/utils/functions';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-profile-edit',
	templateUrl: './edit-profile.component.html',
	styleUrls: ['./edit-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditProfileComponent extends BaseComponent implements OnInit {
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

	@Input()
	public user = '';

	protected baseUrl = environment.apiUrl.replace('/api', '/');

	public separatorKeysCodes: number[] = [ENTER, COMMA];
	public selectedHobbies: IInterest[] = [];
	public socialMediaCtrl = new FormControl('');
	public filteredHobbies$: Observable<IInterest[]> =
		this.profileService.getInterests();
	public socialMedias: string[] = [];
	public actualUser$?: Observable<IUserProfile>;

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

	public setLocalDate = setLocalDate;

	public menuOptions: IMenuOptions[] = [
		{ option: 'editEmail', description: 'Profile-edit.options-email' },
		{ option: 'changePassword', description: 'Profile-edit.change-password' },
		{ option: 'editSurvey', description: 'Profile-edit.edit-survey' },
	];

	public descriptionsMap: Map<string, string> = new Map([
		['read', 'Wczytaj skan dokumentu'],
		['change', 'Zmień skan dokumentu'],
	]);

	constructor(
		private formDir: FormGroupDirective,
		private formBuilder: FormBuilder,
		private router: Router,
		private changeDetectorRef: ChangeDetectorRef,
		public profileService: ProfileService,
		private snackBar: MatSnackBar,
		private translate: TranslateService
	) {
		super();
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
			interestIds: new FormControl([]),
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
			documentNumber: new FormControl('placeholder'),
		});
	}
	public ngOnInit(): void {
		this.createAccountForm = this.formDir.form;
		if (this.modificationType === ModificationType.CREATE) {
			this.dataFormGroupStudent.addControl(
				'birthDate',
				new FormControl(null, [Validators.required, validityAgeValidator()])
			);
		}
		if (this.modificationType === ModificationType.EDIT) {
			this.actualUser$ = this.profileService.getActualProfile();
			this.actualUser$.pipe(this.untilDestroyed()).subscribe(user => {
				this.urlPhoto = this.baseUrl + user.profilePicture.path;
				this.urlScan = this.baseUrl + user.document.path;
				if (user.userType === 1) {
					this.selectedHobbies = user.interests;
					this.socialMedias = user.links;
					this.dataFormGroupStudent.patchValue(user);
					this.dataFormGroupStudent.get('studentNumber')?.disable();
				} else {
					this.dataFormGroupOwner.patchValue(user);
					this.dataFormGroupOwner.get('documentExpireDate')?.disable();
					this.dataFormGroupOwner.get('bankAccount')?.disable();
				}
			});
		}
		if (this.modificationType === ModificationType.CREATE) {
			this.urlPhoto = this.urlAvatar;
			if (this.user === UserType.STUDENT) {
				this.createAccountForm.addControl(
					'userAdditionalData',
					this.dataFormGroupStudent
				);
			} else if (this.user === UserType.OWNER) {
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

	public changeEmail() {
		this.router.navigate(['settings', 'email-change']);
	}
	public changePassword() {
		this.router.navigate(['settings', 'password-change']);
	}

	public changeSurvey() {
		this.router.navigate(['profile', 'survey', 'student']);
	}

	// public getTooltipScan(): string {
	// 	if (!this.urlNewScan || this.modificationType === ModificationType.CREATE) {
	// 		return 'Wczytaj skan dokumentu';
	// 	}
	// 	if (this.urlNewScan || this.modificationType === ModificationType.EDIT) {
	// 		return 'Zmień skan dokumentu';
	// 	} else {
	// 		return 'Wczytaj skan dokumentu';
	// 	}
	// }

	public onSelect(menuOption: IMenuOptions) {
		switch (menuOption.option) {
			case 'editEmail': {
				this.changeEmail();
				break;
			}
			case 'changePassword': {
				this.changePassword();
				break;
			}
			case 'editSurvey': {
				this.changeSurvey();
				break;
			}
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
					.open(
						this.translate.instant('Profile-edit.info1'),
						this.translate.instant('Profile-edit.close'),
						{
							duration: 2000,
						}
					)
					.afterDismissed()
					.pipe(this.untilDestroyed())
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
			this.changeDetectorRef.markForCheck();
		};
	}

	public hobbyComparisonFunction = function (
		option: IInterest,
		value: IInterest
	): boolean {
		return value.interestId === option.interestId;
	};

	public validityTillValidator(): ValidatorFn {
		return (control: AbstractControl): ValidationErrors | null => {
			const value = control.value;

			if (!value) {
				return null;
			}

			return !this.checkValidityTill(value) ? { validityTill: true } : null;
		};
	}
}
