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

@Component({
	selector: 'app-offer-details',
	templateUrl: './offer-details.component.html',
	styleUrls: ['./offer-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferDetailsComponent {
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
		private router: Router,
		private dialog: MatDialog,
		private route: ActivatedRoute,
		public authService: AuthService
	) {}

	public addOffer() {
		this.router.navigate(['offer', 'add']);
	}

	public returnStart() {
		this.router.navigate(['start']);
	}

	public openCancelDialog(id: number): void {
		this.dialog.open(OfferCancelDialogComponent, {
			disableClose: true,
			data: id,
		});
	}

	public openPromotionDialog(id: number): void {
		this.dialog.open(OfferPromotionDialogComponent, {
			disableClose: true,
			data: id,
		});
	}

	public navigateToOffer(id: number) {
		this.router.navigate(['offer', 'details', id]);
	}

	public navigateToProperty(id: number) {
		this.router.navigate(['real-estate', 'owner', id]);
	}

	public onSelect(menuOption: IMenuOptions, offerId?: number, propertyId?: number) {
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
		this.dialog.open(RentPropositionDialogComponent, {
			disableClose: true,
			data: id ?? 0,
		});
	}

	public onRentApproval(id?: number): void {
		this.dialog.open(RentApprovalDialogComponent, {
			disableClose: true,
			data: id ?? 0,
		});
	}

	public showRent(id?: number): void{
		if(id){
			this.router.navigate(['rents', 'owner', id]);
		}
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
