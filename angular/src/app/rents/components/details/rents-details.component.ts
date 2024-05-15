import { ChangeDetectionStrategy, Component } from '@angular/core';
import { IMenuOptions, IRentOffer, IRentPayment } from '../../models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable, map, shareReplay, switchMap } from 'rxjs';
import { slideAnimation } from '../../slide.animation';
import { statusName } from '../../statusName';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { environment } from 'src/environments/environment.prod';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { MeetingAddComponent } from '../meeting-add/meeting-add.component';
import { IOffer } from 'src/app/offer/models/offer.models';
import { UserType } from 'src/app/profile/models/types';
import { OfferService } from 'src/app/offer/services/offer.service';
import { RentsService } from '../../services/rents.service';
import { RentRateComponent } from '../rent-rate/rent-rate.component';
import { AuthService } from '@shared/services/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';

@Component({
	selector: 'app-rents-details',
	templateUrl: './rents-details.component.html',
	styleUrls: ['./rents-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsDetailsComponent extends BaseComponent {
	protected baseUrl = environment.apiUrl.replace('/api', '');
	public uType = UserType;

	public separatorKeysCodes: number[] = [ENTER, COMMA];
	public statusName: typeof statusName = statusName;
	public user$ = this.route.parent?.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);
	private rentId$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('id') ?? '')
	);
	public actualRent$: Observable<IOffer> = this.rentId$?.pipe(
		switchMap(value => this.offerService.getOfferById(parseInt(value))),
		shareReplay(1)
	);

	public actualOfferRent$: Observable<IRentOffer | undefined> = this.rentId$?.pipe(
		switchMap(value => this.rentsService.getOfferRents(0,40).pipe(map(rents => rents.result.find(rent => rent.rentId === parseInt(value))))),
		shareReplay(1)
	);
	public payments: IRentPayment[] = [];

	public currentIndex = 0;

	public displayedColumnsStudent: string[] = ['paymentId', 'paymentPurpose', 'amount', 'isPaid','createdDate', 'paymentDate'];
	public displayedColumnsOwner: string[] = ['paymentId', 'paymentPurpose', 'amount', 'isPaid','createdDate', 'paymentDate'];
	public menuOptions: IMenuOptions[] = [
		{ option: 'rentDetails', description: 'Szczegóły najmu' },
		{ option: 'startDispute', description: 'Rozpocznij spór' }
	];

	constructor(
		public realEstateService: RealEstateService,
		public offerService: OfferService,
		public rentsService: RentsService,
		private router: Router,
		private dialog: MatDialog,
		private route: ActivatedRoute,
		public authService: AuthService
	) {
		super();
		this.actualOfferRent$.pipe(this.untilDestroyed()).subscribe(rent => this.payments = rent?.payments ?? []);
	}

	public addOffer() {
		this.router.navigate(['offer', 'add']);
	}

	public navigateToRent(id: number) {
		this.router.navigate(['rents', 'details', id]);
	}
	public startDispute(id: number) {
		this.router.navigate(['disputes', id]);
	}

	public onSelect(menuOption: IMenuOptions, id?: number) {
		switch (menuOption.option) {
			case 'rentDetails': {
				this.navigateToRent(id ?? 0);
				break;
			}
			case 'startDispute': {
				this.startDispute(id ?? 0);
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

	public onRate(): void {
		this.dialog.open(RentRateComponent, {
			disableClose: true,
			data: this.actualRent$,
		});
	}

	public showProfile(id: number) {
		this.router.navigate(['profile', 'details', 'student', id]);
	}

	public setCurrentSlideIndex(index: number) {
		this.currentIndex = index;
	}

	public isCurrentSlideIndex(index: number) {
		return this.currentIndex === index;
	}

	public prevSlide(length?: number) {
		this.currentIndex =
			this.currentIndex < (length ?? 0) - 1 ? ++this.currentIndex : 0;
	}

	public nextSlide(length?: number) {
		this.currentIndex =
			this.currentIndex > 0 ? --this.currentIndex : (length ?? 0) - 1;
	}
}
