import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { StartDisputeDialogComponent } from './start-dispute-dialog.component';

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

	public openDialog(): void {
		this.dialog.open(StartDisputeDialogComponent, {
			width: '600px',
		});
	}
}
