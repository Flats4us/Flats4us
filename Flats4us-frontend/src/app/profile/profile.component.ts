import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable, filter, map, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { UserService } from '@shared/services/user.service';
import { IMyProfile, IUser, IUserOpinion } from '@shared/models/user.models';
import { ActivatedRoute } from '@angular/router';
import { BaseComponent } from '@shared/components/base/base.component';
import { AuthService } from '@shared/services/auth.service';
import { AuthModels } from '@shared/models/auth.models';

@Component({
	selector: 'app-profile',
	templateUrl: './profile.component.html',
	styleUrls: ['./profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileComponent extends BaseComponent {
	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected id = '';
	protected myId = 0;
	protected user$: Observable<IMyProfile | IUser> = this.route.paramMap.pipe(
		map(params => params.get('id')),
		filter(Boolean),
		switchMap(id => {
			return id === 'my'
				? this.userService.getMyProfile()
				: this.userService.getUserById(id);
		})
	);
	public authModel = AuthModels;

	constructor(
		private route: ActivatedRoute,
		public userService: UserService,
		public authService: AuthService
	) {
		super();
		this.route.paramMap
			.pipe(
				map(params => params.get('id')),
				filter(Boolean),
				this.untilDestroyed()
			)
			.subscribe(id => (this.id = id));
		this.userService
			.getMyProfile()
			.pipe(this.untilDestroyed())
			.subscribe(profile => (this.myId = profile.userId));
	}

	public checkIfYetAssesed(opinions: IUserOpinion[]): boolean {
		return !!opinions.find(opinion => opinion.sourceUserId === this.myId);
	}
}
