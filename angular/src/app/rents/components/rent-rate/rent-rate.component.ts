import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import {
	FormControl,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	UntypedFormGroup,
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
import { Observable, switchMap, takeUntil } from 'rxjs';
import { IOffer } from 'src/app/offer/models/offer.models';
import { RentsService } from '../../services/rents.service';
import { IRentOpinion } from '../../models/rents.models';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';

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
		FormsModule,
		ReactiveFormsModule,
		MatButtonModule,
	],
	templateUrl: './rent-rate.component.html',
	styleUrls: ['./rent-rate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentRateComponent extends BaseComponent {
	public form = new UntypedFormGroup({
		rating: new FormControl(1, Validators.required),
		cleanliness: new FormControl(1, Validators.required),
		service: new FormControl(1, Validators.required),
		location: new FormControl(1, Validators.required),
		equipment: new FormControl(1, Validators.required),
		qualityForMoney: new FormControl(1, Validators.required),
		description: new FormControl(null),
	});

	constructor(
		private dialogRef: MatDialogRef<RentRateComponent>,
		@Inject(MAT_DIALOG_DATA) public data$: Observable<IOffer>,
		private rentsService: RentsService,
		private snackbarService: MatSnackBar
	) {
		super();
	}

	public onSubmit(): void {
		this.data$
			.pipe(
				switchMap(data =>
					this.rentsService.postOpinion(data.offerId, this.form.value)
				),
				takeUntil(this.destroyed)
			)
			.subscribe(() => {
				this.snackbarService.open('Opinia została dodana', 'Zamknij', {
					duration: 2000,
				});
				this.dialogRef.close();
			});
	}
}
