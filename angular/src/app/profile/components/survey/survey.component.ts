import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IQuestionsData } from '../../models/survey.models';
import { Observable, tap } from 'rxjs';
import { typeName } from '../../models/survey.models';
import { ActivatedRoute } from '@angular/router';
import { SurveyService } from '../../services/survey.service';

@Component({
	selector: 'app-student-survey',
	templateUrl: './survey.component.html',
	styleUrls: ['./survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SurveyComponent {
	public questions$: Observable<IQuestionsData[]>;
	public studentSurveyForm: FormGroup;
	public typeName: typeof typeName = typeName;

	constructor(
		private formBuilder: FormBuilder,
		private snackBar: MatSnackBar,
		private route: ActivatedRoute,
		private service: SurveyService
	) {
		this.studentSurveyForm = this.formBuilder.group({
			lookingForRoommate: [''],
		});

		this.questions$ = this.getQuestions().pipe(
			tap(questions =>
				questions.forEach(question =>
					this.studentSurveyForm.addControl(question.id, new FormControl(''))
				)
			)
		);
	}

	public getQuestions(): Observable<IQuestionsData[]> {
		const param = this.route.snapshot.paramMap.get('survey-type');
		if (param === 'student') {
			return this.service.getStudentQuestions();
		} else {
			return this.service.getOwnerQuestions();
		}
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie wypełniono ankietę!', 'Zamknij', {
			duration: 2000,
		});
	}
}
