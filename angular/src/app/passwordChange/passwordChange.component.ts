import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
	selector: 'app-email-change',
	templateUrl: './passwordChange.component.html',
	styleUrls: ['./passwordChange.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PasswordChangeComponent {
	public hideCurrentPassword = true;
	public hideFirstNewPassword = true;
	public hideSecondNewPassword = true;

	public passwordChangeForm: FormGroup;

	constructor(private fb: FormBuilder) {
		this.passwordChangeForm = this.fb.group({
			currentPassword: ['', Validators.required],
			firstNewPassword: ['', Validators.required],
			secondNewPassword: ['', Validators.required],
		});
	}

	public onSubmit() {
		// eslint-disable-next-line no-console
		console.log(this.passwordChangeForm.value);
	}
}
