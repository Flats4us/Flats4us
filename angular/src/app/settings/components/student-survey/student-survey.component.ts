import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-student-survey',
	templateUrl: './student-survey.component.html',
	styleUrls: ['./student-survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StudentSurveyComponent {
	public studentSurveyForm: FormGroup;
	public partyy = 1;
	public tidy!: number;
	public smoke!: number;
	public social!: number;

	constructor(private fb: FormBuilder, private snackBar: MatSnackBar) {
		this.studentSurveyForm = this.fb.group({
			party: 0,
		});
		this.studentSurveyForm.controls['party'].valueChanges.subscribe((value) => {
			this.partyy = value;
		});
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie zmieniono adres mailowy!', 'Zamknij', {
			duration: 2000,
		});
	}
}
