import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseComponent } from '@shared/components/base/base.component';
import { AuthService } from '@shared/services/auth.service';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent extends BaseComponent {
	public hide = true;
	public loginForm: FormGroup = this.fb.group({
		email: ['', Validators.required],
		password: ['', Validators.required],
	});

	constructor(
		private fb: FormBuilder,
		private snackBar: MatSnackBar,
		private service: AuthService
	) {
		super();
	}

	public onSubmit() {
		this.service
			.login(this.loginForm.value)
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open('Zalogowano pomyślnie!', 'Zamknij', {
					duration: 2000,
				})
			);
	}
}
