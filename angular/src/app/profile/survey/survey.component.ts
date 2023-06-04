import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';
import { IQuestionsData } from './questions-data.interface';
import { Observable } from 'rxjs';

export enum TypeName {
	RADIOBUTTON = 'RADIOBUTTON',
	FORM = 'FORM',
	SWITCH = 'SWITCH',
	SLIDER = 'SLIDER',
	CHECKBOX = 'CHECKBOX',
}

@Component({
	selector: 'app-student-survey',
	templateUrl: './survey.component.html',
	styleUrls: ['./survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SurveyComponent implements OnInit {
	public questions$: Observable<IQuestionsData[]>;
	public questions: IQuestionsData[] = [];
	public studentSurveyForm: FormGroup;
	public TypeName: typeof TypeName = TypeName;

	constructor(
		private formBuilder: FormBuilder,
		private snackBar: MatSnackBar,
		private http: HttpClient
	) {
		this.studentSurveyForm = this.formBuilder.group({
			lookingForRoommate: [''],
		});
	}

	public ngOnInit() {
		this.questions$ = this.getQuestions();

		this.questions$.subscribe((questions) =>
			questions.forEach((question) =>
				this.studentSurveyForm.addControl(String(question.id), new FormControl(''))
			)
		);
	}

	public getQuestions(): Observable<IQuestionsData[]> {
		const urlSegments = window.location.href.split('/');
		if (urlSegments[urlSegments.length - 1] === 'student-survey') {
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
