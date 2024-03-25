import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GroupChatListService {
  private apiUrl = 'https://localhost:44376/api'; // Replace with your API URL

  constructor(private http: HttpClient) { }

  getUserGroupChats(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/GroupChat/user/groupchats`);
  }
}
