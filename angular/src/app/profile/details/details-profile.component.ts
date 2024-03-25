import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable, filter, map, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { IUser } from '@shared/models/user.models';
import { UserService } from '@shared/services/user.service';
import { ActivatedRoute } from '@angular/router';

@Component({
	selector: 'app-details-profile',
	templateUrl: './details-profile.component.html',
	styleUrls: ['./details-profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DetailsProfileComponent {
	public id$: Observable<string>;
	protected baseUrl = environment.apiUrl.replace('/api', '');
	protected user$: Observable<IUser>;

	constructor(private route: ActivatedRoute, public userService: UserService) {
		this.id$ = this.route.paramMap.pipe(
			map(params => params.get('id')),
			filter(Boolean)
		);

		this.user$ = this.id$.pipe(
			switchMap(id => this.userService.getUserById(parseInt(id)))
		);
	}
}
