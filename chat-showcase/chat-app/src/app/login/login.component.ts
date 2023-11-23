import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  title = 'Login';
  username: string ='';
  password: string = '';

  constructor(private authService: AuthService) {}

  login() {
    this.authService.login(this.username, this.password).subscribe(
      response => {
        console.log(response); // Print the response to the console
        localStorage.setItem('jwt', response); // Store the JWT token
      },
      error => {
        console.error('Login error:', error); // Print error to the console
      }
    );
  }
}