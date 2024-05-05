import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BaseComponent } from '@shared/components/base/base.component';
import { Observable } from 'rxjs';
import {
	IProperty,
	ITechnicalProblem,
} from '../../models/moderation-console.models';
import { environment } from '../../../../environments/environment.prod';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-problems-verification',
	templateUrl: './problems-verification.component.html',
	styleUrls: ['./problems-verification.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProblemsVerificationComponent extends BaseComponent {
	public technicalProblems$: Observable<ITechnicalProblem[]> =
		this.service.getTechnicalProblems();
	protected baseUrl = environment.apiUrl.replace('/api', '');

	constructor(
		private service: ModerationConsoleService,
		private snackBar: MatSnackBar
	) {
		super();
	}

	public markAsSolved(technicalProblemId: number) {
		this.service
			.markAsSolved(technicalProblemId)
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open('Problem został oznaczony jako rozwiązany!', 'Zamknij', {
					duration: 2000,
				})
			);
	}
}
