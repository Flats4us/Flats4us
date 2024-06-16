import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	EventEmitter,
	Input,
	OnInit,
	Output,
} from '@angular/core';
import { Observable, concat, concatMap, of } from 'rxjs';
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

	public editedPhotoFile: File | undefined;
	public editedScanFile: File | undefined;
	public formData = new FormData();

	public dataFormGroupStudent: FormGroup;
	public dataFormGroupOwner: FormGroup;
	public dataFormGroupModerator: FormGroup;

	public addOnBlur = true;

	public startDate = new Date().getDate();

	public setLocalDate = setLocalDate;

	public ifConfidentialData = false;

	public menuOptions: IMenuOptions[] = [
		{ option: 'editEmail', description: 'Profile-edit.options-email' },
		{ option: 'changePassword', description: 'Profile-edit.change-password' },
		{ option: 'editSurvey', description: 'Profile-edit.edit-survey' },
	];

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
			documentExpireDate: new FormControl(null, [
				Validators.required,
				this.validityTillValidator(),
			]),
			bankAccount: new FormControl('', [
				Validators.required,
				Validators.pattern('^[0-9]{26}$'),
			]),
			documentNumber: new FormControl('', Validators.required),
		});

		this.dataFormGroupModerator = this.formBuilder.group({
			phoneNumber: new FormControl('', [
				Validators.required,
				Validators.pattern(
					'^[+]?[(]?[0-9]{2,3}[)]?[-s.]?[0-9]{2,3}[-s.]?[0-9]{2,6}$'
				),
			]),
		});
	}
	public ngOnInit(): void {
		this.createAccountForm = this.formDir.form;
		if (this.modificationType === ModificationType.CREATE) {
			this.dataFormGroupStudent.addControl(
				'birthDate',
				new FormControl(null, [Validators.required, validityAgeValidator()])
			);
			this.dataFormGroupStudent.addControl(
				'documentType',
				new FormControl(null, Validators.required)
			);
			this.dataFormGroupOwner.addControl(
				'documentType',
				new FormControl(null, Validators.required)
			);
		}
		if (this.modificationType === ModificationType.EDIT) {
			this.actualUser$ = this.profileService.getActualProfile();
			this.actualUser$.pipe(this.untilDestroyed()).subscribe(user => {
				this.urlPhoto = this.baseUrl + user.profilePicture?.path;
				this.urlScan = this.baseUrl + user.document?.path;
				if (user.userType === 1) {
					this.selectedHobbies = user.interests;
					this.socialMedias = user.links;
					this.dataFormGroupStudent.patchValue(user);
					this.dataFormGroupStudent.get('documentExpireDate')?.disable();
					this.dataFormGroupStudent.get('studentNumber')?.disable();
					this.dataFormGroupStudent.get('university')?.disable();
				} else if (user.userType === 2) {
					this.dataFormGroupModerator.patchValue(user);
					if (!user?.profilePicture?.path) {
						this.urlPhoto = this.urlAvatar;
					}
				} else {
					this.dataFormGroupOwner.patchValue(user);
					this.dataFormGroupOwner.get('documentExpireDate')?.disable();
					this.dataFormGroupOwner.get('bankAccount')?.disable();
					this.dataFormGroupOwner.get('documentNumber')?.disable();
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

	public lockUnlockData(userType: number) {
		this.editedScanFile = undefined;
		this.urlNewScan = '';
		this.ifConfidentialData = !this.ifConfidentialData;
		if (this.ifConfidentialData) {
			this.snackBar.open(
				this.translate.instant('Profile-edit.confidential-info'),
				this.translate.instant('close'),
				{ duration: 10000 }
			);
			if (userType === 1) {
				this.dataFormGroupStudent.get('documentExpireDate')?.enable();
				this.dataFormGroupStudent.get('studentNumber')?.enable();
				this.dataFormGroupStudent.get('university')?.enable();
			} else {
				this.dataFormGroupOwner.get('documentExpireDate')?.enable();
				this.dataFormGroupOwner.get('bankAccount')?.enable();
				this.dataFormGroupOwner.get('documentNumber')?.enable();
			}
		} else {
			if (userType === 1) {
				this.dataFormGroupStudent.get('documentExpireDate')?.disable();
				this.dataFormGroupStudent.get('studentNumber')?.disable();
				this.dataFormGroupStudent.get('university')?.disable();
			} else {
				this.dataFormGroupOwner.get('documentExpireDate')?.disable();
				this.dataFormGroupOwner.get('bankAccount')?.disable();
				this.dataFormGroupOwner.get('documentNumber')?.disable();
			}
		}
	}

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

	public onSubmit(userType: number) {
		if (this.modificationType === ModificationType.EDIT) {
			if (this.editedPhotoFile) {
				this.formData.append('ProfilePicture', this.editedPhotoFile);
			}
			if (this.ifConfidentialData && this.editedScanFile) {
				this.formData.append('Document', this.editedScanFile);
			}
			if (this.isValidDocument) {
				this.editUserData(userType)
					.pipe(
						this.untilDestroyed(),
						concatMap(result => {
							if (this.editedPhotoFile || this.ifConfidentialData) {
								return concat(
									this.profileService.addProfileFiles(this.formData),
									of(result)
								);
							}
							return of(result);
						})
					)
					.subscribe({
						next: () =>
							this.snackBar.open(
								this.translate.instant('Profile-edit.info1'),
								this.translate.instant('close'),
								{
									duration: 10000,
								}
							),
						error: () => {
							this.snackBar.open(
								this.translate.instant('Profile-edit.info2'),
								this.translate.instant('close'),
								{ duration: 10000 }
							);
						},
						complete: () => {
							this.actualUser$ = this.profileService.getActualProfile();
							window.location.reload();
						},
					});
			}
		}
	}

	private editUserData(userType: number) {
		if (!this.ifConfidentialData) {
			if (userType === 1) {
				return this.profileService.editProfileStudent({
					address: this.dataFormGroupStudent.value.address,
					phoneNumber: this.dataFormGroupStudent.value.phoneNumber,
					links: this.dataFormGroupStudent.value.links,
					interestIds: this.dataFormGroupStudent.value.interestIds.map(
						(hobby: IInterest) => hobby.interestId
					),
				});
			} else if (userType === 2) {
				return this.profileService.editProfileModerator({
					phoneNumber: this.dataFormGroupModerator.value.phoneNumber,
				});
			} else {
				return this.profileService.editProfileOwner({
					address: this.dataFormGroupOwner.value.address,
					phoneNumber: this.dataFormGroupOwner.value.phoneNumber,
				});
			}
		} else {
			if (userType === 1) {
				return this.profileService.editProfileStudent({
					address: this.dataFormGroupStudent.value.address,
					phoneNumber: this.dataFormGroupStudent.value.phoneNumber,
					university: this.dataFormGroupStudent.value.university,
					links: this.dataFormGroupStudent.value.links,
					interestIds: this.dataFormGroupStudent.value.interestIds.map(
						(hobby: IInterest) => hobby.interestId
					),
					documentExpireDate: this.dataFormGroupStudent.value.documentExpireDate,
					studentNumber: this.dataFormGroupStudent.value.studentNumber,
				});
			} else if (userType === 2) {
				return this.profileService.editProfileModerator({
					phoneNumber: this.dataFormGroupModerator.value.phoneNumber,
				});
			} else {
				return this.profileService.editProfileOwner({
					address: this.dataFormGroupOwner.value.address,
					phoneNumber: this.dataFormGroupOwner.value.phoneNumber,
					documentNumber: this.dataFormGroupStudent.value.documentNumber,
					bankAccount: this.dataFormGroupStudent.value.bankAccount,
					documentExpireDate: this.dataFormGroupOwner.value.documentExpireDate,
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
		const today = new Date();
		const actualDate = new Date(
			today.getFullYear(),
			today.getMonth(),
			today.getDate(),
			today.getHours(),
			today.getMinutes() - today.getTimezoneOffset()
		);
		const days = Math.floor(
			(endDate.getTime() - actualDate.getTime()) / 1000 / 60 / 60 / 24
		);
		this.isValidDocument = days >= 0;
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
				this.editedPhotoFile = file;
				this.newPhotoEvent.emit(file);
			}
			if (regex === this.regexScan) {
				this.fileScanName = file.name;
				formData.append(this.fileScanName, file);
				this.urlScan = <string>reader.result;
				this.urlNewScan = <string>reader.result;
				this.editedScanFile = file;
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
