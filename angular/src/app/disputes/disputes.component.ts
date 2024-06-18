import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { IDispute } from './models/disputes.models';
import { DisputesService } from './services/disputes.service';

@Component({
	selector: 'app-disputes',
	templateUrl: './disputes.component.html',
	styleUrls: ['./disputes.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DisputesComponent {
	public disputes$: Observable<IDispute[]> = this.disputesService.getDisputes();

	constructor(public disputesService: DisputesService) {}
}
