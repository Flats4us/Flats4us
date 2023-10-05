import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-conversations',
	templateUrl: './conversations.component.html',
	styleUrls: ['./conversations.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ConversationsComponent {}
