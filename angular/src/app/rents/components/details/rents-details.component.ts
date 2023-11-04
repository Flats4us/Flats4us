import {
	ChangeDetectionStrategy,
	Component,
	EventEmitter,
	OnDestroy,
	OnInit,
	Output,
} from '@angular/core';
import { RentsService } from '../../services/rents.service';
import { MatTableDataSource } from '@angular/material/table';
import { IMenuOptions, IPayment, IRent } from '../../models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { RentsDialogComponent } from '../dialog/rents-dialog.component';
import { Observable, Subject, map, takeUntil } from 'rxjs';
import { slideAnimation } from '../../slide.animation';
import { statusName } from '../../statusName';

@Component({
	selector: 'app-rents-details',
	templateUrl: './rents-details.component.html',
	styleUrls: ['./rents-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsDetailsComponent implements OnInit, OnDestroy {
	@Output() public updatedState = new EventEmitter<string>();

	public statusName: typeof statusName = statusName;
	public actualRent = this.rentsService.initialRent;
	public actualRent$?: Observable<IRent | null | undefined>;
	public dataSource: MatTableDataSource<IPayment> = new MatTableDataSource();
	public rentId$?: Observable<string>;
	private readonly unsubscribe$: Subject<void> = new Subject();
	public currentIndex = 0;

	constructor(
		public rentsService: RentsService,
		private router: Router,
		public dialog: MatDialog,
		public route: ActivatedRoute
	) {}
	public ngOnInit(): void {
		this.rentId$ = this.route.paramMap.pipe(
			map(params => params.get('id') ?? '')
		);
		this.rentId$.pipe(takeUntil(this.unsubscribe$)).subscribe(value => {
			this.actualRent$ = this.rentsService.getRent(value);
		});
		this.actualRent$?.pipe(takeUntil(this.unsubscribe$)).subscribe(value => {
			this.dataSource = new MatTableDataSource(value?.payments);
			this.actualRent ?? value;
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
			.subscribe(result => {
				this.actualRent = result;
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
