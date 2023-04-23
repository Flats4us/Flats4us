import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	Input,
} from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';
import { IJsonFormData } from '../json-form/json-form.component';

@Component({
	selector: 'app-student-survey',
	templateUrl: './student-survey.component.html',
	styleUrls: ['./student-survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StudentSurveyComponent implements OnInit {
	public studentSurveyForm: FormGroup;
	public partyy = 1;
	public tidy!: number;
	public smoke!: number;
	public social!: number;

	public formData!: IJsonFormData;

	constructor(
		private fb: FormBuilder,
		private snackBar: MatSnackBar,
		private http: HttpClient
	) {
		this.studentSurveyForm = this.fb.group({
			party: 0,
		});
		this.studentSurveyForm.controls['party'].valueChanges.subscribe((value) => {
			this.partyy = value;
		});
	}

	public ngOnInit(): void {
		this.http
			.get<IJsonFormData>('../../assets/survey.json')
			.subscribe((formData: IJsonFormData) => {
				console.log(formData);
				this.formData = formData;
			});
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie zmieniono adres mailowy!', 'Zamknij', {
			duration: 2000,
		});
	}
}
