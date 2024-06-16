import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import {
	FormControl,
	FormGroup,
	FormsModule,
	ReactiveFormsModule,
	UntypedFormGroup,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { RentsService } from '../../../rents/services/rents.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseComponent } from '@shared/components/base/base.component';

@Component({
	selector: 'app-start-dispute-dialog',
	templateUrl: 'start-dispute-dialog.component.html',
	standalone: true,
	imports: [
		CommonModule,
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		ReactiveFormsModule,
	],
})
export class StartDisputeDialogComponent extends BaseComponent {
	public startDisputeForm: FormGroup = new UntypedFormGroup({
		title: new FormControl<string>('', Validators.required),
		description: new FormControl<string>('', Validators.required),
	});

	constructor(
		@Inject(MAT_DIALOG_DATA) public data: number,
		private service: RentsService,
		private snackBar: MatSnackBar
	) {
		super();
	}

	public onSubmit() {
		this.service
			.startDispute(
				this.startDisputeForm.value.title,
				this.startDisputeForm.value.description,
				this.data
			)
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open('Pomyślnie dodano interwencję!', 'Zamknij', {
					duration: 10000,
				})
			);
	}
}
