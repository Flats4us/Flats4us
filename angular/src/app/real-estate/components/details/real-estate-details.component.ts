import {
	ChangeDetectionStrategy,
	Component,
	OnDestroy,
	OnInit,
} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subject, map, switchMap } from 'rxjs';
import { environment } from 'src/environments/environment.prod';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { slideAnimation } from 'src/app/rents/slide.animation';
import { IMenuOptions } from 'src/app/rents/models/rents.models';
import { RealEstateDialogComponent } from '../dialog/real-estate-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { IProperty } from '../../models/real-estate.models';

@Component({
	selector: 'app-real-estate-details',
	templateUrl: './real-estate-details.component.html',
	styleUrls: ['./real-estate-details.component.scss'],
	animations: [slideAnimation],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RealEstateDetailsComponent implements OnInit, OnDestroy {
	protected baseUrl = environment.apiUrl.replace('/api', '');

	public actualRealEstate$?: Observable<IProperty>;
	private realEstateId$?: Observable<string>;
	private readonly unsubscribe$: Subject<void> = new Subject();

	public currentIndex = 0;

	public menuOptions: IMenuOptions[] = [
		{ option: 'deleteRealEstate', description: 'Usuń nieruchomość' },
	];

	constructor(
		public realEstateService: RealEstateService,
		private router: Router,
		private route: ActivatedRoute,
		private dialog: MatDialog
	) {}
	public ngOnInit(): void {
		this.realEstateId$ = this.route.paramMap.pipe(
			map(params => params.get('id') ?? '')
		);
		this.actualRealEstate$ = this.realEstateId$?.pipe(
			switchMap(id =>
				this.realEstateService
					.getRealEstates(false)
					.pipe(
						map(
							realEstates =>
								realEstates.find(
									realEstate => realEstate.propertyId === parseInt(id)
								) ?? ({} as IProperty)
						)
					)
			)
		);
	}

	public onSelect(menuOption: IMenuOptions, id: number) {
		switch (menuOption.option) {
			case 'deleteRealEstate': {
				this.openDialog(id);
				break;
			}
		}
	}

	public openDialog(id: number): void {
		this.dialog.open(RealEstateDialogComponent, { disableClose: true, data: id });
	}

	public addRent() {
		this.router.navigate([`offer/add`]);
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
