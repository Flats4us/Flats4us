import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-email-change',
	templateUrl: './emailChange.component.html',
	styleUrls: ['./emailChange.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmailChangeComponent {
	public hide = true;
	public emailChangeForm: FormGroup;

	constructor(
		private fb: FormBuilder,
		private snackBar: MatSnackBar,
	) {
		this.emailChangeForm = this.fb.group({
			email: ['s22900@pjwstk.edu.pl', [Validators.required, Validators.email]],
			password: ['', Validators.required],
		});
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie zmieniono adres mailowy!', 'Zamknij', {
			duration: 2000,
		});
	}
}
