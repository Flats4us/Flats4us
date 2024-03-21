import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';

@Component({
	selector: 'app-rent-approval-dialog',
	standalone: true,
	imports: [CommonModule, MatDialogModule, MatButtonModule],
	templateUrl: './rent-approval-dialog.component.html',
	styleUrls: ['./rent-approval-dialog.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class RentApprovalDialogComponent {
	constructor(public dialogRef: MatDialogRef<number>) {}

	public onYesClick() {
		this.dialogRef.close();
	}

	public onClose() {
		this.dialogRef.close();
	}
}
