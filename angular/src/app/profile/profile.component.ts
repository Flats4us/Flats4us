import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, Observable } from 'rxjs';

@Component({
	selector: 'app-profile',
	templateUrl: './profile.component.html',
	styleUrls: ['./profile.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfileComponent {
	public id$: Observable<string>;

	constructor(private route: ActivatedRoute) {
		this.id$ = this.route.paramMap.pipe(
			map((params) => params.get('id')),
			filter(Boolean)
		);
	}
}
