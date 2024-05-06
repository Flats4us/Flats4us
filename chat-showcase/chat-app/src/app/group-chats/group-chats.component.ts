import { Component } from '@angular/core';
import { ChatService } from '../chat.service';

@Component({
  selector: 'app-group-chat',
  templateUrl: './group-chats.component.html',
  styleUrls: ['./group-chats.component.css']
})
export class GroupChatComponent {
  currentGroupChatId!: number;
  message = '';
  groupMessages: { groupId: number, user: string, message: string }[] = []; // Array to store messages

  constructor(private chatService: ChatService) { }

  joinGroupChat(): void {
    this.chatService.joinGroupChat(this.currentGroupChatId);
    this.chatService.
  }

  sendMessage(): void {
    this.chatService.sendGroupMessage(this.currentGroupChatId, this.message);
    this.message = ''; // Clear the message input
  }

  ngOnInit(): void {
    this.chatService.addReceiveGroupMessageHandler((groupId, user, message) => {
      this.groupMessages.push({groupId, user, message });
    });
  }
}
