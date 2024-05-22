import { FormGroup } from '@angular/forms';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';

export function setLocalDate(
	event: MatDatepickerInputEvent<Date>,
	form: FormGroup,
	control: string
): void {
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
		form.get(control)?.setValue(changedDate, { onlyself: true });
	}
}
