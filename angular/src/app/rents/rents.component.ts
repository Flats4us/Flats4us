import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RentsService } from './services/rents.service';
import { MatTableDataSource } from '@angular/material/table';
import { IRent } from './models/rents.models';
import { Router } from '@angular/router';

@Component({
	selector: 'app-rents',
	templateUrl: './rents.component.html',
	styleUrls: ['./rents.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsComponent {
	public actualRent: IRent = this.rentsService.rents[0];
	public dataSource = new MatTableDataSource(this.actualRent.payments);

	constructor(public rentsService: RentsService, private router: Router) {}

	public showRent(rent: IRent) {
		this.actualRent = rent;
	}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}
}
