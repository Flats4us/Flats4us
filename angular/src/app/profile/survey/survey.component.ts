import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';
import { IQuestionsData } from './questions-data.interface';
import { Observable } from 'rxjs';
import { typeName } from './typeName';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-student-survey',
	templateUrl: './survey.component.html',
	styleUrls: ['./survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SurveyComponent {
	public questions$: Observable<IQuestionsData[]>;
	public studentSurveyForm: FormGroup;
	public TypeName: typeof typeName = typeName;

	constructor(
		private formBuilder: FormBuilder,
		private snackBar: MatSnackBar,
		private http: HttpClient,
		private route: ActivatedRoute
	) {
		this.studentSurveyForm = this.formBuilder.group({
			lookingForRoommate: [''],
		});

		this.questions$ = this.getQuestions();

		this.questions$.subscribe((questions) =>
			questions.forEach((question) =>
				this.studentSurveyForm.addControl(question.id, new FormControl(''))
			)
		);
	}

	public getQuestions(): Observable<IQuestionsData[]> {
		const param = this.route.snapshot.paramMap.get('survey-type');
		if (param === 'student-survey') {
			return this.http.get<IQuestionsData[]>('../../assets/student-survey.json');
		} else {
			return this.http.get<IQuestionsData[]>('../../assets/owner-survey.json');
		}
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie wypełniono ankietę!', 'Zamknij', {
			duration: 2000,
		});
	}
}
