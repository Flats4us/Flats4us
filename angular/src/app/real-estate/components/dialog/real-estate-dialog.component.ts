import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
	MAT_DIALOG_DATA,
	MatDialogModule,
	MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RealEstateService } from '../../services/real-estate.service';
import { Router } from '@angular/router';
import { BaseComponent } from '@shared/components/base/base.component';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { TranslateModule, TranslateService } from '@ngx-translate/core';

@Component({
	selector: 'app-real-estate-dialog',
	templateUrl: 'real-estate-dialog.component.html',
	styleUrls: ['./real-estate-dialog.component.scss'],
	standalone: true,
	changeDetection: ChangeDetectionStrategy.OnPush,
	imports: [
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		MatSnackBarModule,
		TranslateModule,
	],
	providers: [RealEstateService],
})
export class RealEstateDialogComponent extends BaseComponent {
	constructor(
		public realEstateService: RealEstateService,
		public snackBar: MatSnackBar,
		private router: Router,
		public dialogRef: MatDialogRef<number>,
		private translate: TranslateService,
		@Inject(MAT_DIALOG_DATA) public data: number
	) {
		super();
	}

	public onClose() {
		this.dialogRef.close();
	}

	public onYesClick() {
		this.realEstateService
			.deleteRealEstate(this.data)
			.pipe(this.untilDestroyed())
			.subscribe({
				next: () => {
					this.snackBar.open(
						this.translate.instant('Real-estate-dialog.info1'),
						this.translate.instant('close'),
						{
							duration: 10000,
						}
					);
					this.router.navigate(['/real-estate', 'owner']);
					this.dialogRef.close(this.data);
				},
				error: () => {
					this.snackBar.open(
						this.translate.instant('Real-estate-dialog.info2'),
						this.translate.instant('close'),
						{
							duration: 10000,
						}
					);
					this.dialogRef.close(this.data);
				},
			});
	}
}
