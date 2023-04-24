import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HttpClient } from '@angular/common/http';
import { IJsonFormData } from '../json-form/json-form.component';

@Component({
	selector: 'app-student-survey',
	templateUrl: './student-survey.component.html',
	styleUrls: ['./student-survey.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StudentSurveyComponent implements OnInit {
	public studentSurveyForm: FormGroup;
	public party!: number;
	public tidy!: number;
	public smoke!: number;
	public social!: number;

	public formData!: IJsonFormData;

	constructor(
		private fb: FormBuilder,
		private snackBar: MatSnackBar,
		private http: HttpClient
	) {
		this.studentSurveyForm = this.fb.group({
			party: 0,
			tidy: 0,
			smoke: 0,
			social: 0,
			petOwner: 0,
			veganPerson: 0,
			lookingForRoommates: new FormControl(false),
			maxNumberOfRoommates: ['', Validators.required],
			minRoommateAge: ['', Validators.required],
			maxRoommateAge: ['', Validators.required],
		});
		this.studentSurveyForm.controls['party'].valueChanges.subscribe((value) => {
			this.party = value;
		});
	}

	public ngOnInit(): void {
		this.http
			.get<IJsonFormData>('../../assets/survey.json')
			.subscribe((formData: IJsonFormData) => {
				this.formData = formData;
			});
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie zmieniono adres mailowy!', 'Zamknij', {
			duration: 2000,
		});
	}
}
