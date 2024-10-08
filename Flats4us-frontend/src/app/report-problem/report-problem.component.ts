import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
	FormControl,
	FormsModule,
	ReactiveFormsModule,
	UntypedFormGroup,
	Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSelectModule } from '@angular/material/select';
import { ReportProblemService } from './services/report-problem.service';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-report-problem',
	standalone: true,
	imports: [
		CommonModule,
		FormsModule,
		MatButtonModule,
		MatCardModule,
		MatFormFieldModule,
		MatInputModule,
		ReactiveFormsModule,
		MatSelectModule,
		TranslateModule,
	],
	providers: [MatSnackBar],
	templateUrl: './report-problem.component.html',
	styleUrls: ['./report-problem.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ReportProblemComponent extends BaseComponent {
	public form = new UntypedFormGroup({
		kind: new FormControl<number>(0, [Validators.required]),
		description: new FormControl<string>('', [Validators.required]),
	});

	constructor(
		private service: ReportProblemService,
		private snackBar: MatSnackBar,
		private translate: TranslateService
	) {
		super();
	}

	public reportProblem() {
		this.service
			.reportProblem({
				kind: this.form.value.kind,
				description: this.form.value.description,
			})
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open(
					this.translate.instant('Report-problem.info1'),
					this.translate.instant('close'),
					{
						duration: 10000,
					}
				)
			);
	}
}
