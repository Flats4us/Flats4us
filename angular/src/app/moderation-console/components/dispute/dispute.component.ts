import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IDispute } from './IDispute';
import { ModerationConsoleService } from '../../services/moderation-console.service';

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
	public dataSource$: Observable<IDispute[]>;
	public columnsToDisplayWithExpand = [
		...this.columnsToDisplay.keys(),
		'expand',
	];

	constructor(private service: ModerationConsoleService) {
		this.dataSource$ = this.loadData();
	}

	private loadData(): Observable<IDispute[]> {
		return this.service.getDisputes();
	}
}
