import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { ModerationConsoleService } from '../../services/moderation-console.service';
import { IDispute } from '../../models/moderation-console.models';

@Component({
	selector: 'app-dispute',
	templateUrl: './dispute.component.html',
	styleUrls: ['./dispute.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputeComponent {
	public columnsToDisplay: Map<string, string> = new Map<string, string>([
		['disputeBetween', 'Spór między'],
		['createdBy', 'Stworzył spór'],
		['creationDate', 'Data stworzenia'],
		['moderatorAdditionDate', 'Data dodania moderatora'],
	]);
	public disputes$ = this.service.getDisputes();

	constructor(private service: ModerationConsoleService) {}
}
