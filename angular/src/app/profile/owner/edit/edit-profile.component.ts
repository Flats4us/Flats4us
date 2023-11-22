import { ChangeDetectionStrategy, Component } from '@angular/core';
import { modificationType, userType } from '../../models/types';

@Component({
	selector: 'app-profile-owner-edit',
	templateUrl: './edit-profile.component.html',
	styleUrls: ['./edit-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class EditOwnerProfileComponent {
	public mType = modificationType;
	public uType = userType;
}
