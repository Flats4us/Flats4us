import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BaseComponent } from '@shared/components/base/base.component';
import { OfferService } from '../../services/offer.service';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import {
	MAT_DIALOG_DATA,
	MatDialogModule,
	MatDialogRef,
} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { IProperty } from 'src/app/real-estate/models/real-estate.models';
import { StarRatingComponent } from '@shared/components/star-rating/star-rating.component';
import { MatIconModule } from '@angular/material/icon';
import { TranslateModule } from '@ngx-translate/core';

@Component({
	selector: 'app-property-rating',
	standalone: true,
	imports: [
		CommonModule,
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		MatSnackBarModule,
		StarRatingComponent,
		MatIconModule,
		TranslateModule,
	],
	providers: [OfferService],
	templateUrl: './property-rating.component.html',
	styleUrls: ['./property-rating.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PropertyRatingComponent extends BaseComponent {
	constructor(
		public offerService: OfferService,
		private snackBar: MatSnackBar,
		public dialogRef: MatDialogRef<number>,
		private router: Router,
		@Inject(MAT_DIALOG_DATA) public data: IProperty
	) {
		super();
	}

	public onClose() {
		this.dialogRef.close();
	}
}
