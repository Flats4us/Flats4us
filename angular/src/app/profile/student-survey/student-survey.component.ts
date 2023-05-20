import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	ChangeDetectorRef,
} from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';

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
	) {}

	public ngOnInit() {
		this.http.get<[]>('../../assets/JSON.json').subscribe((data) => {
			this.questions = data;
			this.cdr.detectChanges();
		});

		//this.studentSurveyForm = this.generateSurveyForm();

		this.studentSurveyForm
			.get('lookingForRoommates')
			?.valueChanges.subscribe((value) => (this.showLookingForRoommates = value));
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie wypełniono ankietę!', 'Zamknij', {
			duration: 2000,
		});
	}

	private generateSurveyForm(): FormGroup {
		const baseForm = this.formBuilder.group({});
		while (this.questions.length == 0) {
			/* empty */
		}
		this.questions.forEach((question) => {
			baseForm.addControl(question.formControlName, new FormControl(''));
		});
		return baseForm;
	}
}
