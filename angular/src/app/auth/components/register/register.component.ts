import {
	ChangeDetectionStrategy,
	Component,
	Input,
	OnInit,
} from '@angular/core';
import {
	AbstractControl,
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
	public hideButtons = false;

	public hidePassword = true;
	public hideConfirmPasword = true;

	public registerForm: FormGroup = new FormGroup({});

	constructor(private formDir: FormGroupDirective) {}

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
			new FormControl('', Validators.required)
		);
		this.registerForm?.addControl(
			'password1',
			new FormControl('', [
				Validators.required,
				Validators.pattern(
					'^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$'
				),
			])
		);
		this.registerForm?.addControl(
			'password2',
			new FormControl('', [
				Validators.required,
				this.createPasswordStrengthValidator(),
			])
		);
	}

	public createPasswordStrengthValidator(): ValidatorFn {
		return (control: AbstractControl): ValidationErrors | null => {
			const value = control.value;

			if (!value) {
				return null;
			}

			const passwordValid = value === this.registerForm.get('password1')?.value;

			return !passwordValid ? { passwordValid: true } : null;
		};
	}
}
