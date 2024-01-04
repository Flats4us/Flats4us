import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IQuestionsData, typeName } from '../../models/survey.models';
import { Observable, switchMap, tap } from 'rxjs';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { SurveyService } from '../../services/survey.service';

@Component({
	selector: 'app-student-survey',
	templateUrl: './survey.component.html',
	styleUrls: ['./survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SurveyComponent {
	@Input() public offerForm: FormGroup;
	public questions$: Observable<IQuestionsData[]>;
	public typeName: typeof typeName = typeName;

	constructor(
		private formBuilder: FormBuilder,
		private snackBar: MatSnackBar,
		private route: ActivatedRoute,
		private service: SurveyService
	) {
		this.offerForm = this.formBuilder.group({
			lookingForRoommate: [''],
		});

		this.questions$ = this.getQuestions().pipe(
			tap(questions =>
				questions.forEach(question =>
					this.offerForm.addControl(question.name, new FormControl(null))
				)
			)
		);
	}

	public getQuestions(): Observable<IQuestionsData[]> {
		return this.route.paramMap.pipe(
			switchMap((params: ParamMap) => {
				const surveyType = params.get('survey-type');
				if (surveyType === 'student') {
					return this.service.getStudentQuestions();
				} else {
					return this.service.getOwnerQuestions();
				}
			})
		);
	}
}
