import {
	ChangeDetectionStrategy,
	Component,
	EventEmitter,
	Output,
} from '@angular/core';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
	public isUserLoggedIn = true;
	public isUserLoggedInAsStudent = true;

	public id = '';
	@Output() public sidenav: EventEmitter<any> = new EventEmitter();

	public toggle() {
		this.sidenav.emit();
	}
}
