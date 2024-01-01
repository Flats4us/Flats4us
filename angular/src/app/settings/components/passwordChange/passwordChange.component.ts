import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
	AbstractControl,
	FormBuilder,
	FormControl,
	FormGroup,
	ValidationErrors,
	ValidatorFn,
	Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseComponent } from '@shared/components/base/base.component';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment.prod';
import { PasswordChangeErrorStateMatcher } from './passwordChangeErrorStateMatcher';

@Component({
	selector: 'app-password-change',
	templateUrl: './passwordChange.component.html',
	styleUrls: ['./passwordChange.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PasswordChangeComponent extends BaseComponent {
	public hideOldPassword = true;
	public hideNewPassword = true;
	public hideConfirmPassword = true;

	public matcher = new PasswordChangeErrorStateMatcher();

	public oldPasswordControl = new FormControl('', [Validators.required]);
	public newPasswordControl = new FormControl('', [
		Validators.required,
		Validators.minLength(8),
		Validators.maxLength(50),
		Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$/),
	]);
	public confirmPasswordControl = new FormControl('', [Validators.required]);

	private confirmPasswordValidator: ValidatorFn = (
		control: AbstractControl
	): ValidationErrors | null => {
		return control.value.newPassword === control.value.confirmPassword
			? null
			: { passwordNoMatch: true };
	};

	public passwordChangeForm: FormGroup = this.fb.group(
		{
			oldPassword: this.oldPasswordControl,
			newPassword: this.newPasswordControl,
			confirmPassword: this.confirmPasswordControl,
		},
		{ validators: [this.confirmPasswordValidator] }
	);

	private apiRoute = `${environment.apiUrl}/auth`;

	constructor(
		private fb: FormBuilder,
		private snackBar: MatSnackBar,
		private http: HttpClient
	) {
		super();
	}

	public onSubmit() {
		const headers = {
			'Content-Type': 'application/json',
		};

		return this.http
			.put(`${this.apiRoute}/change-password`, this.passwordChangeForm.value, {
				headers: headers,
				responseType: 'text',
			})
			.pipe(this.untilDestroyed())
			.subscribe({
				error: () =>
					this.snackBar.open('Wystąpił błąd', 'Zamknij', {
						duration: 2000,
					}),
				complete: () =>
					this.snackBar.open('Pomyślnie zmieniono hasło!', 'Zamknij', {
						duration: 2000,
					}),
			});
	}

	protected getErrorMessage(): string {
		if (this.newPasswordControl.hasError('required')) {
			return 'Pole wymagane';
		} else if (
			this.newPasswordControl.hasError('minLength') ||
			this.newPasswordControl.hasError('maxLength')
		) {
			return 'Hasło musi mieć od 8 do 50 znaków';
		} else if (this.newPasswordControl.hasError('pattern')) {
			return 'Hasło musi mieć jedną wielką i małą literę oraz cyfrę';
		}
		return '';
	}
}
