import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { BaseComponent } from '@shared/components/base/base.component';

@Component({
	selector: 'app-change-dispute-status-dialog',
	templateUrl: './change-dispute-status-dialog.component.html',
	styleUrls: ['./change-dispute-status-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChangeDisputeStatusDialogComponent extends BaseComponent {
	public statuses = [
		{ value: 0, name: 'Trwający' },
		{ value: 1, name: 'Rozwiązany' },
		{ value: 2, name: 'Nieuzasadniony' },
		{ value: 3, name: 'Rozwiązany przez moderatora' },
		{ value: 4, name: 'Nieuzasadniony przez moderatora' },
	];

	public addInterventionForm: FormGroup;
	public justification = new FormControl('', Validators.required);

	constructor(
		@Inject(MAT_DIALOG_DATA) public data: { argumentId: number },
		private fb: FormBuilder,
		private service: ModerationConsoleService,
		private snackBar: MatSnackBar
	) {
		super();
		this.addInterventionForm = this.fb.group({
			status: [0, Validators.required],
		});
	}

	public onSubmit() {
		this.service
			.changeDisputeStatus(
				this.data.argumentId,
				this.addInterventionForm.value.status
			)
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open('Pomyślnie dodano interwencję!', 'Zamknij', {
					duration: 10000,
				})
			);
	}
}
