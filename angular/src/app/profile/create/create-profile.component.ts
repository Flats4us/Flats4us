import { ChangeDetectionStrategy, Component, ViewChild } from '@angular/core';
import { modificationType, userType } from '../models/types';
import { MatStepper } from '@angular/material/stepper';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Observable, map } from 'rxjs';

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

	public user$: Observable<string>;

	public registerForm = this.formBuilder.group({
		name: new FormControl('', Validators.required),
		surname: new FormControl('', Validators.required),
		email: new FormControl('', Validators.required),
	});
	public createAccountForm = new FormGroup({});

	constructor(private formBuilder: FormBuilder, private route: ActivatedRoute) {
		this.user$ = this.route.paramMap.pipe(
			map(params => params.get('user') ?? '')
		);
	}
}
