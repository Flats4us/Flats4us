import { ChangeDetectionStrategy, Component, ViewChild } from '@angular/core';
import { modificationType, userType } from '../models/types';
import { MatStepper } from '@angular/material/stepper';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, map } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-profile-create',
	templateUrl: './create-profile.component.html',
	styleUrls: ['./create-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateProfileComponent {
	@ViewChild('stepper')
	public stepper: MatStepper | undefined;

	public uType = userType;
	public mType = modificationType;
	public photoUrl = '';
	public scanUrl = '';

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
		private snackBar: MatSnackBar
	) {}

	public setPhotoUrl(url: string) {
		this.photoUrl = url;
	}

	public setScanUrl(url: string) {
		this.scanUrl = url;
	}

	public onSubmit() {
		if (
			this.registerForm.valid &&
			this.createAccountForm.valid &&
			this.photoUrl &&
			this.scanUrl
		) {
			this.snackBar.open('Pomyślnie utworzono konto!', 'Zamknij', {
				duration: 2000,
			});
		}
	}
}
