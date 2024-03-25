import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';
import { IQuestionsData, typeName } from '../../models/survey.models';
import { Observable, switchMap, tap } from 'rxjs';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { SurveyService } from '../../services/survey.service';

@Component({
	selector: 'app-survey',
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
		private route: ActivatedRoute,
		private service: SurveyService
	) {
		this.offerForm = this.formBuilder.group({
			lookingForRoommate: [''],
		});

		this.questions$ = this.getQuestions().pipe(
			tap(questions => this.getFormControls(questions))
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

	public getFormControls(questions: IQuestionsData[]) {
		questions.forEach(question =>
			this.offerForm.addControl(
				question.name,
				new FormControl(null, Validators.required)
			)
		);
	}
}
