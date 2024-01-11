import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, Observable, switchMap } from 'rxjs';
import { environment } from '../../environments/environment.prod';
import { UserService } from '@shared/services/user.service';
import { IUser } from '@shared/models/user.models';

@Component({
	selector: 'app-profile',
	templateUrl: './profile.component.html',
	styleUrls: ['./profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileComponent {
	public id$: Observable<string>;
	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected user$: Observable<IUser>;

	constructor(private route: ActivatedRoute, public userService: UserService) {
		this.id$ = this.route.paramMap.pipe(
			map(params => params.get('id')),
			filter(Boolean)
		);

		this.user$ = this.id$.pipe(
			switchMap(id => this.userService.getUserById(id)) // Replace getUserById with the actual method name that calls the API
		);
	}
}
