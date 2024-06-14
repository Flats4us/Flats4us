import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
	AbstractControl,
	FormBuilder,
	FormGroup,
	ValidationErrors,
	ValidatorFn,
	Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent } from '@shared/components/base/base.component';
import { UserService } from '@shared/services/user.service';

@Component({
	selector: 'app-email-change',
	templateUrl: './email-change.component.html',
	styleUrls: ['./email-change.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmailChangeComponent extends BaseComponent {
	public hide = true;
	public emailChangeForm: FormGroup;

	private confirmEmailValidator(emailFieldName: string): ValidatorFn {
		return (confirmEmail: AbstractControl): ValidationErrors | null => {
			const email = confirmEmail.root.get(emailFieldName);
			return email && confirmEmail && email.value === confirmEmail.value
				? null
				: { emailNoMatch: true };
		};
	}

	constructor(
		private fb: FormBuilder,
		private snackBar: MatSnackBar,
		private service: UserService,
		private translate: TranslateService
	) {
		super();
		this.emailChangeForm = this.fb.group({
			email: ['', [Validators.required, Validators.email]],
			confirmEmail: [
				'',
				[Validators.required, this.confirmEmailValidator('email')],
			],
		});
	}

	public onSubmit() {
		return this.service
			.changeEmail(this.emailChangeForm.value.email)
			.pipe(this.untilDestroyed())
			.subscribe({
				error: () =>
					this.snackBar.open(
						this.translate.instant('Email-change.error'),
						this.translate.instant('close'),
						{
							duration: 10000,
						}
					),
				complete: () =>
					this.snackBar.open(
						this.translate.instant('Email-change.email-info1'),
						this.translate.instant('close'),
						{
							duration: 10000,
						}
					),
			});
	}
}
