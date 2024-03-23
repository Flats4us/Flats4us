import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function validityAgeValidator(): ValidatorFn {
	return (control: AbstractControl): ValidationErrors | null => {
		const value = control.value;

		if (!value) {
			return null;
		}

		return !checkValidityAge(value) ? { validityAge: true } : null;
	};
}

export const matchPasswordValidator: ValidatorFn = (
	control: AbstractControl,
): ValidationErrors | null => {
	const password = control.get('password');
	const confirmPassword = control.get('confirmPassword');
	return password && confirmPassword && password.value !== confirmPassword.value
		? { matchPassword: true }
		: null;
};

function checkValidityAge(date: Date): boolean {
	const endDate = new Date(date);
	const actualDate = new Date();
	const years = actualDate.getFullYear() - endDate.getFullYear();
	const isValidAge = years >= 18 && years <= 150 ? true : false;
	return isValidAge;
}
