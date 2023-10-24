import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RentsService } from './services/rents.service';
import { MatTableDataSource } from '@angular/material/table';

@Component({
	selector: 'app-rents',
	templateUrl: './rents.component.html',
	styleUrls: ['./rents.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsComponent {
	public title = this.rentsService.rents[0];
	public dataSource = new MatTableDataSource(this.rentsService.payments);

	constructor(public rentsService: RentsService) {}
}
