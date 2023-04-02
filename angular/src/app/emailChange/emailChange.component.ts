import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
	selector: 'app-email-change',
	templateUrl: './emailChange.component.html',
	styleUrls: ['./emailChange.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EmailChangeComponent {
	firstFormGroup = this._formBuilder.group({
		firstCtrl: ['', Validators.required],
	});
	secondFormGroup = this._formBuilder.group({
		secondCtrl: ['', Validators.required],
	});
	hidePassword = true;

	constructor(private _formBuilder: FormBuilder) {}
}
