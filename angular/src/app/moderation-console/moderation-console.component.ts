import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-moderation-console',
	templateUrl: './moderation-console.component.html',
	styleUrls: ['./moderation-console.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ModerationConsoleComponent {}
