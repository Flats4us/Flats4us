import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-dispute-conversation',
	templateUrl: './disputes.component.html',
	styleUrls: ['./disputes.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputesComponent {}
