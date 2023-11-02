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
	constructor(
		public rentsService: RentsService,
		public dialogRef: MatDialogRef<RentsDialogComponent>,
		@Inject(MAT_DIALOG_DATA) public actualRent: IRent
	) {}

	public onNoClick(): void {
		this.dialogRef.close();
	}

	public onYesClick(): void {
		this.actualRent.status = 'suspended';
		this.dialogRef.close();
	}
}
