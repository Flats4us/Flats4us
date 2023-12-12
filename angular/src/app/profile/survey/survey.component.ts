import {
	ChangeDetectionStrategy,
	Component,
	Input,
	OnDestroy,
	OnInit,
} from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	FormGroupDirective,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';
import { IQuestionsData } from './questions-data.interface';
import { Observable, Subject, map, switchMap, takeUntil } from 'rxjs';
import { typeName } from './typeName';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-student-survey',
	templateUrl: './survey.component.html',
	styleUrls: ['./survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SurveyComponent implements OnInit, OnDestroy {
	@Input()
	public hideButtons = false;

	private readonly unsubscribe$: Subject<void> = new Subject();

	public questions$?: Observable<IQuestionsData[]>;
	public param$?: Observable<string>;
	public studentSurveyForm: FormGroup;
	public surveyForm: FormGroup = new FormGroup({});
	public typeName: typeof typeName = typeName;
	public param: string | null = '';

	constructor(
		private formBuilder: FormBuilder,
		private snackBar: MatSnackBar,
		private http: HttpClient,
		private route: ActivatedRoute,
		private formDir: FormGroupDirective
	) {
		this.studentSurveyForm = this.formBuilder.group({
			lookingForRoommate: [''],
		});
	}
	public ngOnInit(): void {
		if (!this.hideButtons) {
			this.param$ = this.route.paramMap.pipe(
				map(params => params.get('survey-type') ?? '')
			);
		} else {
			this.param$ = this.route.paramMap.pipe(
				map(params => params.get('user') ?? '')
			);
		}
		this.param$
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(param => (this.param = param));
		this.questions$ = this.param$.pipe(
			switchMap(param => this.getQuestions(param))
		);
		this.questions$
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(questions =>
				questions.forEach(question =>
					this.studentSurveyForm.addControl(question.id, new FormControl(''))
				)
			);
		this.surveyForm.addControl('lookingForRoommate', this.studentSurveyForm);
	}

	public getQuestions(user: string): Observable<IQuestionsData[]> {
		if (user === 'student') {
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

	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
