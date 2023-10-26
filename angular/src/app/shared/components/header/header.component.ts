// eslint-disable-next-line max-classes-per-file
import { ChangeDetectionStrategy, Component } from '@angular/core';
import {
	MatDialog,
	MatDialogModule,
	MatDialogRef,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
	constructor(public dialog: MatDialog) {}

	public isUserLoggedIn = true;
	public isUserLoggedInAsStudent = true;

	public showSidenav = false;

	public animal = 'Dog';
	public name = 'Kuba';

	public openDialog(): void {
		const dialogRef = this.dialog.open(StartDisputeComponent, {
			width: '600px',
			data: { name: this.name, animal: this.animal },
		});
	}
}

@Component({
	selector: 'app-start-dispute-dialog',
	templateUrl: 'start-dispute-dialog.html',
	standalone: true,
	imports: [
		MatDialogModule,
		MatFormFieldModule,
		MatInputModule,
		FormsModule,
		MatButtonModule,
	],
})
export class StartDisputeComponent {
	constructor(public dialogRef: MatDialogRef<StartDisputeComponent>) {}
}
