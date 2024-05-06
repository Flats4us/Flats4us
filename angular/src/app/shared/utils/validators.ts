import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';
import { UserService } from '@shared/services/user.service';
import { map, takeUntil } from 'rxjs';

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
	control: AbstractControl
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

function checkIfEmailExist(email: string, userService: UserService): boolean {
	let exist = false;
	userService
		.checkIfEmailExist(email)
		.subscribe(result => (exist = result.result));
	return exist;
}
