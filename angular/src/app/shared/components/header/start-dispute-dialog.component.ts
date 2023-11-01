import { Component } from '@angular/core';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {
	FormBuilder,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

export interface IDialogData {
	message: string;
}

@Component({
	selector: 'app-start-dispute-dialog',
	templateUrl: 'start-dispute-dialog.html',
	standalone: true,
	imports: [
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		ReactiveFormsModule,
	],
})
export class StartDisputeDialogComponent {
	public form: FormGroup;

	constructor(private fb: FormBuilder) {
		this.form = this.fb.group({
			message: ['', Validators.required],
		});
	}
}
