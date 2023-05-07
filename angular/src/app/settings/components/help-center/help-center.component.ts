import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-help-center',
	templateUrl: './help-center.component.html',
	styleUrls: ['./help-center.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HelpCenterComponent {}
