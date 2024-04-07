// auth.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginUrl = 'https://localhost:44376/api/Auth/login';

  constructor(private http: HttpClient) {}

  login(username: string, password: string): Observable<any> {
    // Create form data
    const formData = new FormData();
    formData.append('email', username);
    formData.append('password', password); 

    const requestBody = {
      email: username,
      password: password
    };

    // Change the responseType property to json
    return this.http.post(this.loginUrl, requestBody, { responseType: 'json' });
  }
}
