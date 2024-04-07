import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
	FormBuilder,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
	selector: 'app-add-opinion',
	templateUrl: './add-opinion.component.html',
	styleUrls: ['./add-opinion.component.scss'],
	standalone: true,
	imports: [
		CommonModule,
		MatCardModule,
		MatIconModule,
		FormsModule,
		MatButtonModule,
		MatCheckboxModule,
		MatInputModule,
		MatListModule,
		ReactiveFormsModule,
		MatSnackBarModule,
	],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddOpinionComponent {
	public starsScale: number[] = [1, 2, 3, 4, 5];
	public rating = 4.98;
	public opinionForm: FormGroup;

	constructor(private fb: FormBuilder, private snackBar: MatSnackBar) {
		this.opinionForm = this.fb.group({
			helpful: false,
			cooperative: false,
			organized: false,
			friendly: false,
			respectfulOfPrivacy: false,
			communicative: false,
			dishonest: false,
			badPersonalHygiene: false,
			disorganized: false,
			conflictual: false,
			tooNoisy: false,
			doesNotFollowArrangements: false,
			selectedGrade: [0, Validators.min(1)],
			comment: [''],
		});
	}

	public countStar(star: number) {
		this.opinionForm.controls['selectedGrade'].setValue(star);
	}

	public onSubmit() {
		this.snackBar.open('Pomyślnie dodano opinię!', 'Zamknij', {
			duration: 10000,
		});
	}
}
