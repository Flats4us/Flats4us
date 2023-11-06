import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-agreement',
	templateUrl: './agreement.component.html',
	styleUrls: ['./agreement.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AgreementComponent {}
