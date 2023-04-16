import { ChangeDetectionStrategy, Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
	selector: 'app-header',
	templateUrl: './header.component.html',
	styleUrls: ['./header.component.scss'],
	changeDetection: ChangeDetectionStrategy.OnPush,
})
export class HeaderComponent {
  isUserLoggedIn = true;
  isUserLoggedInAsStudent = true;

  constructor(private router: Router) {}

  logIn() {
    this.router.navigate(['auth/login']);
  }
  signIn() {
    this.router.navigate(['auth/register']);
  }
  profile() {
    this.router.navigate(['profile/profile'])
  }
}
