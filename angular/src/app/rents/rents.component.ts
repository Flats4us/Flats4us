import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RentsService } from './services/rents.service';
import { IRent } from './models/rents.models';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
	selector: 'app-rents',
	templateUrl: './rents.component.html',
	styleUrls: ['./rents.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsComponent {
	public rentsOptions$: Observable<IRent[]>;

	constructor(public rentsService: RentsService, private router: Router) {
		this.rentsOptions$ = this.rentsService.getRents();
	}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}
}
