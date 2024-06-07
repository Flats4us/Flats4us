import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable, filter, map, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { UserService } from '@shared/services/user.service';
import { IMyProfile, IUser } from '@shared/models/user.models';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-profile',
	templateUrl: './profile.component.html',
	styleUrls: ['./profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileComponent {
	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected user$: Observable<IMyProfile | IUser> = this.route.paramMap.pipe(
		map(params => params.get('id')),
		filter(Boolean),
		switchMap(id => {
			return id === 'my'
				? this.userService.getMyProfile()
				: this.userService.getUserById(id);
		})
	);

	constructor(private route: ActivatedRoute, public userService: UserService) {}
}
