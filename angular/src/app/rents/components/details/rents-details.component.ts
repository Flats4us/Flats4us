import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
} from '@angular/core';
import { IMenuOptions, IRent } from '../../models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable, map, switchMap } from 'rxjs';
import { slideAnimation } from '../../slide.animation';
import { statusName } from '../../statusName';
import { environment } from 'src/environments/environment.prod';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { MeetingAddComponent } from '../meeting-add/meeting-add.component';
import { UserType } from 'src/app/profile/models/types';
import { RentsService } from '../../services/rents.service';
import { RentRateComponent } from '../rent-rate/rent-rate.component';
import { AuthService } from '@shared/services/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from '@shared/services/user.service';

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

	public statusName: typeof statusName = statusName;
	public user$ = this.route.parent?.paramMap.pipe(
		map(params => params.get('user')?.toUpperCase() ?? '')
	);
	private rentId$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('id') ?? '')
	);
	public actualRent$: Observable<IRent> = this.rentId$?.pipe(
		switchMap(value => this.rentsService.getRentById(parseInt(value)))
	);
	public student$ = this.userService.getMyProfile();

	public currentIndex = 0;

	public displayedColumns: string[] = [
		'paymentId',
		'paymentPurpose',
		'amount',
		'isPaid',
		'createdDate',
		'paymentDate',
		'actions',
	];

	public menuOptions: IMenuOptions[] = [
		{ option: 'rentDetails', description: 'Szczegóły najmu' },
		{ option: 'offerDetails', description: 'Powiązana oferta' },
		{ option: 'propertyDetails', description: 'Powiązana nieruchomość' },
		{ option: 'startDispute', description: 'Rozpocznij spór' },
	];

	constructor(
		public realEstateService: RealEstateService,
		public rentsService: RentsService,
		private router: Router,
		private dialog: MatDialog,
		private route: ActivatedRoute,
		public authService: AuthService,
		private snackBar: MatSnackBar,
		private cdr: ChangeDetectorRef,
		private userService: UserService
	) {
		super();
	}

	public addOffer() {
		this.router.navigate(['offer', 'add']);
	}

	public navigateToRent(id: number) {
		this.router.navigate(['rents', 'details', id]);
	}
	public navigateToOffer(id: number) {
		this.router.navigate(['offer', 'owner', id]);
	}
	public navigateToProperty(id: number) {
		this.router.navigate(['real-estate', 'owner', id]);
	}
	public startDispute(id: number) {
		this.router.navigate(['disputes', id]);
	}

	public onSelect(
		menuOption: IMenuOptions,
		rentId?: number,
		offerId?: number,
		propertyId?: number
	) {
		switch (menuOption.option) {
			case 'rentDetails': {
				this.navigateToRent(rentId ?? 0);
				break;
			}
			case 'offerDetails': {
				this.navigateToOffer(offerId ?? 0);
				break;
			}
			case 'propertyDetails': {
				this.navigateToProperty(propertyId ?? 0);
				break;
			}
			case 'startDispute': {
				this.startDispute(rentId ?? 0);
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

	public navigateStudentRents() {
		this.router.navigate(['rents', 'student']);
	}

	public navigateOwnerRents() {
		this.router.navigate(['rents', 'owner']);
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

	public makePayment(paymentId: number) {
		this.rentsService
			.makePayment(paymentId)
			.pipe(this.untilDestroyed())
			.subscribe(() => {
				this.snackBar.open('Płatność została pomyślnie opłacona!', 'Zamknij', {
					duration: 10000,
				});
				this.actualRent$ = this.rentId$?.pipe(
					switchMap(value => this.rentsService.getRentById(parseInt(value)))
				);
				this.cdr.detectChanges();
			});
	}
}
