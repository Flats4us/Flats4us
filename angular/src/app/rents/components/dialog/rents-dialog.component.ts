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
import { RentsService } from '../../services/rents.service';
import { IRent } from '../../models/rents.models';
import { statusName } from '../../statusName';
import { BehaviorSubject, take } from 'rxjs';

@Component({
	selector: 'app-rents-dialog',
	templateUrl: 'rents-dialog.component.html',
	styleUrls: ['./rents-dialog.component.scss'],
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
export class RentsDialogComponent {
	public statusName: typeof statusName = statusName;
	constructor(
		public rentsService: RentsService,
		public dialogRef: MatDialogRef<RentsDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: BehaviorSubject<IRent>
	) {}

	public onYesClick() {
		this.data
			.pipe(take(1))
			.subscribe(value =>
				this.data.next({ ...value, status: statusName.SUSPENDED })
			);
		this.dialogRef.close(this.data);
	}
}
