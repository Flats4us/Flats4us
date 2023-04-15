import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';

@Component({
	selector: 'app-email-change',
	templateUrl: './emailChange.component.html',
	styleUrls: ['./emailChange.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmailChangeComponent {
	hide = true;
	favoriteColorControl = new FormControl('');
	emailChangeForm: FormGroup;

	constructor(private fb: FormBuilder) {
		this.emailChangeForm = this.fb.group({
			email: ['s22900@pjwstk.edu.pl', Validators.required],
			password: ['', Validators.required],
		});
	}

	onSubmit() {
		console.log(this.emailChangeForm.value);
	}
}
