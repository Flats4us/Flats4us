import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { RentsService } from '../../services/rents.service';

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
		public dialogRef: MatDialogRef<RentsDialogComponent>
	) {}
}
