import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RentsService } from './services/rents.service';
import { ISendRent } from './models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, map } from 'rxjs';
import { statusName } from './statusName';
import { UserType } from '../profile/models/types';
import { AuthService } from '@shared/services/auth.service';
import { RealEstateService } from '../real-estate/services/real-estate.service';
import { AuthModels } from '@shared/models/auth.models';

@Component({
	selector: 'app-rents',
	templateUrl: './rents.component.html',
	styleUrls: ['./rents.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsComponent {
	public uType = UserType;
	public rentsOffers$: Observable<ISendRent> | undefined;
	public user$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);

	public statusName: typeof statusName = statusName;

	constructor(
		public rentsService: RentsService,
		public realEstateService: RealEstateService,
		private router: Router,
		private route: ActivatedRoute,
		public authService: AuthService
	) {
		if (this.authService.getUserType() !== AuthModels.MODERATOR) {
			this.rentsOffers$ = this.rentsService.getRents();
		}
	}
}
