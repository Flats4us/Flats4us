import { Component, OnInit } from "@angular/core";
import { ChatService } from "../chat.service";
import { ChangeDetectorRef } from '@angular/core';

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

  constructor(private chatService: ChatService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.chatService.startConnection();

    this.chatService.addReceiveMessageHandler((user, message) => {
      this.messages.push({ user, message });
    });

    this.chatService.addReceivePrivateMessageHandler((user, message) => {
      this.privateMessages.push({ user, message });
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