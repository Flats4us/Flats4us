import { ChangeDetectionStrategy, Component } from '@angular/core';
import { BaseComponent } from '@shared/components/base/base.component';
import { Observable, switchMap } from 'rxjs';
import { ITechnicalProblem } from '../../models/moderation-console.models';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { formatDate } from '@angular/common';
import {
	animate,
	state,
	style,
	transition,
	trigger,
} from '@angular/animations';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-problems-verification',
	templateUrl: './problems-verification.component.html',
	animations: [
		trigger('detailExpand', [
			state('collapsed', style({ height: '0px', minHeight: '0' })),
			state('expanded', style({ height: '*' })),
			transition(
				'expanded <=> collapsed',
				animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')
			),
		]),
	],
	styleUrls: ['./problems-verification.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProblemsVerificationComponent extends BaseComponent {
	public pageSize = 4;
	public pageIndex = 0;
	public technicalProblems$: Observable<ITechnicalProblem[]> =
		this.service.getTechnicalProblems(this.pageSize, this.pageIndex);
	public problemsCount$ = this.service.getTechnicalProblemsCount();
	public displayedColumns: string[] = [
		'kind',
		'date',
		'userId',
		'solved',
		'actions',
	];
	public expandedElement: ITechnicalProblem | null = null;

	constructor(
		private service: ModerationConsoleService,
		private snackBar: MatSnackBar,
		private translate: TranslateService
	) {
		super();
	}

	public markAsSolved(technicalProblemId: number) {
		this.technicalProblems$ = this.service.markAsSolved(technicalProblemId).pipe(
			this.untilDestroyed(),
			switchMap(() => {
				this.snackBar.open(
					this.translate.instant('Report-problem.info2'),
					this.translate.instant('close'),
					{
						duration: 10000,
					}
				);
				return this.service.getTechnicalProblems(this.pageSize, this.pageIndex);
			})
		);
	}

	protected readonly formatDate = formatDate;
}
