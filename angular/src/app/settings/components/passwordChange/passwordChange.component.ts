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
import { PasswordChangeErrorStateMatcher } from './passwordChangeErrorStateMatcher';
import { AuthService } from '@shared/services/auth.service';

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

	private confirmPasswordValidator: ValidatorFn = (
		control: AbstractControl
	): ValidationErrors | null => {
		return control.value.newPassword === control.value.confirmPassword
			? null
			: { passwordNoMatch: true };
	};

	public passwordChangeForm: FormGroup = this.fb.group(
		{
			oldPassword: new FormControl('', [Validators.required]),
			newPassword: new FormControl('', [
				Validators.required,
				Validators.minLength(8),
				Validators.maxLength(50),
				Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$/),
			]),
			confirmPassword: new FormControl('', [Validators.required]),
		},
		{ validators: [this.confirmPasswordValidator] }
	);

	constructor(
		private fb: FormBuilder,
		private snackBar: MatSnackBar,
		private service: AuthService
	) {
		super();
	}

	public onSubmit() {
		return this.service
			.changePassword(this.passwordChangeForm)
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
		if (this.passwordChangeForm.controls['newPassword'].hasError('required')) {
			return 'Pole wymagane';
		} else if (
			this.passwordChangeForm.controls['newPassword'].hasError('minLength') ||
			this.passwordChangeForm.controls['newPassword'].hasError('maxLength')
		) {
			return 'Hasło musi mieć od 8 do 50 znaków';
		} else if (
			this.passwordChangeForm.controls['newPassword'].hasError('pattern')
		) {
			return 'Hasło musi mieć jedną wielką i małą literę oraz cyfrę';
		}
		return '';
	}
}
