import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {

	isUserLoggedIn = false;

  showOptions1 = false;
  showOptions2 = false;
  showOptions3 = false;
  showOptions4 = false;
  showOptions5 = false;

  constructor(private router: Router) { }

  logIN() {
    this.router.navigate(['/login']);
  }

}
