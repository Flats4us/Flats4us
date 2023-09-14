import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-roommate-candidate',
	templateUrl: './roommate-candidate.component.html',
	styleUrls: ['./roommate-candidate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RoommateCandidateComponent {}
