import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ChatComponent } from './chat/chat.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './login/login.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'; // Import HttpClientModule
import { AuthService } from './auth.service';
import { GroupChatsListComponent } from './group-chats-list/group-chats-list.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { PrivateChatsListComponent } from './private-chats-list/private-chats-list.component';
import { GroupChatComponent } from './group-chats/group-chats.component';
// import { HTTP_INTERCEPTORS } from '@angular/common/http';
// import { JwtInterceptor } from './interceptors/jwt.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    ChatComponent,
    LoginComponent,
    GroupChatsListComponent,
    PrivateChatsListComponent,
    GroupChatComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    AuthService,
    
      { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  
    // { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
