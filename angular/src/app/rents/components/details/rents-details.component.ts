import { ChangeDetectionStrategy, Component } from '@angular/core';
import { IMenuOptions, IPayment } from '../../models/rents.models';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Observable, map, switchMap } from 'rxjs';
import { slideAnimation } from '../../slide.animation';
import { statusName } from '../../statusName';
import { COMMA, ENTER } from '@angular/cdk/keycodes';
import { environment } from 'src/environments/environment.prod';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { MeetingAddComponent } from '../meeting-add/meeting-add.component';
import { IOffer } from 'src/app/offer/models/offer.models';
import { UserType } from 'src/app/profile/models/types';
import { RentsCancelDialogComponent } from '../dialog/rents-cancel-dialog/rents-cancel-dialog.component';
import { OfferService } from 'src/app/offer/services/offer.service';
import { RentsService } from '../../services/rents.service';

@Component({
	selector: 'app-rents-details',
	templateUrl: './rents-details.component.html',
	styleUrls: ['./rents-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentsDetailsComponent {
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
		switchMap(value => this.offerService.getOfferById(parseInt(value)))
	);
	public payments: IPayment[] = [
		{ sum: 1000, date: '20.12.2020', kind: 'CZYNSZ' },
	];

	public currentIndex = 0;

	public displayedColumnsStudent: string[] = ['sum', 'date', 'kind'];
	public displayedColumnsOwner: string[] = ['sum', 'date', 'kind', 'who'];
	public menuOptions: IMenuOptions[] = [
		{ option: 'rentDetails', description: 'Szczegóły najmu' },
		{ option: 'startDispute', description: 'Rozpocznij spór' },
		{ option: 'closeRent', description: 'Zakończ najem' },
	];

	constructor(
		public realEstateService: RealEstateService,
		public offerService: OfferService,
		private rentsService: RentsService,
		private router: Router,
		private dialog: MatDialog,
		private route: ActivatedRoute
	) {}

	public addOffer() {
		this.router.navigate(['offer', 'add']);
	}

	public openCancelDialog(id: number): void {
		this.dialog.open(RentsCancelDialogComponent, {
			disableClose: true,
			data: id,
		});
	}

	public navigateToRent(id: number) {
		this.router.navigate(['rents', 'details', id]);
	}
	public startDispute(id: number) {
		this.router.navigate([['disputes', id]]);
	}

	public onSelect(menuOption: IMenuOptions, id: number | undefined) {
		switch (menuOption.option) {
			case 'rentDetails': {
				this.navigateToRent(id ?? 0);
				break;
			}
			case 'startDispute': {
				this.startDispute(id ?? 0);
				break;
			}
			case 'closeRent': {
				this.openCancelDialog(id ?? 0);
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

	public setCurrentSlideIndex(index: number) {
		this.currentIndex = index;
	}

	public isCurrentSlideIndex(index: number) {
		return this.currentIndex === index;
	}

	public prevSlide(length: number | undefined) {
		this.currentIndex =
			this.currentIndex < (length ?? 0) - 1 ? ++this.currentIndex : 0;
	}

	public nextSlide(length: number | undefined) {
		this.currentIndex =
			this.currentIndex > 0 ? --this.currentIndex : (length ?? 0) - 1;
	}
}
