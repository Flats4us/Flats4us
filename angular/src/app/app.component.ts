import { ChangeDetectionStrategy, Component, ViewChild } from '@angular/core';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class AppComponent {
	public title = 'angular';
	/*@ViewChild(HeaderComponent)
	public buttons: HeaderComponent

	constructor(buttons: HeaderComponent) {
		this.buttons = buttons;
	}*/
}
