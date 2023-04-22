import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
	public showMenu = false;
	public isUserLoggedIn = true;

	public toggleMenu() {
		this.showMenu = !this.showMenu;
	}
}