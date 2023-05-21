import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	ChangeDetectorRef,
} from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';
import { IQuestionsData } from './questions-data.interface';

@Component({
	selector: 'app-student-survey',
	templateUrl: './student-survey.component.html',
	styleUrls: ['./student-survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StudentSurveyComponent implements OnInit {
	public questions: IQuestionsData[] = [];
	public studentSurveyForm: FormGroup;

	constructor(
		private formBuilder: FormBuilder,
		private snackBar: MatSnackBar,
		private http: HttpClient,
		private cdr: ChangeDetectorRef
	) {}

	public ngOnInit() {
		this.http.get<[]>('../../assets/JSON.json').subscribe((data) => {
			this.questions = data;
			this.cdr.detectChanges();

			this.studentSurveyForm = this.formBuilder.group({
				lookingForRoommate: [''],
			});
			this.questions.forEach((question) =>
				this.studentSurveyForm.addControl(
					question.formControlName,
					new FormControl('')
				)
			);
		});
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie wypełniono ankietę!', 'Zamknij', {
			duration: 2000,
		});
	}
}
