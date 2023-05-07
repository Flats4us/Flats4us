import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	ChangeDetectorRef,
} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
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
	public questions: any[] = [];
	public studentSurveyForm: FormGroup;
	public showLookingForRoommates = false;

	constructor(
		private formBuilder: FormBuilder,
		private snackBar: MatSnackBar,
		private http: HttpClient,
		private cdr: ChangeDetectorRef
	) {
		this.studentSurveyForm = this.formBuilder.group({
			party: 0,
			tidy: 0,
			smoke: 0,
			social: 0,
			petOwner: 0,
			veganPerson: 0,
			maxNumberOfRoommates: ['', Validators.required],
			minRoommateAge: ['', Validators.required],
			maxRoommateAge: ['', Validators.required],
			lookingForRoommates: [''],
		});
	}

	public ngOnInit(): void {
		this.http
			.get<IQuestionsData>('../../assets/survey.json')
			.subscribe((data) => {
				this.questions = data.questions;
				this.cdr.detectChanges();
			});

		this.studentSurveyForm
			.get('lookingForRoommates')
			?.valueChanges.subscribe((value) => (this.showLookingForRoommates = value));
	}

	public getQuestionById(id: number) {
		return this.questions.find((question) => question.id === id).content;
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie wypełniono ankietę!', 'Zamknij', {
			duration: 2000,
		});
	}
}
