import {
	ChangeDetectionStrategy,
	Component,
	Inject,
	OnDestroy,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { Subject, takeUntil } from 'rxjs';
import { RealEstateService } from '../../services/real-estate.service';
import { Router } from '@angular/router';

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
	],
})
export class RealEstateDialogComponent implements OnDestroy {
	private readonly unsubscribe$: Subject<void> = new Subject();
	constructor(
		public realEstateService: RealEstateService,
		@Inject(MAT_DIALOG_DATA) public data: number,
		private router: Router
	) {}

	public onYesClick() {
		this.realEstateService
			.deleteRealEstate(this.data)
			.pipe(takeUntil(this.unsubscribe$))
			.subscribe(() => {
				this.router.navigate(['real-estate/owner']);
				parent.location.reload();
			});
	}
	public ngOnDestroy() {
		this.unsubscribe$.next();
		this.unsubscribe$.complete();
	}
}
