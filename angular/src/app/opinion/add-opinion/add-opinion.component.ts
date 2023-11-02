import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-add-opinion',
	templateUrl: './add-opinion.component.html',
	styleUrls: ['./add-opinion.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddOpinionComponent {}
