import { Component } from '@angular/core';
import {
	trigger,
	state,
	style,
	transition,
	animate,
} from '@angular/animations';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss']

})
export class HeaderComponent {
	showMenu = false;
	isUserLoggedIn = true;

	toggleMenu() {
		this.showMenu = !this.showMenu;
	}
}
