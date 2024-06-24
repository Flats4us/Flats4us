import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable, map } from 'rxjs';
import { statusName } from '../rents/statusName';
import { ActivatedRoute, Router } from '@angular/router';
import { RealEstateService } from './services/real-estate.service';
import { IProperty } from './models/real-estate.models';
import { AuthService } from '@shared/services/auth.service';
import { AuthModels } from '@shared/models/auth.models';
import { UserType } from '../profile/models/types';

@Component({
	selector: 'app-real-estate',
	templateUrl: './real-estate.component.html',
	styleUrls: ['./real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RealEstateComponent {
	public realEstateOptions$: Observable<IProperty[]> | undefined;

	public uType = UserType;
	public user$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);

	public statusName: typeof statusName = statusName;

	public authModels = AuthModels;

	constructor(
		public realEstateService: RealEstateService,
		private router: Router,
		public authService: AuthService,
		private route: ActivatedRoute
	) {
		if (this.authService.getUserType() !== AuthModels.MODERATOR) {
			this.realEstateOptions$ = this.realEstateService.getRealEstates(false);
		}
	}

	public addRealEstate() {
		this.router.navigate(['real-estate', 'add']);
	}

	public onDeactivate() {
		this.realEstateOptions$ = this.realEstateService.getRealEstates(false);
	}
}
