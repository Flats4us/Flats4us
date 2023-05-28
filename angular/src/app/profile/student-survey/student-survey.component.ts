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
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Component({
	selector: 'app-student-survey',
	templateUrl: './student-survey.component.html',
	styleUrls: ['./student-survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StudentSurveyComponent implements OnInit {
	public questions$: Observable<IQuestionsData[]>;
	public questions: IQuestionsData[] = [];
	public studentSurveyForm: FormGroup;

	constructor(
		private formBuilder: FormBuilder,
		private snackBar: MatSnackBar,
		private http: HttpClient,
		private cdr: ChangeDetectorRef
	) {}

	public ngOnInit() {
		this.questions$ = this.http.get<[]>('../../assets/JSON.json').pipe(
			tap((data) => {
				this.questions = data;
				this.cdr.detectChanges();
			})
		);

		this.studentSurveyForm = this.formBuilder.group({
			lookingForRoommate: [''],
		});

		this.questions$.subscribe((questions) => {
			questions.forEach((question) =>
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
