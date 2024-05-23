import { AbstractControl } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

export function setLocalDate(
	event: MatDatepickerInputEvent<Date>,
	control?: AbstractControl | null
): void {
	if (!control) {
		return;
	}
	const selectedDate = event.value;
	if (selectedDate) {
		const date = new Date(selectedDate);
		const changedDate = new Date(
			date.getFullYear(),
			date.getMonth(),
			date.getDate(),
			date.getHours(),
			date.getMinutes() - date.getTimezoneOffset()
		);
		control?.setValue(changedDate);
	}
}
