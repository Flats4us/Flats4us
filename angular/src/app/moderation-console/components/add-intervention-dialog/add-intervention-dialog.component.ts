import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import {
	FormBuilder,
	FormControl,
	FormGroup,
	Validators,
} from '@angular/forms';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-add-intervention-dialog',
	templateUrl: './add-intervention-dialog.component.html',
	styleUrls: ['./add-intervention-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddInterventionDialogComponent extends BaseComponent {
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
			justification: ['', Validators.required],
		});
	}

	public onSubmit() {
		this.service
			.addIntervention(
				this.addInterventionForm.value.justification,
				this.data.argumentId
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
