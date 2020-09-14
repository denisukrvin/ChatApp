import { Component, OnInit } from '@angular/core';
import { Chat } from '../../models/chat/chat';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  chats: Array<Chat>;
  constructor(private chatService: ChatService) { }
  
  ngOnInit(): void {
    this.chatService.all().subscribe(data => {
      this.chats = data;
    });
  }

}