import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
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
		private service: UserService
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
					this.snackBar.open('Wystąpił błąd', 'Zamknij', {
						duration: 2000,
					}),
				complete: () =>
					this.snackBar.open('Pomyślnie zmieniono adres mailowy!', 'Zamknij', {
						duration: 2000,
					}),
			});
	}
}
