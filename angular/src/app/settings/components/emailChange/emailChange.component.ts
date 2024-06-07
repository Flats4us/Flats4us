import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent } from '@shared/components/base/base.component';
import { UserService } from '@shared/services/user.service';

@Component({
	selector: 'app-email-change',
	templateUrl: './emailChange.component.html',
	styleUrls: ['./emailChange.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmailChangeComponent extends BaseComponent {
	public hide = true;
	public emailChangeForm: FormGroup;

	constructor(
		private fb: FormBuilder,
		private snackBar: MatSnackBar,
		private service: UserService,
		private translate: TranslateService
	) {
		super();
		this.emailChangeForm = this.fb.group({
			email: ['', [Validators.required, Validators.email]],
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
