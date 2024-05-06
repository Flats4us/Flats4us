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
import { Subject } from 'rxjs';
import { TranslateModule } from '@ngx-translate/core';

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
		TranslateModule
	],
})
export class RentsDialogComponent {
	private readonly unsubscribe$: Subject<void> = new Subject();
	public statusName: typeof statusName = statusName;
	constructor(
		public rentsService: RentsService,
		public dialogRef: MatDialogRef<RentsDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public data: IRent
	) {}

	public onYesClick() {
		this.data = { ...this.data, status: statusName.SUSPENDED };
		this.dialogRef.close(this.data);
	}
}
