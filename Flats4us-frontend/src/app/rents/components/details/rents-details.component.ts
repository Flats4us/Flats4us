import {
	ChangeDetectionStrategy,
	ChangeDetectorRef,
	Component,
} from '@angular/core';
import { IMenuOptions, IRent, ITenant } from '../../models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, Observable, map, switchMap, zip } from 'rxjs';
import { slideAnimation } from '../../slide.animation';
import { statusName } from '../../statusName';
import { environment } from 'src/environments/environment';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { MeetingAddComponent } from '../meeting-add/meeting-add.component';
import { UserType } from 'src/app/profile/models/types';
import { RentsService } from '../../services/rents.service';
import { RentRateComponent } from '../rent-rate/rent-rate.component';
import { AuthService } from '@shared/services/auth.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserService } from '@shared/services/user.service';
import { AuthModels } from '@shared/models/auth.models';
import { TranslateService } from '@ngx-translate/core';
import { StartDisputeDialogComponent } from '@shared/components/start-dispute-dialog/start-dispute-dialog.component';

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
	private showRent: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
		false
	);
	public showRent$: Observable<boolean> = this.showRent.asObservable();
	public studentId = 0;

	public currentIndex = 0;

	public authModels = AuthModels;

	public displayedColumnsPayments: string[] = [
		'paymentId',
		'paymentPurpose',
		'amount',
		'isPaid',
		'createdDate',
		'paymentDate',
		'paidAtDate',
		'actions',
	];

	public menuOptions: IMenuOptions[] = [
		{ option: 'rentDetails', description: 'Rents-details.option-details' },
		{ option: 'offerDetails', description: 'Rents-details.option-offer' },
		{ option: 'propertyDetails', description: 'Rents-details.option-property' },
		{ option: 'startDispute', description: 'Rents-details.option-dispute' },
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
		private userService: UserService,
		private translate: TranslateService
	) {
		super();
		if (this.authService.getUserType() === AuthModels.MODERATOR) {
			this.showRent.next(true);
		} else {
			zip(this.rentId$, this.rentsService.getRents())
				.pipe(this.untilDestroyed())
				.subscribe(([id, rents]) => {
					const result = rents.result.find(rents => rents.rentId === parseInt(id));
					this.showRent.next(!!result);
				});
			this.userService
				.getMyProfile()
				.pipe(this.untilDestroyed())
				.subscribe(user => (this.studentId = user.userId));
		}
	}

	public addOffer() {
		this.router.navigate(['offer', 'add']);
	}

	public getAvgRatingDesc(desc: string, rating?: number): string {
		return desc + ': ' + (rating ?? 0) + '/10';
	}

	public navigateToRent(id?: number) {
		this.router.navigate(['rents', 'details', id]);
	}
	public navigateToOffer(id?: number, user?: string, rating?: number) {
		if (!user || (user != 'owner' && !rating)) {
			return;
		}
		let path = user?.toLowerCase();
		if (path === 'student') {
			path = 'details';
		}
		this.router.navigate(['offer', path, id]);
	}
	public navigateToProperty(id?: number) {
		this.router.navigate(['real-estate', 'owner', id]);
	}
	public startDispute(rentId: number) {
		this.dialog.open(StartDisputeDialogComponent, {
			width: '500px',
			data: rentId,
		});
	}

	public onSelect(
		menuOption: IMenuOptions,
		rentId?: number,
		offerId?: number,
		propertyId?: number,
		user?: string
	) {
		switch (menuOption.option) {
			case 'rentDetails': {
				this.navigateToRent(rentId ?? 0);
				break;
			}
			case 'offerDetails': {
				this.navigateToOffer(offerId, user, 1);
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

	public onAddMeeting(offerId?: number): void {
		const ref = this.dialog.open(MeetingAddComponent, {
			disableClose: true,
			data: offerId ?? 0,
		});
		ref.componentInstance.owner$ = this.actualRent$.pipe(map(rent => rent.owner));
		ref.componentInstance.tenant$ = this.actualRent$.pipe(
			map(
				rent =>
					rent.tenants.find(tenant => tenant.userId === rent.mainTenantId) as ITenant
			)
		);
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

	public showProfile(id?: number) {
		this.router.navigate(['profile', 'details', id]);
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
		this.actualRent$ = this.rentsService.makePayment(paymentId).pipe(
			this.untilDestroyed(),
			switchMap(() => {
				this.snackBar.open(
					this.translate.instant('Rents-details.success-info'),
					this.translate.instant('Rents-details.close'),
					{
						duration: 10000,
					}
				);
				return this.rentId$?.pipe(
					switchMap(rentId => this.rentsService.getRentById(parseInt(rentId, 10)))
				);
			})
		);
	}
}
