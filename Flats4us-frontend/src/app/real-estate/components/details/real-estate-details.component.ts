import { ChangeDetectionStrategy, Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable, map, switchMap, zip } from 'rxjs';
import { environment } from 'src/environments/environment';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { slideAnimation } from 'src/app/rents/slide.animation';
import { IMenuOptions } from 'src/app/rents/models/rents.models';
import { RealEstateDialogComponent } from '../dialog/real-estate-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { IProperty } from '../../models/real-estate.models';
import { BaseComponent } from '@shared/components/base/base.component';
import { OfferService } from 'src/app/offer/services/offer.service';
import { PropertyRatingComponent } from 'src/app/offer/components/property-rating/property-rating.component';
import { AuthService } from '@shared/services/auth.service';
import { AuthModels } from '@shared/models/auth.models';

@Component({
	selector: 'app-real-estate-details',
	templateUrl: './real-estate-details.component.html',
	styleUrls: ['./real-estate-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RealEstateDetailsComponent extends BaseComponent {
	protected baseUrl = environment.apiUrl.replace('/api', '');

	private realEstateId$: Observable<string> = this.route.paramMap.pipe(
		map(params => params.get('id') ?? '')
	);
	public actualRealEstate$: Observable<IProperty> = this.realEstateId$?.pipe(
		switchMap(id => this.realEstateService.getRealEstateById(parseInt(id)))
	);
	private showRealEstate: BehaviorSubject<boolean> =
		new BehaviorSubject<boolean>(false);
	public showRealEstate$ = this.showRealEstate.asObservable();

	public authModels = AuthModels;

	public currentIndex = 0;

	public menuOptions: IMenuOptions[] = [
		{
			option: 'realEstateDetails',
			description: 'Real-estate-details.option-details',
		},
		{
			option: 'editRealEstate',
			description: 'Real-estate-details.edit-property',
		},
		{
			option: 'deleteRealEstate',
			description: 'Real-estate-details.delete-property',
		},
	];

	constructor(
		public realEstateService: RealEstateService,
		public offerService: OfferService,
		public authService: AuthService,
		private router: Router,
		private route: ActivatedRoute,
		private dialog: MatDialog
	) {
		super();
		if (this.authService.getUserType() === AuthModels.MODERATOR) {
			this.showRealEstate.next(true);
		} else {
			zip(this.realEstateId$, this.realEstateService.getRealEstates(false))
				.pipe(this.untilDestroyed())
				.subscribe(([id, properties]) => {
					const result = properties.find(
						property => property.propertyId === parseInt(id)
					);
					this.showRealEstate.next(!!result);
				});
		}
	}

	public onSelect(menuOption: IMenuOptions, id?: number) {
		switch (menuOption.option) {
			case 'realEstateDetails': {
				this.realEstateDetails(id ?? 0);
				break;
			}
			case 'deleteRealEstate': {
				this.openDialog(id ?? 0);
				break;
			}
			case 'editRealEstate': {
				this.editRealEstate(id ?? 0);
				break;
			}
		}
	}

	public openDialog(id: number): void {
		const deleteDialog = this.dialog.open(RealEstateDialogComponent, {
			disableClose: true,
			data: id,
		});
		deleteDialog
			.afterClosed()
			.pipe(this.untilDestroyed())
			.subscribe(
				() =>
					(this.actualRealEstate$ = this.realEstateId$.pipe(
						switchMap(value =>
							this.realEstateService.getRealEstateById(parseInt(value))
						)
					))
			);
	}

	public addOffer() {
		this.router.navigate(['offer', 'add']);
	}

	public showOffer(id: number) {
		this.router.navigate(['offer', 'details', id]);
	}

	public realEstateDetails(id: number) {
		this.router.navigate(['real-estate', 'details', id]);
	}

	public editRealEstate(id: number) {
		this.router.navigate(['real-estate', 'edit', id]);
	}

	public showRating(property?: IProperty | null) {
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
