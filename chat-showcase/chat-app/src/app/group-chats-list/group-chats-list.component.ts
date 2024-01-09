import { Component, OnInit } from '@angular/core';
import { GroupChatListService } from '../group-chats-list.service';

@Component({
  selector: 'app-group-chats',
  templateUrl: './group-chats-list.component.html',
  styleUrls: ['./group-chats-list.component.css']
})
export class GroupChatsListComponent implements OnInit {
  groupChats: any[] = [];

  constructor(private groupChatService: GroupChatListService) { }

  ngOnInit(): void {
    this.groupChatService.getUserGroupChats().subscribe(
      data => {
        this.groupChats = data;
        console.log(this.groupChats)
      },
      error => {
        console.error('Error fetching group chats', error);
      }
    );
  }
}
