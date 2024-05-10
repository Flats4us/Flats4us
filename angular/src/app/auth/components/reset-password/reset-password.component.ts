import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
	FormControl,
	ReactiveFormsModule,
	UntypedFormGroup,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '@shared/components/base/base.component';
import { AuthService } from '@shared/services/auth.service';

@Component({
	selector: 'app-reset-password',
	standalone: true,
	imports: [
		CommonModule,
		ReactiveFormsModule,
		MatCardModule,
		MatFormFieldModule,
		MatInputModule,
		MatButtonModule,
	],
	templateUrl: './reset-password.component.html',
	styleUrls: ['./reset-password.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ResetPasswordComponent extends BaseComponent {
	private resetPasswordToken: string;

	public form = new UntypedFormGroup({
		password: new FormControl<string>('', [Validators.required]),
		confirmPassword: new FormControl<string>('', [Validators.required]),
	});

	constructor(
		private route: ActivatedRoute,
		private authService: AuthService,
		private snackBar: MatSnackBar
	) {
		super();

		this.resetPasswordToken = this.route.snapshot.queryParams['token'];
	}

	public resetPassword() {
		if (this.form.invalid || !this.form.value.password) {
			return;
		}

		this.authService
			.resetPassword(this.resetPasswordToken, this.form.value.password)
			.pipe(this.untilDestroyed())
			.subscribe({
				error: () =>
					this.snackBar.open('Wystąpił błąd', 'Zamknij', {
						duration: 2000,
					}),
				complete: () =>
					this.snackBar.open('Pomyślnie zresetowano hasło!', 'Zamknij', {
						duration: 2000,
					}),
			});
	}
}
