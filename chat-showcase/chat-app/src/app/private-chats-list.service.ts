

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PrivateChatsListService {
  private apiUrl = 'https://localhost:44376/api'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  getUserChats(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Chat/user/chats`);
  }

  // Add other service methods as needed for sending/receiving private messages
}
