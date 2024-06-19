import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { formatDate } from '@angular/common';

import {
	animate,
	state,
	style,
	transition,
	trigger,
} from '@angular/animations';
import { MatDialog } from '@angular/material/dialog';
import { AddInterventionDialogComponent } from '../add-intervention-dialog/add-intervention-dialog.component';
import { ChangeDisputeStatusDialogComponent } from '../change-dispute-status-dialog/change-dispute-status-dialog.component';
import { IDispute } from '../../models/moderation-console.models';
import { BaseComponent } from '@shared/components/base/base.component';

@Component({
	selector: 'app-dispute',
	templateUrl: './dispute.component.html',
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
	styleUrls: ['./dispute.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputeComponent extends BaseComponent {
	public disputes$ = this.service.getDisputes();
	public displayedColumns: string[] = [
		'title',
		'startDate',
		'interventionNeed',
		'rentId',
		'student',
		'owner',
		'argumentStatus',
		'actions',
	];
	public expandedElement: IDispute | null = null;

	constructor(
		private service: ModerationConsoleService,
		private dialog: MatDialog
	) {
		super();
	}

	public openInterventionDialog(argumentId: number): void {
		this.dialog.open(AddInterventionDialogComponent, {
			height: '300px',
			width: '500px',
			data: { argumentId: argumentId },
		});
	}

	public openChangeStatusDialog(argumentId: number): void {
		this.dialog.open(ChangeDisputeStatusDialogComponent, {
			width: '500px',
			data: { argumentId: argumentId },
		});
	}
	protected readonly formatDate = formatDate;

	public joinGroupChat(groupChatId: number) {
		this.service
			.joinGroupChat(groupChatId)
			.pipe(this.untilDestroyed())
			.subscribe();
	}
}
