import {
	ChangeDetectionStrategy,
	Component,
	OnDestroy,
	OnInit,
	ViewChild,
} from '@angular/core';
import { modificationType, userType } from '../models/types';
import { MatStepper } from '@angular/material/stepper';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject, map, takeUntil } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ProfileService } from '../services/profile.service';
import { IAddOwner, IAddStudent } from '../models/profile.models';
import { IUser } from '@shared/models/auth.models';
import { AuthService } from '@shared/services/auth.service';

@Component({
	selector: 'app-profile-create',
	templateUrl: './create-profile.component.html',
	styleUrls: ['./create-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateProfileComponent implements OnInit, OnDestroy {
	@ViewChild('stepper')
	public stepper: MatStepper | undefined;

	public uType = userType;
	public mType = modificationType;
	public photo: File = {} as File;
	public scan: File = {} as File;
	public photoName = this.photo.name;
	public scanName = this.scan.name;
	private formData = new FormData();
	public studentToAdd = {} as IAddStudent;
	public ownerToAdd = {} as IAddOwner;
	private readonly unsubscribe$: Subject<void> = new Subject();
	private user = '';
	private modificationType = '';

	public user$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);
	public modificationType$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('modificationType')?.toUpperCase() ?? '')
	);

	public registerForm = this.formBuilder.group({});

	public createAccountForm = this.formBuilder.group({});

	public surveyForm = this.formBuilder.group({});

	constructor(
		private formBuilder: FormBuilder,
		private route: ActivatedRoute,
		private snackBar: MatSnackBar,
		private router: Router,
		private profileService: ProfileService,
		private authService: AuthService
	) {}
	public ngOnInit(): void {
		this.profileService
			.readAllInterests()
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe();
		this.user$
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(user => (this.user = user));
		this.modificationType$
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(type => (this.modificationType = type));
	}

	public onAdd() {
		if (this.user === userType.STUDENT) {
			this.studentToAdd = {
				name: this.registerForm.get('name')?.value ?? '',
				surname: this.registerForm.get('surname')?.value ?? '',
				address:
					this.createAccountForm.get('userAdditionalData')?.get('address')?.value ??
					'',
				password: this.registerForm.get('password')?.value ?? '',
				email: this.registerForm.get('email')?.value ?? '',
				phoneNumber:
					this.createAccountForm.get('userAdditionalData')?.get('phoneNumber')
						?.value ?? '',
				documentType:
					this.createAccountForm.get('userAdditionalData')?.get('documentType')
						?.value ?? 0,
				documentExpireDate:
					this.createAccountForm.get('userAdditionalData')?.get('validTill')
						?.value ?? new Date(),
				birthDate:
					this.createAccountForm.get('userAdditionalData')?.get('birthDate')
						?.value ?? new Date(),
				studentNumber:
					this.createAccountForm.get('userAdditionalData')?.get('indexNumber')
						?.value ?? '',
				university:
					this.createAccountForm.get('userAdditionalData')?.get('university')
						?.value ?? '',
				links:
					this.createAccountForm.get('userAdditionalData')?.get('socialMedia')
						?.value ?? [],
				party: this.surveyForm.get('survey')?.get('party')?.value ?? 0,
				tidiness: this.surveyForm.get('survey')?.get('tidiness')?.value ?? 0,
				smoking: this.surveyForm.get('survey')?.get('smoking')?.value ?? false,
				sociability:
					this.surveyForm.get('survey')?.get('sociability')?.value ?? false,
				animals: this.surveyForm.get('survey')?.get('animals')?.value ?? false,
				vegan: this.surveyForm.get('survey')?.get('vegan')?.value ?? false,
				lookingForRoommate:
					this.surveyForm.get('survey')?.get('lookingForRoommate')?.value ?? false,
				maxNumberOfRoommates:
					this.surveyForm.get('survey')?.get('maxNumberOfRoommates')?.value ?? 0,
				roommateGender:
					this.surveyForm.get('survey')?.get('roommateGender')?.value ?? 0,
				minRoommateAge:
					this.surveyForm.get('survey')?.get('minRoommateAge')?.value ?? 0,
				maxRoommateAge:
					this.surveyForm.get('survey')?.get('maxRoommateAge')?.value ?? 0,
				interests:
					this.createAccountForm.get('userAdditionalData')?.get('hobbies')?.value ??
					[],
			};
		}
		if (this.user === userType.OWNER) {
			this.ownerToAdd = {
				name: this.registerForm.get('name')?.value ?? '',
				surname: this.registerForm.get('surname')?.value ?? '',
				address:
					this.createAccountForm.get('userAdditionalData')?.get('address')?.value ??
					'',
				password: this.registerForm.get('password')?.value ?? '',
				email: this.registerForm.get('email')?.value ?? '',
				phoneNumber:
					this.createAccountForm.get('userAdditionalData')?.get('phoneNumber')
						?.value ?? '',
				documentType:
					this.createAccountForm.get('userAdditionalData')?.get('documentType')
						?.value ?? 0,
				documentExpireDate:
					this.createAccountForm.get('userAdditionalData')?.get('validTill')
						?.value ?? new Date(),
				bankAccount:
					this.createAccountForm.get('userAdditionalData')?.get('bankAccount')
						?.value ?? '',
				documentNumber: 'placeholder',
			};
		}
		this.formData.append('ProfilePicture', this.photo);
		this.formData.append('Document', this.scan);
	}

	public setPhoto(photo: File) {
		this.photo = photo;
		this.photoName = photo.name;
	}

	public setScan(scan: File) {
		this.scan = scan;
		this.scanName = scan.name;
	}

	public onSubmit() {
		if (
			this.registerForm.valid &&
			this.createAccountForm.valid &&
			this.photoName &&
			this.scanName
		) {
			this.onAdd();
			if (this.modificationType === modificationType.CREATE) {
				if (this.user === userType.STUDENT) {
					this.profileService
						.addProfileStudent(this.studentToAdd)
						.pipe(takeUntil(this.unsubscribe$))
						.subscribe(() =>
							this.authService
								.login({
									email: this.studentToAdd.email,
									password: this.studentToAdd.password,
								} as IUser)
								.pipe(takeUntil(this.unsubscribe$))
								.subscribe(() =>
									this.profileService
										.addProfileFiles(this.formData)
										.pipe(takeUntil(this.unsubscribe$))
										.subscribe()
								)
						);
				}
				if (this.user === userType.OWNER) {
					this.profileService
						.addProfileOwner(this.ownerToAdd)
						.pipe(takeUntil(this.unsubscribe$))
						.subscribe(() =>
							this.authService
								.login({
									email: this.ownerToAdd.email,
									password: this.ownerToAdd.password,
								} as IUser)
								.pipe(takeUntil(this.unsubscribe$))
								.subscribe(() =>
									this.profileService
										.addProfileFiles(this.formData)
										.pipe(takeUntil(this.unsubscribe$))
										.subscribe()
								)
						);
				}
			}
			this.snackBar
				.open('Pomyślnie utworzono konto!', 'Zamknij', {
					duration: 2000,
				})
				.afterDismissed()
				.pipe(takeUntil(this.unsubscribe$))
				.subscribe(() => {
					this.router.navigate(['/']);
				});
		}
	}
	public ngOnDestroy(): void {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
