import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection!: signalR.HubConnection;
  private onReceiveMessageCallbacks: ((user: string, message: string) => void)[] = [];
  private onReceivePrivateMessageCallbacks: ((user: string, message: string) => void)[] = [];

  public startConnection = (token?: string) => {
    this.stopConnection();
    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:44376/chatHub', {
                              accessTokenFactory: () => token ? token : ''
                            })
                            .build();
    
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        this.registerEventHandlers();
      })
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  private registerEventHandlers() {
    this.onReceiveMessageCallbacks.forEach(callback => {
      this.hubConnection.on('ReceiveMessage', (user, message) => {
        console.log('Broadcast message received:', user, message);
        callback(user, message);
      });
    });

    this.onReceivePrivateMessageCallbacks.forEach(callback => {
      this.hubConnection.on('ReceivePrivateMessage', (user, message) => {
        console.log('Private message received:', user, message);
        callback(user, message);
      });
    });
  }

  public addReceiveMessageHandler(callback: (user: string, message: string) => void) {
    this.onReceiveMessageCallbacks.push(callback);
  }

  public addReceivePrivateMessageHandler(callback: (user: string, message: string) => void) {
    this.onReceivePrivateMessageCallbacks.push(callback);
  }


  public sendPrivateMessage(receiverId: number, message: string) {
    this.hubConnection.invoke('SendPrivateMessage', receiverId, message)
      .catch(err => console.error(err));
  }
  // public receivePrivateMessage = (callback: (user: string, message: string) => void) => {
  //   this.hubConnection.on('ReceivePrivateMessage', (user, message) => {
  //     console.log('Private message received:', user, message);
  //     callback(user, message);
  //   });
  // }
  
  public sendMessage = (user: string, message: string) => {

    this.hubConnection.invoke('SendMessage', user, message)
      .catch(err => console.error(err));
  }

  // public receiveMessage = (callback: (user: string, message: string) => void) => {
  //   this.hubConnection.on('ReceiveMessage', (user, message) => {
  //     console.log('Broadcast message received:', user, message);

  //     callback(user, message);
  //   });
  // }
  

  public isConnected(): boolean {
    return this.hubConnection && this.hubConnection.state === signalR.HubConnectionState.Connected;
}

  public stopConnection = () => {
    if (this.isConnected()) {
        this.hubConnection.stop()
            .then(() => console.log('Connection stopped'))
            .catch(err => console.log('Error while stopping connection: ' + err));
    }
  }

}
