import {
	ChangeDetectionStrategy,
	Component,
	ElementRef,
	OnDestroy,
	OnInit,
	ViewChild,
	inject,
} from '@angular/core';
import { LiveAnnouncer } from '@angular/cdk/a11y';
import { ENTER, COMMA } from '@angular/cdk/keycodes';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatChipInputEvent } from '@angular/material/chips';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { Observable, Subject, startWith, map, switchMap } from 'rxjs';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { RentsTenantsDialogComponent } from 'src/app/rents/components/dialog/rents-tenants-dialog.component';
import { MeetingAddComponent } from 'src/app/rents/components/meeting-add/meeting-add.component';
import { IPayment, IMenuOptions } from 'src/app/rents/models/rents.models';
import { RentsService } from 'src/app/rents/services/rents.service';
import { statusName } from 'src/app/rents/statusName';
import { environment } from 'src/environments/environment.prod';
import { IOffer } from '../../models/offer.models';
import { slideAnimation } from 'src/app/rents/slide.animation';
import { userType } from 'src/app/profile/models/types';
import { RentsCancelDialogComponent } from 'src/app/rents/components/dialog/rents-cancel-dialog.component';

@Component({
	selector: 'app-offer-details',
	standalone: false,
	templateUrl: './offer-details.component.html',
	styleUrls: ['./offer-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OfferDetailsComponent implements OnInit, OnDestroy {
	protected baseUrl = environment.apiUrl.replace('/api', '');

	@ViewChild('tenantsInput')
	public tenantsInput!: ElementRef<HTMLInputElement>;

	public separatorKeysCodes: number[] = [ENTER, COMMA];
	public statusName: typeof statusName = statusName;
	public actualRent$?: Observable<IOffer>;
	public user$?: Observable<string>;
	public user = '';
	private rentId$?: Observable<string>;
	private readonly unsubscribe$: Subject<void> = new Subject();
	public tenantsCtrl = new FormControl('');
	public filteredTenants$?: Observable<string[]>;
	public myTenants: string[] = [];
	private announcer = inject(LiveAnnouncer);
	public payments: IPayment[] = [
		{ sum: 1000, date: '20.12.2020', kind: 'CZYNSZ' },
	];
	public uType = userType;

	private tenants: string[] = ['jk@wp.pl', 'sk@wp.pl', 'kl@onet.pl'];

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
	) {
		this.filteredTenants$ = this.tenantsCtrl.valueChanges.pipe(
			startWith(null),
			map((tenant: string | null) =>
				tenant ? this.filter(tenant) : this.tenants.slice()
			)
		);
	}
	public ngOnInit(): void {
		this.user$ = this.route.parent?.paramMap.pipe(
			map(params => params.get('user')?.toUpperCase() ?? '')
		);
		this.rentId$ = this.route.paramMap.pipe(
			map(params => params.get('id') ?? '')
		);
		this.actualRent$ = this.rentId$?.pipe(
			switchMap(value => this.rentsService.getOfferById(parseInt(value)))
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

			this.announcer.announce(`Removed ${item}`);
		}
	}

	public selected(event: MatAutocompleteSelectedEvent): void {
		if (!this.myTenants.includes(event.option.viewValue)) {
			this.myTenants.push(event.option.viewValue);
		}
		this.tenantsInput.nativeElement.value = '';
		this.tenantsCtrl.setValue(null);
	}

	private filter(value: string): string[] {
		const filterValue = value.toLowerCase();

		return this.tenants.filter(tenant =>
			tenant.toLowerCase().includes(filterValue)
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
