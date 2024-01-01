import { ErrorStateMatcher } from '@angular/material/core';
import { FormControl } from '@angular/forms';

export class PasswordChangeErrorStateMatcher implements ErrorStateMatcher {
	public isErrorState(control: FormControl | null): boolean {
		return !!(control?.parent?.invalid && control?.touched);
	}
}
