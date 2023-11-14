import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-profile-edit',
	templateUrl: './edit-profile.component.html',
	styleUrls: ['./edit-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditProfileComponent {
	newPassword = true;
	actualPassword = true;
}
