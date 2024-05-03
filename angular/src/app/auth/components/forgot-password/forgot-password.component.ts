import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {
	FormControl,
	ReactiveFormsModule,
	UntypedFormGroup,
	Validators,
} from '@angular/forms';
import { AuthService } from '@shared/services/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-forgot-password',
	standalone: true,
	imports: [
		CommonModule,
		MatButtonModule,
		MatCardModule,
		MatFormFieldModule,
		MatInputModule,
		ReactiveFormsModule,
	],
	templateUrl: './forgot-password.component.html',
	styleUrls: ['./forgot-password.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ForgotPasswordComponent extends BaseComponent {
	public form = new UntypedFormGroup({
		email: new FormControl<string>('', [Validators.required, Validators.email]),
	});

	constructor(private authService: AuthService, private snackBar: MatSnackBar) {
		super();
	}

	public sendPasswordResetLink() {
		if (this.form.invalid || !this.form.value.email) {
			return;
		}

		this.authService
			.sendPasswordResetLink(this.form.value.email)
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open(
					'Jeżeli podany adres e-mail jest u nas zarejestrowany, otrzymasz na niego wiadomość.',
					'Zamknij',
					{
						duration: 10000,
					}
				)
			);
	}
}
