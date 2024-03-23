import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import {
	FormControl,
	FormGroup,
	ReactiveFormsModule,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
	MAT_DIALOG_DATA,
	MatDialogModule,
	MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatSliderModule } from '@angular/material/slider';
import { IEnumerableItem } from '@shared/models/shared.models';
import { Observable } from 'rxjs';
import { IOffer } from 'src/app/offer/models/offer.models';

@Component({
	selector: 'app-rent-rate',
	standalone: true,
	imports: [
		CommonModule,
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		MatSliderModule,
		MatSelectModule,
		ReactiveFormsModule,
		MatButtonModule,
	],
	templateUrl: './rent-rate.component.html',
	styleUrls: ['./rent-rate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentRateComponent {
	public rates: IEnumerableItem[] = [
		{
			id: 1,
			name: 'Bardzo zły',
		},
		{
			id: 2,
			name: 'Zły',
		},
		{
			id: 3,
			name: 'Średni',
		},
		{
			id: 4,
			name: 'Dobry',
		},
		{
			id: 5,
			name: 'Bardzo dobry',
		},
	];

	public form = new FormGroup({
		rate: new FormControl(null, Validators.required),
		cleanliness: new FormControl(null, Validators.required),
		service: new FormControl(null, Validators.required),
		location: new FormControl(null, Validators.required),
		equipment: new FormControl(null, Validators.required),
		price: new FormControl(null, Validators.required),
		opinion: new FormControl(null, Validators.required),
	});

	constructor(
		private dialogRef: MatDialogRef<RentRateComponent>,
		@Inject(MAT_DIALOG_DATA) public data$: Observable<IOffer>,
	) {}

	public onSubmit(): void {
		this.dialogRef.close(this.form.value);
	}
}
