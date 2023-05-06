import {
	ChangeDetectionStrategy,
	Component,
	OnInit,
	ChangeDetectorRef,
} from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';

@Component({
	selector: 'app-student-survey',
	templateUrl: './student-survey.component.html',
	styleUrls: ['./student-survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StudentSurveyComponent implements OnInit {
	public question = 'test';
	public studentSurveyForm!: FormGroup;
	public showLookingForRoommates = false;
	public party!: number;
	public tidy!: number;
	public smoke!: number;
	public social!: number;

	//public formData!: IJsonFormData;
	public parsedData: any;

	constructor(
		private formBuilder: FormBuilder,
		private snackBar: MatSnackBar,
		private http: HttpClient,
		private cdr: ChangeDetectorRef
	) {}

	public ngOnInit(): void {
		this.http.get('../../assets/survey.json').subscribe((data) => {
			this.parsedData = JSON.parse(JSON.stringify(data));
			this.question = this.parsedData.questions.find(
				(q: { id: number }) => q.id === 3
			).content;
			this.cdr.detectChanges();
		});

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

		this.studentSurveyForm
			.get('lookingForRoommates')
			?.valueChanges.subscribe((value) => {
				this.showLookingForRoommates = value;
			});
	}
	public onSubmit() {
		this.snackBar.open('Pomyślnie zmieniono adres mailowy!', 'Zamknij', {
			duration: 2000,
		});
	}
}
