import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './components/register/register.component';
import { AuthService } from './services/auth/auth.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthGuardService } from './services/auth/auth-guard.service';
import { ChatComponent } from './components/chat/chat.component';
import { ChatService } from './services/chat.service';
import { TokenInterceptorService } from './services/interceptors/token-interceptor.service';
import { HeaderComponent } from './components/header/header.component';
import { UserComponent } from './components/user/user.component';
import { UserService } from './services/user.service';
import { UserDetailsComponent } from './components/user-details/user-details.component';
import { ErrorInterceptorService } from './services/interceptors/error-interceptor.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { MessageService } from './services/message.service';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './components/home/home.component';
import { ChatsListComponent } from './components/chats-list/chats-list.component';
import { ProfileComponent } from './components/profile/profile.component';
import { ProfileService } from './services/profile.service';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    ChatComponent,
    HeaderComponent,
    UserComponent,
    UserDetailsComponent,
    HomeComponent,
    ChatsListComponent,
    ProfileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    AuthService,
    UserService,
    ChatService,
    MessageService,
    ProfileService,
    AuthGuardService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptorService,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }