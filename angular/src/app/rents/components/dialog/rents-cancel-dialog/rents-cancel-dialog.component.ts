import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RentsService } from '../../../services/rents.service';
import { statusName } from '../../../statusName';
import { TranslateModule } from '@ngx-translate/core';

@Component({
	selector: 'app-rents-cancel-dialog',
	templateUrl: './rents-cancel-dialog.component.html',
	styleUrls: ['./rents-cancel-dialog.component.scss'],
	standalone: true,
	changeDetection: ChangeDetectionStrategy.OnPush,
	imports: [
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
		TranslateModule,
	],
	providers: [RentsService],
})
export class RentsCancelDialogComponent {
	public statusName: typeof statusName = statusName;
	constructor(
		public rentsService: RentsService,
		@Inject(MAT_DIALOG_DATA) public data: number
	) {}

	public onYesClick() {
		return;
	}
}
