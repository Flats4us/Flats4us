import { HttpErrorResponse } from '@angular/common/http';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent } from '@shared/components/base/base.component';
import { AuthService } from '@shared/services/auth.service';
import { map, tap } from 'rxjs';
import { ProfileService } from 'src/app/profile/services/profile.service';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent extends BaseComponent {
	public hide = true;
	public loginForm: FormGroup = new FormGroup({
		email: new FormControl('', [Validators.required, Validators.email]),
		password: new FormControl('', [Validators.required, Validators.minLength(8)]),
	});
	public invalidCredentials$ = this.loginForm.statusChanges.pipe(
		map(() => this.loginForm.hasError('invalidCredentials')),
		this.untilDestroyed()
	);

	constructor(
		private snackBar: MatSnackBar,
		private service: AuthService,
		private router: Router,
		private route: ActivatedRoute,
		private translate: TranslateService,
		private profileService: ProfileService
	) {
		super();
	}

	public onSubmit() {
		this.service
			.login(this.loginForm.value)
			.pipe(this.untilDestroyed())
			.subscribe({
				next: () => {
					this.snackBar.open(
						this.translate.instant('Login.success-message'),
						this.translate.instant('close'),
						{
							duration: 10000,
						}
					);
					this.router.navigateByUrl(
						this.route.snapshot.queryParamMap.get('returnUrl') || '/'
					);
				},
				error: (error: HttpErrorResponse) => {
					if (error.status === 401) {
						this.loginForm.setErrors({ invalidCredentials: true });
					}
				},
				complete: () => {
					this.profileService
						.getActualProfile()
						.pipe(
							tap(profile =>
								this.profileService.setHeaderPhotoURL(profile.profilePicture.path)
							)
						);
				},
			});
	}
}
