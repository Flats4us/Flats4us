import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
	isUserLoggedIn = true;

  showOptions1 = false;
  showOptions2 = false;
  showOptions3 = false;
  showOptions4 = false;

}
