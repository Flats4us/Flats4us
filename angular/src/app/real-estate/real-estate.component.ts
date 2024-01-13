import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { statusName } from '../rents/statusName';
import { Router } from '@angular/router';
import { RealEstateService } from './services/real-estate.service';
import { IProperty } from './models/real-estate.models';

@Component({
	selector: 'app-real-estate',
	templateUrl: './real-estate.component.html',
	styleUrls: ['./real-estate.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RealEstateComponent implements OnInit {
	public realEstateOptions$: Observable<IProperty[]> =
		this.realEstateService.getRealEstates(false);

	public statusName: typeof statusName = statusName;

	constructor(
		public realEstateService: RealEstateService,
		private router: Router
	) {}

	public ngOnInit(): void {
		this.realEstateOptions$ = this.realEstateService.getRealEstates(false);
	}

	public addRealEstate() {
		this.router.navigate(['/real-estate', 'add']);
	}
}
