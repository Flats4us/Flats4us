import {
	ChangeDetectionStrategy,
	Component,
	Input,
	OnInit,
	Optional,
} from '@angular/core';
import {
	AbstractControl,
	FormControl,
	FormGroup,
	FormGroupDirective,
	FormGroupName,
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

	public registerForm!: FormGroup;

	constructor(
		private formDir: FormGroupDirective,
		@Optional() private formGroupName: FormGroupName
	) {}

	public ngOnInit() {
		this.registerForm = this.formGroupName
			? (this.formDir.form.controls['registerForm'] as FormGroup)
			: this.formDir.form;
		this.buildForm();
	}

	public buildForm() {
		const ctrl1 = new FormControl('', Validators.required);
		this.registerForm?.addControl('name', ctrl1);
		const ctrl2 = new FormControl('', Validators.required);
		this.registerForm?.addControl('surname', ctrl2);
		const ctrl3 = new FormControl('', Validators.required);
		this.registerForm?.addControl('email', ctrl3);
		const ctrl4 = new FormControl('', [
			Validators.required,
			Validators.pattern(
				'^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$'
			),
		]);
		this.registerForm?.addControl('password1', ctrl4);
		const ctrl5 = new FormControl('', [
			Validators.required,
			this.createPasswordStrengthValidator(),
		]);
		this.registerForm?.addControl('password2', ctrl5);
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
