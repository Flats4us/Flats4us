import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-password-change',
	templateUrl: './passwordChange.component.html',
	styleUrls: ['./passwordChange.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PasswordChangeComponent {
	public hideCurrentPassword = true;
	public hideFirstNewPassword = true;
	public hideSecondNewPassword = true;

	public passwordChangeForm: FormGroup;

	constructor(private fb: FormBuilder, private snackBar: MatSnackBar) {
		this.passwordChangeForm = this.fb.group({
			currentPassword: ['', Validators.required],
			firstNewPassword: ['', Validators.required],
			secondNewPassword: ['', Validators.required],
		});
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie zmieniono hasło!', 'Zamknij', {
			duration: 2000,
		});
	}
}
