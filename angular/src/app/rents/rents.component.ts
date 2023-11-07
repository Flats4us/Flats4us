import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RentsService } from './services/rents.service';
import { IRent } from './models/rents.models';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { statusName } from './statusName';

@Component({
	selector: 'app-rents',
	templateUrl: './rents.component.html',
	styleUrls: ['./rents.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsComponent {
	public rentsOptions$: Observable<IRent[]> = this.rentsService.getRents();

	public statusName: typeof statusName = statusName;

	constructor(public rentsService: RentsService, private router: Router) {}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}
}
