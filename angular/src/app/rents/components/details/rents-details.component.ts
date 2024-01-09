import {
	ChangeDetectionStrategy,
	Component,
	ElementRef,
	OnDestroy,
	OnInit,
	ViewChild,
} from '@angular/core';
import { RentsService } from '../../services/rents.service';
import { IMenuOptions, IPayment } from '../../models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable, Subject, map, switchMap } from 'rxjs';
import { slideAnimation } from '../../slide.animation';
import { statusName } from '../../statusName';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { RentsTenantsDialogComponent } from '../dialog/rents-tenants-dialog.component';
import { environment } from 'src/environments/environment.prod';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { MeetingAddComponent } from '../meeting-add/meeting-add.component';
import { IOffer } from 'src/app/offer/models/offer.models';
import { userType } from 'src/app/profile/models/types';
import { RentsCancelDialogComponent } from '../dialog/rents-cancel-dialog.component';

@Component({
	selector: 'app-rents-details',
	templateUrl: './rents-details.component.html',
	styleUrls: ['./rents-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsDetailsComponent implements OnInit, OnDestroy {
	protected baseUrl = environment.apiUrl.replace('/api', '');
	public uType = userType;

	public separatorKeysCodes: number[] = [ENTER, COMMA];
	public statusName: typeof statusName = statusName;
	public actualRent$?: Observable<IOffer>;
	public user$?: Observable<string>;
	private rentId$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('id') ?? '')
	);
	private readonly unsubscribe$: Subject<void> = new Subject();
	public payments: IPayment[] = [
		{ sum: 1000, date: '20.12.2020', kind: 'CZYNSZ' },
	];

	public currentIndex = 0;

	public displayedColumnsStudent: string[] = ['sum', 'date', 'kind'];
	public displayedColumnsOwner: string[] = ['sum', 'date', 'kind', 'who'];
	public menuOptions: IMenuOptions[] = [
		{ option: 'offerDetails', description: 'Szczegóły oferty' },
		{ option: 'startDispute', description: 'Rozpocznij spór' },
		{ option: 'closeRent', description: 'Zakończ najem' },
	];

	constructor(
		public realEstateService: RealEstateService,
		public rentsService: RentsService,
		private router: Router,
		private dialog: MatDialog,
		private route: ActivatedRoute
	) {}
	public ngOnInit(): void {
		this.user$ = this.route.parent?.paramMap.pipe(
			map(params => params.get('user')?.toUpperCase() ?? '')
		);
		this.actualRent$ = this.rentId$?.pipe(
			switchMap(value => this.rentsService.getOfferById(parseInt(value)))
		);
	}

	public addOffer() {
		this.router.navigate(['offer/add']);
	}

	public openCancelDialog(actualRent: IOffer): void {
		this.dialog.open(RentsCancelDialogComponent, {
			disableClose: true,
			data: actualRent,
		});
	}

	public openTenantsDialog(): void {
		this.dialog.open(RentsTenantsDialogComponent, { disableClose: true });
	}
	public navigateToFlat(id: number) {
		this.router.navigate([`rents/details/${id}`]);
	}
	public startDispute(id: number) {
		this.router.navigate([`disputes/${id}`]);
	}

	public onSelect(menuOption: IMenuOptions, actualRent: IOffer) {
		switch (menuOption.option) {
			case 'offerDetails': {
				this.navigateToFlat(actualRent.offerId);
				break;
			}
			case 'startDispute': {
				this.startDispute(actualRent.offerId);
				break;
			}
			case 'closeRent': {
				this.openCancelDialog(actualRent);
				break;
			}
		}
	}

	public onAddMeeting(): void {
		this.dialog.open(MeetingAddComponent, {
			disableClose: true,
			data: this.rentId$,
		});
	}

	public startRent() {
		this.openTenantsDialog();
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
