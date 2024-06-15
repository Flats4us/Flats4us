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
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-change-dispute-status-dialog',
	templateUrl: './change-dispute-status-dialog.component.html',
	styleUrls: ['./change-dispute-status-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ChangeDisputeStatusDialogComponent extends BaseComponent {
	public statuses = [
		{ value: 0, name: 'Moderation-console.statutes0' },
		{ value: 1, name: 'Moderation-console.statutes0' },
		{ value: 2, name: 'Moderation-console.statutes0' },
		{ value: 3, name: 'Moderation-console.statutes0' },
		{ value: 4, name: 'Moderation-console.statutes0' },
	];

	public addInterventionForm: FormGroup;
	public justification = new FormControl('', Validators.required);

	constructor(
		@Inject(MAT_DIALOG_DATA) public data: { argumentId: number },
		private fb: FormBuilder,
		private service: ModerationConsoleService,
		private snackBar: MatSnackBar,
		private translate: TranslateService
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
				this.snackBar.open(
					this.translate.instant('Moderation-console.info1'),
					this.translate.instant('close'),
					{
						duration: 10000,
					}
				)
			);
	}
}
