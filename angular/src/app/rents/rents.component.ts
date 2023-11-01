import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RentsService } from './services/rents.service';
import { MatTableDataSource } from '@angular/material/table';
import { IMenuOptions, IRent } from './models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { RentsDialogComponent } from './components/dialog/rents-dialog.component';
import { Observable, map } from 'rxjs';

@Component({
	selector: 'app-rents',
	templateUrl: './rents.component.html',
	styleUrls: ['./rents.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsComponent {
	public actualRent: IRent = this.rentsService.rents[0];
	public dataSource = new MatTableDataSource(this.actualRent.payments);
	public rentId$: Observable<string>;

	constructor(
		public rentsService: RentsService,
		private router: Router,
		public dialog: MatDialog,
		public route: ActivatedRoute
	) {
		this.rentId$ = route.paramMap.pipe(map(params => params.get('id') ?? ''));
	}

	public showRent(rent: IRent) {
		this.actualRent = rent;
		this.dataSource = new MatTableDataSource(this.actualRent.payments);
	}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}

	public openDialog(): void {
		const dialogRef = this.dialog.open(RentsDialogComponent, {
			data: this.actualRent,
		});
		dialogRef.afterClosed().subscribe(result => {
			result;
		});
	}

	public onSelect(menuOption: IMenuOptions) {
		switch (menuOption.option) {
			case 'startDispute': {
				break;
			}
			case 'closeRent': {
				this.openDialog();
				break;
			}
		}
	}
}
