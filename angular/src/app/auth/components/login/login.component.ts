import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
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
		private service: AuthService,
		private router: Router,
		private route: ActivatedRoute
	) {
		super();
	}

	public onSubmit() {
		this.service
			.login(this.loginForm.value)
			.pipe(this.untilDestroyed())
			.subscribe(
				() => (
					this.snackBar.open('Zalogowano pomyślnie!', 'Zamknij', {
						duration: 2000,
					}),
					this.router.navigateByUrl(
						this.route.snapshot.queryParamMap.get('returnUrl') || '/'
					)
				)
			);
	}
}
