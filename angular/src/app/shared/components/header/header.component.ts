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
	styleUrls: ['./header.component.scss'],
	animations: [
		trigger('sidenav', [
			state(
				'open',
				style({
					transform: 'translateX(0)',
				})
			),
			state(
				'closed',
				style({
					transform: 'translateX(-100%)',
				})
			),
			transition('open => closed', [animate('0.2s')]),
			transition('closed => open', [animate('0.2s')]),
		]),
	],
})
export class HeaderComponent {
	showMenu = false;
	isUserLoggedIn = true;

	toggleMenu() {
		this.showMenu = !this.showMenu;
	}
}
