import { ChangeDetectionStrategy, Component } from '@angular/core';
import { modificationType, userType } from '../../models/types';

@Component({
	selector: 'app-profile-owner-create',
	templateUrl: './create-profile.component.html',
	styleUrls: ['./create-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CreateOwnerProfileComponent {
	public mType = modificationType;
	public uType = userType;
}
