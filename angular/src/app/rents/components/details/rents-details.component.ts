import {
	ChangeDetectionStrategy,
	Component,
	OnDestroy,
	OnInit,
} from '@angular/core';
import { RentsService } from '../../services/rents.service';
import { IMenuOptions, IRent } from '../../models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { RentsDialogComponent } from '../dialog/rents-dialog.component';
import { Observable, Subject, map, of, switchMap, takeUntil } from 'rxjs';
import { slideAnimation } from '../../slide.animation';
import { statusName } from '../../statusName';
import { StartDisputeDialogComponent } from '@shared/components/start-dispute-dialog/start-dispute-dialog.component';

@Component({
	selector: 'app-rents-details',
	templateUrl: './rents-details.component.html',
	styleUrls: ['./rents-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsDetailsComponent implements OnInit, OnDestroy {
	public statusName: typeof statusName = statusName;
	public actualRent$?: Observable<IRent>;
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
	}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}

	private startDispute() {
		this.dialog.open(StartDisputeDialogComponent, {
			width: '600px',
		});
	}

	public openDialog(actualRent: IRent): void {
		const dialogRef = this.dialog.open(RentsDialogComponent, {
			data: actualRent,
		});
		dialogRef
			.afterClosed()
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(result => {
				this.actualRent$ = of(result);
			});
	}

	public onSelect(menuOption: IMenuOptions, actualRent: IRent) {
		switch (menuOption.option) {
			case 'startDispute': {
				this.startDispute();
				break;
			}
			case 'closeRent': {
				this.openDialog(actualRent);
				break;
			}
		}
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

	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
