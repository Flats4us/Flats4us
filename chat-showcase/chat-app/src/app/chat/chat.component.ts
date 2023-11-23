import { Component, OnInit } from "@angular/core";
import { ChatService } from "../chat.service";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  messages: {user: string, message: string}[] = [];
  privateMessages: { user: string, message: string }[] = []; 
  username: string = '';
  message: string = '';
  receiverId: number = 0;

  constructor(private chatService: ChatService) { }

  ngOnInit(): void {
    this.chatService.startConnection();
    this.chatService.receiveMessage((user, message) => {
      this.messages.push({user, message});
    });
    this.chatService.receivePrivateMessage((user, message) => {
      // You can add a condition or a flag to differentiate private messages
      this.messages.push({user, message});
    });
  }

  sendMessage(): void {
    this.chatService.sendMessage(this.username, this.message);
    this.message = '';
  }
  
  sendPrivateMessage(receiverId: number): void {
    this.chatService.sendPrivateMessage(receiverId, this.message);
    this.message = '';
  }
}