import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RealEstateService } from '../../services/real-estate.service';
import { Router } from '@angular/router';
import { BaseComponent } from '@shared/components/base/base.component';
import { catchError, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

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
		MatSnackBarModule
	],
	providers: [RealEstateService],
})
export class RealEstateDialogComponent extends BaseComponent {
	constructor(
		public realEstateService: RealEstateService,
		public snackBar: MatSnackBar,
		@Inject(MAT_DIALOG_DATA) public data: number,
		private router: Router
	) {
		super();
	}

	public onYesClick() {
		this.realEstateService
			.deleteRealEstate(this.data)
			.pipe(this.untilDestroyed(), catchError(this.handleError))
			.subscribe({
				next: () => {
					this.snackBar.open(
						'Nieruchomość została pomyślnie usunięta.',
						'Zamknij',
						{ duration: 2000 }
					);
					this.router.navigate(['real-estate', 'owner']);
				},
				error: () => {
					this.snackBar.open(
						'Nie udało się usunąć nieruchomości.',
						'Zamknij',
						{ duration: 2000 }
					);
				},
			});
	}
	private handleError(error: HttpErrorResponse) {
		return throwError(
			() => new Error('Nie udało się usunąć nieruchomości')
		);
	}
}