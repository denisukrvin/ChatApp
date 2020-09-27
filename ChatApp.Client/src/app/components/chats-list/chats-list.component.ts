import { Component, OnInit, Input } from '@angular/core';
import { ChatService } from '../../services/chat.service';
import { Chat } from '../../models/chat/chat';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-chats-list',
  templateUrl: './chats-list.component.html',
  styleUrls: ['./chats-list.component.css']
})
export class ChatsListComponent implements OnInit {

  @Input() selectedChatId: number;
  currentUserId: number;
  chatsList: Array<Chat> = [];
  constructor(private authService: AuthService, private chatService: ChatService) { }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.currentUserId = this.authService.getUserId();
    this.chatService.all().subscribe(res => {
      this.chatsList = res;
    });
  }
}