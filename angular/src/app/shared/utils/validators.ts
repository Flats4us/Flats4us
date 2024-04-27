import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { UserService } from '@shared/services/user.service';

export function validityAgeValidator(): ValidatorFn {
	return (control: AbstractControl): ValidationErrors | null => {
		const value = control.value;

		if (!value) {
			return null;
		}

		return !checkValidityAge(value) ? { validityAge: true } : null;
	};
}

export function matchPasswordValidator(
	control: AbstractControl
): ValidationErrors | null {
	const password = control.get('password');
	const confirmPassword = control.get('confirmPassword');
	if (!(password || confirmPassword)) {
		return null;
	} else if (
		password &&
		confirmPassword &&
		password.value !== confirmPassword.value
	) {
		return { matchPassword: true };
	} else {
		return null;
	}
}

export function validityEmailValidator(
	userService: UserService,
	checkIfEmailExist: (email: string, service: UserService) => boolean
): ValidatorFn {
	return (control: AbstractControl): ValidationErrors | null => {
		const value = control.value;

		if (!value) {
			return null;
		}

		return checkIfEmailExist(value, userService) ? { validityEmail: true } : null;
	};
}

function checkValidityAge(date: Date): boolean {
	const endDate = new Date(date);
	const actualDate = new Date();
	const years = actualDate.getFullYear() - endDate.getFullYear();
	const isValidAge = years >= 18 && years <= 150 ? true : false;
	return isValidAge;
}
