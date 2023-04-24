import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent {
	public hide = true;
	public loginForm: FormGroup;

	constructor(private fb: FormBuilder, private snackBar: MatSnackBar) {
		this.loginForm = this.fb.group({
			email: ['', Validators.required],
			password: ['', Validators.required],
		});
	}

	public onSubmit() {
		this.snackBar.open('Zalogowano pomyślnie!', 'Zamknij', {
			duration: 2000,
		});
	}
}
