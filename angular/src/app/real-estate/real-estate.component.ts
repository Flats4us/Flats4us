import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Observable } from 'rxjs';
import { statusName } from '../rents/statusName';
import { Router } from '@angular/router';
import { RealEstateService } from './services/real-estate.service';
import { IProperty } from './models/real-estate.models';
import { AuthService } from '@shared/services/auth.service';

@Component({
	selector: 'app-real-estate',
	templateUrl: './real-estate.component.html',
	styleUrls: ['./real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RealEstateComponent {
	public realEstateOptions$: Observable<IProperty[]> =
		this.realEstateService.getRealEstates(false);

	public statusName: typeof statusName = statusName;

	constructor(
		public realEstateService: RealEstateService,
		private router: Router,
		public authService: AuthService
	) {}

	public addRealEstate() {
		this.router.navigate(['real-estate', 'add']);
	}

	public onDeactivate() {
		this.realEstateOptions$ = this.realEstateService.getRealEstates(false);
	}
}
