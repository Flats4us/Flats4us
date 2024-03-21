import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable, startWith, map, switchMap, of } from 'rxjs';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { MeetingAddComponent } from 'src/app/rents/components/meeting-add/meeting-add.component';
import { IPayment, IMenuOptions } from 'src/app/rents/models/rents.models';
import { statusName } from 'src/app/rents/statusName';
import { environment } from 'src/environments/environment.prod';
import { IOffer } from '../../models/offer.models';
import { slideAnimation } from 'src/app/rents/slide.animation';
import { UserType } from 'src/app/profile/models/types';
import { RentsCancelDialogComponent } from 'src/app/rents/components/dialog/rents-cancel-dialog/rents-cancel-dialog.component';
import { OfferPromotionDialogComponent } from '../dialog/offer-promotion-dialog/offer-promotion-dialog.component';
import { OfferService } from '../../services/offer.service';
import { RentPropositionDialogComponent } from '../dialog/rent-proposition-dialog/rent-proposition-dialog.component';
import { RentApprovalDialogComponent } from '../dialog/rent-approval-dialog/rent-approval-dialog.component';

@Component({
	selector: 'app-offer-details',
	templateUrl: './offer-details.component.html',
	styleUrls: ['./offer-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferDetailsComponent {
	protected baseUrl = environment.apiUrl.replace('/api', '');

	public separatorKeysCodes: number[] = [ENTER, COMMA];
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
	public tenantsCtrl = new FormControl('');
	public filteredTenants$: Observable<string[]>;
	public myTenants: string[] = [];
	public payments: IPayment[] = [
		{ sum: 1000, date: '20.12.2020', kind: 'CZYNSZ' },
	];
	public uType = UserType;

	private tenants: string[] = ['jk@wp.pl', 'sk@wp.pl', 'kl@onet.pl'];

	public currentIndex = 0;

	public displayedColumnsStudent: string[] = ['sum', 'date', 'kind'];
	public displayedColumnsOwner: string[] = ['sum', 'date', 'kind', 'who'];
	public menuOptions: IMenuOptions[] = [
		{ option: 'offerDetails', description: 'Szczegóły oferty' },
		{ option: 'startDispute', description: 'Rozpocznij spór' },
		{ option: 'promoteOffer', description: 'Promuj ofertę' },
		{ option: 'closeOffer', description: 'Zakończ ofertę' },
	];

	constructor(
		public realEstateService: RealEstateService,
		public offerService: OfferService,
		private router: Router,
		private dialog: MatDialog,
		private route: ActivatedRoute
	) {
		this.filteredTenants$ = this.tenantsCtrl.valueChanges.pipe(
			startWith(null),
			map((tenant: string | null) =>
				tenant ? this.filter(tenant) : this.tenants.slice()
			)
		);
	}

	public add(
		event: MatChipInputEvent,
		items: string[],
		formControl: FormControl
	): void {
		const value = (event.value || '').trim();
		if (value && !items.includes(value.trim())) {
			items.push(value);
		}
		event.chipInput.clear();

		formControl.setValue(null);
	}

	public remove(item: string, items: string[]): void {
		const index = items.indexOf(item);

		if (index >= 0) {
			items.splice(index, 1);
		}
	}

	public selected(event: MatAutocompleteSelectedEvent): void {
		if (!this.myTenants.includes(event.option.viewValue)) {
			this.myTenants.push(event.option.viewValue);
		}
		this.tenantsCtrl.setValue(null);
	}

	private filter(value: string): string[] {
		const filterValue = value.toLowerCase().trim();

		return this.tenants.filter(tenant =>
			tenant.toLowerCase().includes(filterValue)
		);
	}

	public addOffer() {
		this.router.navigate(['offer', 'add']);
	}

	public openCancelDialog(id: number): void {
		this.dialog.open(RentsCancelDialogComponent, {
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
	public startDispute(id: number) {
		this.router.navigate(['disputes', id]);
	}

	public onSelect(menuOption: IMenuOptions, id: number) {
		switch (menuOption.option) {
			case 'offerDetails': {
				this.navigateToOffer(id);
				break;
			}
			case 'startDispute': {
				this.startDispute(id);
				break;
			}
			case 'closeRent': {
				this.openCancelDialog(id);
				break;
			}
			case 'promoteOffer': {
				this.openPromotionDialog(id);
				break;
			}
		}
	}

	public onAddMeeting(id: number): void {
		this.dialog.open(MeetingAddComponent, {
			disableClose: true,
			data: id,
		});
	}

	public startRent(id: number) {
		this.dialog.open(RentPropositionDialogComponent, {
			disableClose: true,
			data: id,
		});
	}

	public onRentApproval(id: number): void {
		this.dialog.open(RentApprovalDialogComponent, {
			disableClose: true,
			data: id,
		});
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
