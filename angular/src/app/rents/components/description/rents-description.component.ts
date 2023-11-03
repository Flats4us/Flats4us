import { ChangeDetectionStrategy, Component, OnDestroy } from '@angular/core';
import { RentsService } from '../../services/rents.service';
import { MatTableDataSource } from '@angular/material/table';
import { IMenuOptions, IPayment } from '../../models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { RentsDialogComponent } from '../dialog/rents-dialog.component';
import { Observable, Subject, map, takeUntil } from 'rxjs';
import { slideAnimation } from '../../slide.animation';

@Component({
	selector: 'app-rents-description',
	templateUrl: './rents-description.component.html',
	styleUrls: ['./rents-description.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsDescriptionComponent implements OnDestroy {
	public id = '1';
	public actualRent = this.rentsService.initialRent;
	public dataSource: MatTableDataSource<IPayment>;
	public rentId$: Observable<string>;
	private readonly unsubscribe$: Subject<void> = new Subject();
	public currentIndex = 0;

	constructor(
		public rentsService: RentsService,
		private router: Router,
		public dialog: MatDialog,
		public route: ActivatedRoute
	) {
		this.rentId$ = route.paramMap.pipe(map(params => params.get('id') ?? ''));
		this.rentId$
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(id => (this.id = id));
		this.rentsService
			.getRent(this.id)
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(rent => {
				if (rent) {
					this.actualRent = rent;
				}
			});
		this.dataSource = new MatTableDataSource(this.actualRent.payments);
	}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}

	public openDialog(): void {
		const dialogRef = this.dialog.open(RentsDialogComponent);
		dialogRef
			.afterClosed()
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(result => {
				if (result) {
					this.actualRent.status = 'suspended';
				}
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

	public setCurrentSlideIndex(index: number) {
		this.currentIndex = index;
	}

	public isCurrentSlideIndex(index: number) {
		return this.currentIndex === index;
	}

	public prevSlide() {
		this.currentIndex =
			this.currentIndex < this.actualRent.imageArray.length - 1
				? ++this.currentIndex
				: 0;
	}

	public nextSlide() {
		this.currentIndex =
			this.currentIndex > 0
				? --this.currentIndex
				: this.actualRent.imageArray.length - 1;
	}
}
