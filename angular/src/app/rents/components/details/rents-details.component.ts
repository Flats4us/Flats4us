import {
	ChangeDetectionStrategy,
	Component,
	EventEmitter,
	OnDestroy,
	OnInit,
	Output,
} from '@angular/core';
import { RentsService } from '../../services/rents.service';
import { IMenuOptions, IRent } from '../../models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { RentsDialogComponent } from '../dialog/rents-dialog.component';
import {
	BehaviorSubject,
	Observable,
	Subject,
	map,
	switchMap,
	takeUntil,
} from 'rxjs';
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
	@Output() public updatedRent = new EventEmitter<BehaviorSubject<IRent>>();

	public statusName: typeof statusName = statusName;
	public actualRent$?: Observable<IRent>;
	public actualRentSubject$?: BehaviorSubject<IRent> = new BehaviorSubject(
		{} as IRent
	);
	private rentId$?: Observable<string>;
	private readonly unsubscribe$: Subject<void> = new Subject();

	public currentIndex = 0;

	constructor(
		public rentsService: RentsService,
		private router: Router,
		private dialog: MatDialog,
		private route: ActivatedRoute
	) {}
	public ngOnInit(): void {
		this.rentId$ = this.route.paramMap.pipe(
			map(params => params.get('id') ?? '')
		);
		this.actualRent$ = this.rentId$.pipe(
			switchMap(value => this.rentsService.getRent(value))
		);
		this.actualRent$.subscribe(this.actualRentSubject$);
	}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}

	public openDialog(): void {
		const dialogRef = this.dialog.open(RentsDialogComponent, {
			data: this.actualRentSubject$,
		});
		dialogRef
			.afterClosed()
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(result => {
				this.actualRentSubject$?.pipe(switchMap(result));
				this.updatedRent.emit(result);
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

	public prevSlide(length: number) {
		this.currentIndex = this.currentIndex < length - 1 ? ++this.currentIndex : 0;
	}

	public nextSlide(length: number) {
		this.currentIndex = this.currentIndex > 0 ? --this.currentIndex : length - 1;
	}
}
