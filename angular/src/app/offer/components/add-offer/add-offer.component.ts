import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { OfferService } from '../../services/offer.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RealEstateService } from 'src/app/real-estate/services/real-estate.service';
import { setLocalDate } from '@shared/utils/functions';
import { Router } from '@angular/router';

@Component({
	selector: 'app-add-offer',
	templateUrl: './add-offer.component.html',
	styleUrls: ['./add-offer.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddOfferComponent extends BaseComponent {
	public setLocalDate = setLocalDate;

	public offerForm: FormGroup = new FormGroup({
		propertyId: new FormControl('', Validators.required),
		price: new FormControl('', Validators.required),
		deposit: new FormControl('', Validators.required),
		description: new FormControl('', Validators.required),
		startDate: new FormControl(null, Validators.required),
		endDate: new FormControl(null, Validators.required),
		regulations: new FormControl('', Validators.required),
	});

	public properties$ = this.realEstateService.getRealEstates(true);

	constructor(
		private offerService: OfferService,
		private realEstateService: RealEstateService,
		private snackBar: MatSnackBar,
		private router: Router
	) {
		super();
	}

	public onSubmit() {
		this.offerService
			.addOffer(this.offerForm.value)
			.pipe(this.untilDestroyed())
			.subscribe({
				next: () => {
					this.snackBar.open('Oferta dodana pomyślnie', 'Zamknij', {
						duration: 10000,
					});
					this.router.navigate(['offer', 'owner']);
				},
				error: () => {
					this.snackBar.open(
						'Nie udało się dodać oferty. Spróbuj ponownie.',
						'Zamknij',
						{ duration: 10000 }
					);
				},
			});
	}
}
