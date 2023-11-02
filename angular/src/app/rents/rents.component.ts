import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
	OnDestroy,
} from '@angular/core';
import { RentsService } from './services/rents.service';
import { MatTableDataSource } from '@angular/material/table';
import { IMenuOptions, IPayment, IRent } from './models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { RentsDialogComponent } from './components/dialog/rents-dialog.component';
import { Observable, Subject, map, takeUntil } from 'rxjs';

@Component({
	selector: 'app-rents',
	templateUrl: './rents.component.html',
	styleUrls: ['./rents.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsComponent implements OnDestroy {
	public rentsOptions$?: Observable<IRent[]>;
	public actualRent: any;
	public dataSource: MatTableDataSource<IPayment> = new MatTableDataSource();
	public rentId$: Observable<string>;
	private readonly unsubscribe$: Subject<void> = new Subject();

	constructor(
		public rentsService: RentsService,
		private router: Router,
		public dialog: MatDialog,
		public route: ActivatedRoute,
		private changeDetectionRef: ChangeDetectorRef
	) {
		this.rentId$ = route.paramMap.pipe(map(params => params.get('id') ?? ''));
		this.rentsOptions$ = this.rentsService.getRents();
		this.rentsOptions$.pipe(takeUntil(this.unsubscribe$)).subscribe(rents => {
			this.actualRent = rents[0];
			this.dataSource = new MatTableDataSource(this.actualRent.payments);
		});
	}

	public showRent(rent: IRent) {
		this.rentsOptions$?.pipe(takeUntil(this.unsubscribe$)).subscribe(() => {
			this.actualRent = rent;
			this.dataSource = new MatTableDataSource(this.actualRent.payments);
			this.changeDetectionRef.detectChanges();
		});
	}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}

	public openDialog(): void {
		const dialogRef = this.dialog.open(RentsDialogComponent, {
			data: this.actualRent,
		});
		dialogRef
			.afterClosed()
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(() => {
				this.changeDetectionRef.detectChanges();
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
	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
