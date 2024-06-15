import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable, filter, map, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { UserService } from '@shared/services/user.service';
import { IMyProfile, IUser } from '@shared/models/user.models';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '@shared/components/base/base.component';

@Component({
	selector: 'app-profile',
	templateUrl: './profile.component.html',
	styleUrls: ['./profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileComponent extends BaseComponent {
	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected id = '';
	protected user$: Observable<IMyProfile | IUser> = this.route.paramMap.pipe(
		map(params => params.get('id')),
		filter(Boolean),
		switchMap(id => {
			return id === 'my'
				? this.userService.getMyProfile()
				: this.userService.getUserById(id);
		})
	);

	constructor(private route: ActivatedRoute, public userService: UserService) {
		super();
		this.route.paramMap
			.pipe(
				map(params => params.get('id')),
				filter(Boolean),
				this.untilDestroyed()
			)
			.subscribe(id => (this.id = id));
	}
}
