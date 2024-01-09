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
    formData.append('username', username);
    formData.append('password', password);

    // You might not need to set the Content-Type header yourself,
    // Angular's HttpClient does that automatically based on the request body.
    return this.http.post(this.loginUrl, formData, { responseType: 'text' });
  }
}
