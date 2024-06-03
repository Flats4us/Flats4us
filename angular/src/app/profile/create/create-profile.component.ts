import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	ViewChild,
} from '@angular/core';
import { ModificationType, UserType } from '../models/types';
import { MatStepper } from '@angular/material/stepper';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, concatMap, map, of, switchMap } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProfileService } from '../services/profile.service';
import { IAddOwner, IAddStudent, IInterest } from '../models/profile.models';
import { AuthService } from '@shared/services/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { matchPasswordValidator } from '@shared/utils/validators';
import { UserService } from '@shared/services/user.service';

@Component({
	selector: 'app-profile-create',
	templateUrl: './create-profile.component.html',
	styleUrls: ['./create-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateProfileComponent extends BaseComponent implements OnInit {
	@ViewChild('stepper')
	public stepper: MatStepper | undefined;

	public uType = UserType;
	public mType = ModificationType;
	public photo?: File;
	public scan?: File;
	public photoName?: string;
	public scanName?: string;
	private formData = new FormData();
	public studentToAdd?: IAddStudent;
	public ownerToAdd?: IAddOwner;
	public user = '';
	public modificationType = '';
	public emailExist$: Observable<boolean> = of(false);
	private studentFormGroup: FormGroup = this.formBuilder.group({
		name: new FormControl(''),
		surname: new FormControl(''),
		address: new FormControl(''),
		password: new FormControl(''),
		email: new FormControl(''),
		phoneNumber: new FormControl(''),
		documentType: new FormControl(''),
		documentExpireDate: new FormControl(null),
		birthDate: new FormControl(null),
		studentNumber: new FormControl(''),
		university: new FormControl(''),
		links: new FormControl([]),
		party: new FormControl(1),
		tidiness: new FormControl(1),
		smoking: new FormControl(false),
		sociability: new FormControl(false),
		animals: new FormControl(false),
		vegan: new FormControl(false),
		lookingForRoommate: new FormControl(false),
		maxNumberOfRoommates: new FormControl(null),
		roommateGender: new FormControl(null),
		minRoommateAge: new FormControl(null),
		maxRoommateAge: new FormControl(null),
		city: new FormControl(''),
		interestIds: new FormControl([]),
	});
	private ownerFormGroup: FormGroup = this.formBuilder.group({
		name: new FormControl(''),
		surname: new FormControl(''),
		address: new FormControl(''),
		password: new FormControl(''),
		email: new FormControl(''),
		phoneNumber: new FormControl(''),
		documentType: new FormControl(''),
		documentExpireDate: new FormControl(null),
		bankAccount: new FormControl(''),
		documentNumber: new FormControl('placeholder'),
	});

	public user$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);
	public modificationType$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('modificationType')?.toUpperCase() ?? '')
	);

	public registerForm = new FormGroup(
		{
			name: new FormControl('', Validators.required),
			surname: new FormControl('', Validators.required),
			email: new FormControl('', [Validators.required, Validators.email]),
			password: new FormControl('', [
				Validators.required,
				Validators.pattern('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,50}$'),
			]),
			confirmPassword: new FormControl('', [Validators.required]),
		},
		{ validators: [matchPasswordValidator] }
	);

	public createAccountForm = new FormGroup({});

	public surveyForm = new FormGroup({});

	constructor(
		private route: ActivatedRoute,
		private snackBar: MatSnackBar,
		private router: Router,
		private profileService: ProfileService,
		private authService: AuthService,
		private formBuilder: FormBuilder,
		private userService: UserService
	) {
		super();
	}
	public ngOnInit(): void {
		this.profileService
			.readAllInterests()
			.pipe(this.untilDestroyed())
			.subscribe();
		this.user$.pipe(this.untilDestroyed()).subscribe(user => (this.user = user));
		this.modificationType$
			.pipe(this.untilDestroyed())
			.subscribe(type => (this.modificationType = type));
		this.emailExist$ = !this.registerForm.get('email')?.hasError('email')
			? this.registerForm
					.get('email')
					?.valueChanges.pipe(
						switchMap(value => this.checkIfEmailExist(value ?? ''))
					) ?? of(false)
			: of(false);
	}

	public onAdd() {
		if (this.user === UserType.STUDENT) {
			this.studentFormGroup.patchValue(this.registerForm.value);
			this.studentFormGroup.patchValue(
				this.createAccountForm.get('userAdditionalData')?.value ??
					new FormControl('')
			);
			this.studentFormGroup.get('interestIds')?.setValue(
				this.createAccountForm
					.get('userAdditionalData')
					?.get('interestIds')
					?.getRawValue()
					.map((hobby: IInterest) => hobby.interestId)
			);
			this.studentFormGroup.patchValue(
				this.surveyForm.get('survey')?.value ?? new FormControl('')
			);
		}
		if (this.user === UserType.OWNER) {
			this.ownerFormGroup.patchValue(this.registerForm.value);
			this.ownerFormGroup.patchValue(
				this.createAccountForm.get('userAdditionalData')?.value ??
					new FormControl('')
			);
		}
		this.formData.append('ProfilePicture', this.photo as File);
		this.formData.append('Document', this.scan as File);
	}

	public setPhoto(photo: File) {
		this.photo = photo;
		this.photoName = photo.name;
	}

	public setScan(scan: File) {
		this.scan = scan;
		this.scanName = scan.name;
	}

	public checkIfEmailExist(email: string): Observable<boolean> {
		return this.userService
			.checkIfEmailExist(email)
			.pipe(map(result => result.result));
	}

	public onSubmit() {
		if (
			this.registerForm.valid &&
			this.createAccountForm.valid &&
			this.surveyForm.valid &&
			this.photoName &&
			this.scanName
		) {
			this.onAdd();
			if (this.modificationType === ModificationType.CREATE) {
				if (this.user === UserType.STUDENT) {
					this.authService
						.addProfileStudent(this.studentFormGroup.value)
						.pipe(
							this.untilDestroyed(),
							concatMap(() => this.profileService.addProfileFiles(this.formData))
						)
						.subscribe({
							next: () =>
								this.snackBar.open('Pomyślnie utworzono konto!', 'Zamknij', {
									duration: 10000,
								}),
							error: () => {
								this.snackBar.open(
									'Nie udało się utworzyć konta Studenta. Spróbuj ponownie.',
									'Zamknij',
									{ duration: 10000 }
								);
							},
							complete: () => this.router.navigate(['/']),
						});
				}
				if (this.user === UserType.OWNER) {
					this.authService
						.addProfileOwner(this.ownerFormGroup.value)
						.pipe(
							this.untilDestroyed(),
							concatMap(() => this.profileService.addProfileFiles(this.formData))
						)
						.subscribe({
							next: () =>
								this.snackBar.open(
									'Pomyślnie utworzono konto Właściciela!',
									'Zamknij',
									{
										duration: 10000,
									}
								),
							error: () => {
								this.snackBar.open(
									'Nie udało się utworzyć konta Właściciela. Spróbuj ponownie.',
									'Zamknij',
									{ duration: 10000 }
								);
							},
							complete: () => this.router.navigate(['/']),
						});
				}
			}
		}
	}
}
