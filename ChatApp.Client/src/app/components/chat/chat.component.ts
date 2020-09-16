import { Component, OnInit } from '@angular/core';
import { Chat } from '../../models/chat/chat';
import { AuthService } from '../../services/auth.service';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  chats: Array<Chat>;
  userId: number;
  constructor(private chatService: ChatService, private authService: AuthService) { }
  
  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(){
    this.userId = this.authService.getUserId();
    this.chatService.all().subscribe(res => {
      this.chats = res;
    });
  }

}