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
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent } from '@shared/components/base/base.component';
import { AuthService } from '@shared/services/auth.service';

@Component({
	selector: 'app-password-change',
	templateUrl: './password-change.component.html',
	styleUrls: ['./password-change.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PasswordChangeComponent extends BaseComponent {
	public hideOldPassword = true;
	public hideNewPassword = true;
	public hideConfirmPassword = true;

	private confirmPasswordValidator(passwordFieldName: string): ValidatorFn {
		return (confirmPassword: AbstractControl): ValidationErrors | null => {
			const password = confirmPassword.root.get(passwordFieldName);
			return password &&
				confirmPassword &&
				password.value === confirmPassword.value
				? null
				: { passwordNoMatch: true };
		};
	}

	public passwordChangeForm: FormGroup = this.fb.group({
		oldPassword: new FormControl('', [Validators.required]),
		newPassword: new FormControl('', [
			Validators.required,
			Validators.minLength(8),
			Validators.maxLength(50),
			Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$/),
		]),
		confirmPassword: new FormControl('', [
			Validators.required,
			this.confirmPasswordValidator('newPassword'),
		]),
	});

	constructor(
		private fb: FormBuilder,
		private snackBar: MatSnackBar,
		private service: AuthService,
		private translate: TranslateService
	) {
		super();
	}

	public onSubmit() {
		return this.service
			.changePassword(this.passwordChangeForm.value)
			.pipe(this.untilDestroyed())
			.subscribe({
				error: () =>
					this.snackBar.open(
						this.translate.instant('Password-change.info1'),
						this.translate.instant('Password-change.close'),
						{
							duration: 2000,
						}
					),
				complete: () =>
					this.snackBar.open(
						this.translate.instant('Password-change.info2'),
						this.translate.instant('Password-change.close'),
						{
							duration: 2000,
						}
					),
			});
	}
}
