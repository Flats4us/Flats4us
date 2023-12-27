import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { OfferService } from '../../services/offer.service';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
	selector: 'app-add-offer',
	templateUrl: './add-offer.component.html',
	styleUrls: ['./add-offer.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AddOfferComponent extends BaseComponent {
	public offerForm: FormGroup = this.fb.group({
		propertyId: [''],
		price: [''],
		deposit: [''],
		description: [''],
		startDate: [''],
		endDate: [''],
		regulations: [''],
	});

	public properties$ = this.service.properties$;

	constructor(
		private service: OfferService,
		private fb: FormBuilder,
		private snackBar: MatSnackBar
	) {
		super();
	}

	public onSubmit() {
		this.service
			.addOffer(this.offerForm)
			.pipe(this.untilDestroyed())
			.subscribe(() =>
				this.snackBar.open('Oferta dodana pomyślnie', 'Zamknij', {
					duration: 2000,
				})
			);
	}
}
