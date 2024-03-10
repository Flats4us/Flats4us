import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { RentsService } from './services/rents.service';
import { IRent } from './models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { statusName } from './statusName';
import { UserType } from '../profile/models/types';

@Component({
	selector: 'app-rents',
	templateUrl: './rents.component.html',
	styleUrls: ['./rents.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsComponent implements OnInit {
	public uType = UserType;
	public rentsOptions$: Observable<IRent[]> = this.rentsService.getRents();
	public user$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);

	public statusName: typeof statusName = statusName;

	constructor(
		public rentsService: RentsService,
		private router: Router,
		private route: ActivatedRoute
	) {}
	public ngOnInit(): void {
		this.rentsOptions$ = this.rentsService.getRents();
	}

	public addOffer() {
		this.router.navigate(['/offer', 'add']);
	}
}
