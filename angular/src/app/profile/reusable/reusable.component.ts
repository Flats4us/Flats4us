import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	ElementRef,
	Input,
	OnDestroy,
	OnInit,
	ViewChild,
	inject,
} from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
	Observable,
	map,
	startWith,
	switchMap,
	of,
	Subject,
	takeUntil,
} from 'rxjs';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipEditedEvent, MatChipInputEvent } from '@angular/material/chips';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { modificationType, userType } from '../models/types';
import { IOwner, IStudent } from '../models/profile.models';
import { ProfileService } from '../services/profile.service';

@Component({
	selector: 'app-profile-reusable',
	templateUrl: './reusable.component.html',
	styleUrls: ['./reusable.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ReusableProfileComponent implements OnInit, OnDestroy {
	@Input()
	public title = 'Edycja profilu';

	@Input()
	public createId = 2;

	@Input()
	public userType = userType.STUDENT;

	@Input()
	public modificationType = modificationType.EDIT;

	@ViewChild('hobbiesInput')
	public hobbiesInput!: ElementRef<HTMLInputElement>;

	public uType = userType;
	public mType = modificationType;

	private readonly unsubscribe$: Subject<void> = new Subject();

	public separatorKeysCodes: number[] = [ENTER, COMMA];
	public hobbyCtrl = new FormControl('');
	public socialMediaCtrl = new FormControl('');
	public filteredHobbies$?: Observable<string[]>;
	private rentId$?: Observable<string>;
	public myHobbies: string[] = [];
	public socialMedias: string[] = [];
	public actualStudent$?: Observable<IStudent>;
	public actualStudent = {} as IStudent;
	public actualOwner$?: Observable<IOwner>;
	public actualOwner = {} as IOwner;

	private validYear: number = new Date().getFullYear() - 18;

	private announcer = inject(LiveAnnouncer);

	public urlAvatar = '../../../assets/avatar.png';
	public fileScanName = '';
	public filePhotoName = '';
	public urlPhoto = '';
	public urlScan = '';

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

	public dataFormGroup;
	public dataFormGroup2;

	public addOnBlur = true;

	public startDate = new Date().getDate();

	constructor(
		private formBuilder: FormBuilder,
		private router: Router,
		private changeDetectorRef: ChangeDetectorRef,
		private route: ActivatedRoute,
		private profileService: ProfileService
	) {
		this.filteredHobbies$ = this.hobbyCtrl.valueChanges.pipe(
			startWith(null),
			map((hobby: string | null) =>
				hobby ? this.filter(hobby) : this.hobbies.slice()
			)
		);
		this.dataFormGroup = this.formBuilder.group({
			photo: new FormControl('', Validators.required),
			name: new FormControl('', Validators.required),
			surname: new FormControl('', Validators.required),
			yearOfBirth: new FormControl('', [
				Validators.required,
				Validators.pattern('^(19|20)[0-9]{2}$'),
				Validators.max(this.validYear),
			]),
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
			documentScan: new FormControl('', Validators.required),
			validTill: new FormControl(new Date(), Validators.required),
			email: new FormControl({ value: '', disabled: true }),
			password: new FormControl({ value: '', disabled: true }),
		});

		this.dataFormGroup2 = this.formBuilder.group({
			photo: new FormControl('', Validators.required),
			name: new FormControl('', Validators.required),
			surname: new FormControl('', Validators.required),
			address: new FormControl('', Validators.required),
			phoneNumber: new FormControl('', [
				Validators.required,
				Validators.pattern(
					'^[+]?[(]?[0-9]{2,3}[)]?[-s.]?[0-9]{2,3}[-s.]?[0-9]{2,6}$'
				),
			]),
			documentType: new FormControl('', Validators.required),
			documentScan: new FormControl('', Validators.required),
			validTill: new FormControl(new Date(), Validators.required),
			bankAccount: new FormControl('', [
				Validators.required,
				Validators.pattern('^[0-9]{26}$'),
			]),
			email: new FormControl({ value: '', disabled: true }),
			password: new FormControl({ value: '', disabled: true }),
		});
		this.dataFormGroup.get('photo')?.setValue(this.urlAvatar);
		this.dataFormGroup2.get('photo')?.setValue(this.urlAvatar);
	}
	public ngOnInit(): void {
		this.rentId$ = this.route.paramMap.pipe(
			map(params => params.get('id') ?? '')
		);
		if (this.userType === userType.STUDENT) {
			if (this.modificationType === modificationType.EDIT) {
				this.actualStudent$ = this.rentId$.pipe(
					switchMap(value => this.profileService.getStudent(value))
				);
			} else {
				this.actualStudent$ = this.profileService
					.getUser(this.createId.toString())
					.pipe(
						switchMap(user =>
							of({
								...this.actualStudent,
								id: user.id,
								name: user.name,
								surname: user.surname,
								address: user.address,
								phoneNumber: user.phoneNumber,
								email: user.email,
								password: user.password,
							})
						)
					);
			}
			this.actualStudent$.pipe(takeUntil(this.unsubscribe$)).subscribe(student => {
				this.dataFormGroup
					.get('photo')
					?.setValue(student.photo ? student.photo : this.urlAvatar);
				this.urlPhoto = student.photo ? student.photo : this.urlAvatar;
				this.dataFormGroup.get('name')?.setValue(student.name);
				this.dataFormGroup.get('surname')?.setValue(student.surname);
				this.dataFormGroup.get('yearOfBirth')?.setValue(student.yearOfBirth);
				this.dataFormGroup.get('indexNumber')?.setValue(student.indexNumber);
				this.dataFormGroup.get('university')?.setValue(student.university);
				this.dataFormGroup.get('address')?.setValue(student.address);
				this.dataFormGroup.get('phoneNumber')?.setValue(student.phoneNumber);
				this.myHobbies = student.hobbies;
				this.socialMedias = student.socialMedia;
				this.dataFormGroup.get('documentType')?.setValue(student.documentType);
				this.urlScan = student.documentScan;
				this.dataFormGroup.get('validTill')?.setValue(new Date(student.validTill));
				this.dataFormGroup.get('email')?.setValue(student.email);
				this.dataFormGroup.get('password')?.setValue(student.password);
			});
		}

		if (this.userType === userType.OWNER) {
			if (this.modificationType === modificationType.EDIT) {
				this.actualOwner$ = this.rentId$.pipe(
					switchMap(value => this.profileService.getOwner(value))
				);
			} else {
				this.actualOwner$ = this.profileService
					.getUser(this.createId.toString())
					.pipe(
						switchMap(user =>
							of({
								...this.actualOwner,
								id: user.id,
								name: user.name,
								surname: user.surname,
								address: user.address,
								phoneNumber: user.phoneNumber,
								email: user.email,
								password: user.password,
							})
						)
					);
			}
			this.actualOwner$.pipe(takeUntil(this.unsubscribe$)).subscribe(owner => {
				this.dataFormGroup2
					.get('photo')
					?.setValue(owner.photo ? owner.photo : this.urlAvatar);
				this.urlPhoto = owner.photo ? owner.photo : this.urlAvatar;
				this.dataFormGroup2.get('name')?.setValue(owner.name);
				this.dataFormGroup2.get('surname')?.setValue(owner.surname);
				this.dataFormGroup2.get('address')?.setValue(owner.address);
				this.dataFormGroup2.get('phoneNumber')?.setValue(owner.phoneNumber);
				this.dataFormGroup2.get('documentType')?.setValue(owner.documentType);
				this.urlScan = owner.documentScan;
				this.dataFormGroup2.get('validTill')?.setValue(new Date(owner.validTill));
				this.dataFormGroup2.get('bankAccount')?.setValue(owner.bankAccount);
				this.dataFormGroup2.get('email')?.setValue(owner.email);
				this.dataFormGroup2.get('password')?.setValue(owner.password);
			});
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

	public onSubmit() {
		if (this.dataFormGroup.valid) {
			this.router.navigate(['/']);
		}
	}

	public getAge(): string {
		const age =
			new Date().getFullYear() -
			Number(this.dataFormGroup.get('yearOfBirth')?.value);
		if (age > 150) {
			return '';
		}
		return age ? age.toString() : '';
	}

	public onScanSelected(event: Event) {
		const formData = new FormData();
		const fileEvent = (event.target as HTMLInputElement).files;

		if (!fileEvent) {
			return;
		}
		const file: File = fileEvent[0];
		const fileType = file.type;
		if (fileType.match(/image\/*/ || /application\/pdf/) == null) {
			return;
		}
		this.fileScanName = file.name;
		formData.append(this.fileScanName, file);
		const reader = new FileReader();
		reader.readAsDataURL(file);
		reader.onload = () => {
			this.urlScan = <string>reader.result;
			this.changeDetectorRef.detectChanges();
		};
	}
	public onPhotoSelected(event: Event) {
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
		this.filePhotoName = file.name;
		formData.append(this.filePhotoName, file);
		const reader = new FileReader();
		reader.readAsDataURL(file);
		reader.onload = () => {
			this.urlPhoto = <string>reader.result;
			this.changeDetectorRef.detectChanges();
		};
	}
	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
