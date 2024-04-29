import {
	ChangeDetectionStrategy,
	Component,
	Input,
	OnChanges,
	OnInit,
	SimpleChanges,
} from '@angular/core';
import { FormGroup, FormGroupDirective } from '@angular/forms';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent implements OnInit, OnChanges {
	@Input()
	public createProfileMode = false;
	@Input()
	public emailExist = false;

	public hidePassword = true;
	public hideConfirmPasword = true;

	public registerForm: FormGroup = this.formDir.form;

	constructor(private formDir: FormGroupDirective) {}

	public ngOnInit() {
		this.registerForm = this.formDir.form;
	}

	public ngOnChanges(changes: SimpleChanges): void {
		if (changes['emailExist'].currentValue) {
			if (this.emailExist) {
				this.registerForm.controls['email'].setErrors({ emailExist: true });
			}
		}
	}
}
