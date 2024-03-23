import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { UserService } from '@shared/services/user.service';
import { IMyProfile } from '@shared/models/user.models';

@Component({
	selector: 'app-profile',
	templateUrl: './profile.component.html',
	styleUrls: ['./profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileComponent {
	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected user$: Observable<IMyProfile> = this.userService.getMyProfile();

	constructor(public userService: UserService) {}
}
