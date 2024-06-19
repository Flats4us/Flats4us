import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable, map, switchMap, of, zip, BehaviorSubject } from 'rxjs';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { MeetingAddComponent } from 'src/app/rents/components/meeting-add/meeting-add.component';
import { IMenuOptions } from 'src/app/rents/models/rents.models';
import { statusName } from 'src/app/rents/statusName';
import { environment } from 'src/environments/environment';
import { IOffer } from '../../models/offer.models';
import { slideAnimation } from 'src/app/rents/slide.animation';
import { UserType } from 'src/app/profile/models/types';
import { OfferPromotionDialogComponent } from '../dialog/offer-promotion-dialog/offer-promotion-dialog.component';
import { OfferService } from '../../services/offer.service';
import { RentPropositionDialogComponent } from '../dialog/rent-proposition-dialog/rent-proposition-dialog.component';
import { RentApprovalDialogComponent } from '../dialog/rent-approval-dialog/rent-approval-dialog.component';
import { OfferCancelDialogComponent } from '../dialog/offer-cancel-dialog/offer-cancel-dialog.component';
import { AuthService } from '@shared/services/auth.service';
import { RentsService } from 'src/app/rents/services/rents.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { StartService } from 'src/app/start/services/start.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IProperty } from 'src/app/real-estate/models/real-estate.models';
import { PropertyRatingComponent } from '../property-rating/property-rating.component';
import { TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-offer-details',
	templateUrl: './offer-details.component.html',
	styleUrls: ['./offer-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferDetailsComponent extends BaseComponent {
	protected baseUrl = environment.apiUrl.replace('/api', '');

	public statusName: typeof statusName = statusName;
	private user: BehaviorSubject<UserType> = new BehaviorSubject<UserType>(
		UserType.DETAILS
	);
	public user$: Observable<string | undefined> =
		this.route.parent?.paramMap.pipe(
			map(params => params.get('user')?.toUpperCase())
		) ?? of('');

	private offerId$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('id') ?? '')
	);

	public actualOffer$: Observable<IOffer> = this.offerId$.pipe(
		switchMap(value => this.offerService.getOfferById(parseInt(value)))
	);

	private showOffer: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
		false
	);

	public showOffer$: Observable<boolean> = this.showOffer.asObservable();

	public uType = UserType;

	public currentIndex = 0;

	public menuOptions: IMenuOptions[] = [
		{ option: 'offerDetails', description: 'Offer.offer-details' },
		{ option: 'promoteOffer', description: 'Offer.promote-offer' },
		{ option: 'property', description: 'Offer.related-property' },
		{ option: 'closeOffer', description: 'Offer.close-offer' },
	];

	constructor(
		public realEstateService: RealEstateService,
		public offerService: OfferService,
		public rentsService: RentsService,
		public startService: StartService,
		private router: Router,
		private dialog: MatDialog,
		private route: ActivatedRoute,
		public authService: AuthService,
		private snackBar: MatSnackBar,
		private translate: TranslateService
	) {
		super();

		this.user$
			.pipe(this.untilDestroyed())
			.subscribe(user => this.user.next(user as UserType));

		if (this.user.value !== UserType.DETAILS) {
			zip(this.offerId$, this.offerService.getOffers())
				.pipe(this.untilDestroyed())
				.subscribe(([id, offers]) => {
					const result = offers.result.find(offer => offer.offerId === parseInt(id));
					this.showOffer.next(!!result);
				});
		}
	}

	public addOffer() {
		this.router.navigate(['offer', 'add']);
	}

	public returnStart() {
		this.router.navigate(['start']);
	}

	public openCancelDialog(id: number): void {
		const cancelDialog = this.dialog.open(OfferCancelDialogComponent, {
			disableClose: true,
			data: id,
		});
		this.actualOffer$ = cancelDialog
			.afterClosed()
			.pipe(switchMap(value => this.offerService.getOfferById(value)));
		cancelDialog
			.afterClosed()
			.pipe(this.untilDestroyed())
			.subscribe(
				() =>
					(this.actualOffer$ = this.offerId$.pipe(
						switchMap(value => this.offerService.getOfferById(parseInt(value)))
					))
			);
	}

	public openPromotionDialog(id: number): void {
		const promotionDialog = this.dialog.open(OfferPromotionDialogComponent, {
			disableClose: true,
			data: id,
		});
		this.actualOffer$ = promotionDialog
			.afterClosed()
			.pipe(switchMap(value => this.offerService.getOfferById(value)));
		promotionDialog
			.afterClosed()
			.pipe(this.untilDestroyed())
			.subscribe(
				() =>
					(this.actualOffer$ = this.offerId$.pipe(
						switchMap(value => this.offerService.getOfferById(parseInt(value)))
					))
			);
	}

	public navigateToOffer(id: number) {
		this.router.navigate(['offer', 'details', id]);
	}

	public navigateToProperty(id: number) {
		this.router.navigate(['real-estate', 'owner', id]);
	}

	public onSelect(
		menuOption: IMenuOptions,
		offerId?: number,
		propertyId?: number
	) {
		switch (menuOption.option) {
			case 'offerDetails': {
				this.navigateToOffer(offerId ?? 0);
				break;
			}
			case 'closeOffer': {
				this.openCancelDialog(offerId ?? 0);
				break;
			}
			case 'promoteOffer': {
				this.openPromotionDialog(offerId ?? 0);
				break;
			}
			case 'property': {
				this.navigateToProperty(propertyId ?? 0);
				break;
			}
		}
	}

	public onAddMeeting(id?: number): void {
		const ref = this.dialog.open(MeetingAddComponent, {
			disableClose: true,
			data: id ?? 0,
		});
		ref.componentInstance.owner$ = this.actualOffer$.pipe(
			map(offer => offer.owner)
		);
	}

	public startRent(id?: number, maxNumberOfInhabitants?: number) {
		const rentPropositionDialog = this.dialog.open(
			RentPropositionDialogComponent,
			{
				disableClose: true,
				data: { id: id ?? 0, maxNumberOfInhabitants: maxNumberOfInhabitants ?? 0 },
			}
		);
		this.actualOffer$ = rentPropositionDialog
			.afterClosed()
			.pipe(switchMap(value => this.offerService.getOfferById(value.id)));
		rentPropositionDialog
			.afterClosed()
			.pipe(this.untilDestroyed())
			.subscribe(
				() =>
					(this.actualOffer$ = this.offerId$.pipe(
						switchMap(value => this.offerService.getOfferById(parseInt(value)))
					))
			);
	}

	public onRentApproval(rentId?: number, offerId?: number): void {
		const rentApprovalDialog = this.dialog.open(RentApprovalDialogComponent, {
			disableClose: true,
			data: { rentId: rentId, offerId: offerId } ?? { rentId: 0, offerId: 0 },
		});
		this.actualOffer$ = rentApprovalDialog
			.afterClosed()
			.pipe(switchMap(value => this.offerService.getOfferById(value)));
		rentApprovalDialog
			.afterClosed()
			.pipe(this.untilDestroyed())
			.subscribe(
				() =>
					(this.actualOffer$ = this.offerId$.pipe(
						switchMap(value => this.offerService.getOfferById(parseInt(value)))
					))
			);
	}

	public addToWatched(id?: number) {
		if (!id) {
			return;
		}
		this.startService
			.addToWatched(id)
			.pipe(this.untilDestroyed())
			.subscribe({
				next: () => {
					this.actualOffer$ = this.offerId$.pipe(
						switchMap(value => this.offerService.getOfferById(parseInt(value)))
					);
					this.snackBar.open(
						this.translate.instant('Offer.offer-info3'),
						this.translate.instant('close'),
						{
							duration: 10000,
						}
					);
				},
				error: () => {
					this.snackBar.open(
						this.translate.instant('Offer.offer-info4'),
						this.translate.instant('close'),
						{ duration: 10000 }
					);
				},
			});
	}

	public showRent(id?: number): void {
		if (!id) {
			return;
		}
		this.router.navigate(['rents', 'owner', id]);
	}

	public showRating(property?: IProperty) {
		if (!property || !property.avgRating) {
			return;
		}
		this.dialog.open(PropertyRatingComponent, {
			disableClose: false,
			closeOnNavigation: true,
			data: property,
		});
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
}
