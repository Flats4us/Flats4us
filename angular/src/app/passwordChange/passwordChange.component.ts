import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-email-change',
	templateUrl: './passwordChange.component.html',
	styleUrls: ['./passwordChange.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PasswordChangeComponent {
	public hidePassword = true;
}
