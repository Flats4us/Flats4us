import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ModerationConsoleService } from '../../services/moderation-console.service';

@Component({
	selector: 'app-dispute',
	templateUrl: './dispute.component.html',
	styleUrls: ['./dispute.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputeComponent {
	public disputes$ = this.service.getDisputes();
	public displayedColumns: string[] = [
		'disputeBetween',
		'createdBy',
		'creationDate',
		'moderatorAdditionDate',
		'actions',
	];

	constructor(private service: ModerationConsoleService) {}
}
