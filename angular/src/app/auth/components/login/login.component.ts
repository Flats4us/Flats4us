import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
	selector: 'app-login',
	templateUrl: './login.component.html',
	styleUrls: ['./login.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent {
	hide = true;
	loginForm: FormGroup;

	constructor(private fb: FormBuilder) {
		this.loginForm = this.fb.group({
			email: ['', Validators.required],
			password: ['', Validators.required],
		});
	}

	onSubmit() {
		console.log(this.loginForm.value);
	}
}
