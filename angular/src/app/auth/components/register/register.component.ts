import {
	ChangeDetectionStrategy,
	Component,
	Input,
	OnInit,
} from '@angular/core';
import {
	AbstractControl,
	FormBuilder,
	FormControl,
	FormGroup,
	FormGroupDirective,
	ValidationErrors,
	ValidatorFn,
	Validators,
} from '@angular/forms';

@Component({
	selector: 'app-register',
	templateUrl: './register.component.html',
	styleUrls: ['./register.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RegisterComponent implements OnInit {
	@Input()
	public createProfileMode = false;

	public hidePassword = true;
	public hideConfirmPasword = true;

	public registerForm: FormGroup = this.formDir.form;

	constructor(
		private formDir: FormGroupDirective,
		private formBuilder: FormBuilder
	) {}

	public ngOnInit() {
		this.registerForm = this.formDir.form;
		this.buildForm();
	}

	public buildForm() {
		this.registerForm?.addControl(
			'name',
			new FormControl('', Validators.required)
		);
		this.registerForm?.addControl(
			'surname',
			new FormControl('', Validators.required)
		);
		this.registerForm?.addControl(
			'email',
			new FormControl('', [Validators.required, Validators.email])
		);
		this.registerForm?.addControl(
			'password',
			new FormControl('', [
				Validators.required,
				Validators.pattern(
					'^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$'
				),
			])
		);
		this.registerForm.addControl(
			'confirmPassword',
			new FormControl('', [Validators.required, this.matchPasswordValidator()])
		);
	}

	public matchPasswordValidator(): ValidatorFn {
		return (control: AbstractControl): ValidationErrors | null => {
			const value = control.value;

			if (!value) {
				return null;
			}

			const passwordValid = value === this.registerForm.get('password')?.value;

			return !passwordValid ? { passwordValid: true } : null;
		};
	}
}
