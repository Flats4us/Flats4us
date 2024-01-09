// import { Component } from '@angular/core';

// @Component({
//   selector: 'app-private-chats-list',
//   templateUrl: './private-chats-list.component.html',
//   styleUrls: ['./private-chats-list.component.css']
// })
// export class PrivateChatsListComponent {

// }
// @@@@@@@@@@@@@@
import { Component, OnInit } from '@angular/core';
import { PrivateChatsListService } from '../private-chats-list.service';

@Component({
  selector: 'app-private-chats-list',
  templateUrl: './private-chats-list.component.html',
  styleUrls: ['./private-chats-list.component.css']
})
export class PrivateChatsListComponent implements OnInit {
  privateChats: any[] = [];

  constructor(private privateChatsListService: PrivateChatsListService) { }

  ngOnInit(): void {
    this.privateChatsListService.getUserChats().subscribe(
      data => {
        console.log("got some data");
        console.log(data);

        this.privateChats = data;
      },
      error => {
        console.error('Error fetching private chats', error);
      }
    );
  }
}
