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
import { slideAnimation } from './slide.animation';

@Component({
	selector: 'app-rents',
	templateUrl: './rents.component.html',
	styleUrls: ['./rents.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsComponent implements OnDestroy {
	public rentsOptions$: Observable<IRent[]>;
	public actualRent: any;
	public dataSource: MatTableDataSource<IPayment> = new MatTableDataSource();
	public rentId$: Observable<string>;
	private readonly unsubscribe$: Subject<void> = new Subject();
	public currentIndex = 0;

	constructor(
		public rentsService: RentsService,
		private router: Router,
		public dialog: MatDialog,
		public route: ActivatedRoute,
		private changeDetectorRef: ChangeDetectorRef
	) {
		this.rentId$ = route.paramMap.pipe(map(params => params.get('id') ?? ''));
		this.rentsOptions$ = this.rentsService.getRents();
		this.dataSource = new MatTableDataSource(this.actualRent.payments);
		this.rentsOptions$.pipe(takeUntil(this.unsubscribe$)).subscribe(rents => {
			this.actualRent = rents[0];
			this.dataSource = new MatTableDataSource(this.actualRent.payments);
		});
	}

	public showRent(rent: IRent) {
		this.actualRent = rent;
		this.dataSource = new MatTableDataSource(this.actualRent.payments);
		this.changeDetectorRef.detectChanges();
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
