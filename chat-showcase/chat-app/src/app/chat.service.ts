import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection!: signalR.HubConnection;

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:44376/chatHub') // Update with your backend URL
                            .build();
    
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public sendPrivateMessage(receiverId: number, message: string) {
    this.hubConnection.invoke('SendPrivateMessage', receiverId, message)
      .catch(err => console.error(err));
  }
  public receivePrivateMessage = (callback: (user: string, message: string) => void) => {
    this.hubConnection.on('ReceivePrivateMessage', (user, message) => {
      callback(user, message);
    });
  }
  
  public sendMessage = (user: string, message: string) => {
    this.hubConnection.invoke('SendMessage', user, message)
      .catch(err => console.error(err));
  }

  public receiveMessage = (callback: (user: string, message: string) => void) => {
    this.hubConnection.on('ReceiveMessage', (user, message) => {
      callback(user, message);
    });
  }
  
}
