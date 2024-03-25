import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { GroupChatsListComponent } from './group-chats-list/group-chats-list.component';
import { PrivateChatsListComponent } from './private-chats-list/private-chats-list.component';
import { GroupChatComponent } from './group-chats/group-chats.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'group-chats-list', component: GroupChatsListComponent },
  { path: 'private-chats-list', component: PrivateChatsListComponent},
  { path: 'group-chats', component: GroupChatComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
