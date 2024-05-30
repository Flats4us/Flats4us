import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable, map, switchMap, of } from 'rxjs';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { MeetingAddComponent } from 'src/app/rents/components/meeting-add/meeting-add.component';
import { IMenuOptions } from 'src/app/rents/models/rents.models';
import { statusName } from 'src/app/rents/statusName';
import { environment } from 'src/environments/environment.prod';
import { IOffer } from '../../models/offer.models';
import { slideAnimation } from 'src/app/rents/slide.animation';
import { UserType } from 'src/app/profile/models/types';
import { OfferPromotionDialogComponent } from '../dialog/offer-promotion-dialog/offer-promotion-dialog.component';
import { OfferService } from '../../services/offer.service';
import { RentPropositionDialogComponent } from '../dialog/rent-proposition-dialog/rent-proposition-dialog.component';
import { RentApprovalDialogComponent } from '../dialog/rent-approval-dialog/rent-approval-dialog.component';
import { OfferCancelDialogComponent } from '../dialog/offer-cancel-dialog/offer-cancel-dialog.component';
import { AuthService } from '@shared/services/auth.service';
import { LoggedUserType } from '@shared/models/auth.models';
import { RentsService } from 'src/app/rents/services/rents.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { StartService } from 'src/app/start/services/start.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IProperty } from 'src/app/real-estate/models/real-estate.models';
import { PropertyRatingComponent } from '../property-rating/property-rating.component';

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

	public showOffer$: Observable<boolean> = of(false);

	public uType = UserType;

	public loggedInUserType = LoggedUserType;

	public currentIndex = 0;

	public menuOptions: IMenuOptions[] = [
		{ option: 'offerDetails', description: 'Szczegóły oferty' },
		{ option: 'promoteOffer', description: 'Promuj ofertę' },
		{ option: 'property', description: 'Powiązana nieruchomość' },
		{ option: 'closeOffer', description: 'Zakończ ofertę' },
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
		private snackBar: MatSnackBar
	) {
		super();
		this.user$.pipe(this.untilDestroyed()).subscribe(user => {
			if (user === UserType.DETAILS) {
				return;
			}
			this.showOffer$ = this.offerId$?.pipe(
				switchMap(value =>
					this.offerService
						.getOffers()
						.pipe(
							map(offers =>
								offers.result.find(offer => offer.offerId === parseInt(value))
									? true
									: false
							)
						)
				)
			);
		});
	}

	public addOffer() {
		this.router.navigate(['/offer', 'add']);
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
		this.router.navigate(['/offer', 'details', id]);
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
		this.dialog.open(MeetingAddComponent, {
			disableClose: true,
			data: id ?? 0,
		});
	}

	public startRent(id?: number) {
		const rentPropositionDialog = this.dialog.open(
			RentPropositionDialogComponent,
			{
				disableClose: true,
				data: id ?? 0,
			}
		);
		this.actualOffer$ = rentPropositionDialog
			.afterClosed()
			.pipe(switchMap(value => this.offerService.getOfferById(value)));
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
					this.snackBar.open('Oferta została dodana do obserwowanych!', 'Zamknij', {
						duration: 2000,
					});
				},
				error: () => {
					this.snackBar.open(
						'Nie udało się dodać oferty do obserowowanych. Spróbuj ponownie.',
						'Zamknij',
						{ duration: 2000 }
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
