import { Component, OnInit } from '@angular/core';
import { Chat } from '../../models/chat/chat';
import { AuthService } from '../../services/auth.service';
import { ChatService } from '../../services/chat.service';
import { MessageService } from '../../services/message.service';
import { Message } from '../../models/message/message';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {
  chats: Array<Chat> = [];
  userId: number;
  selectedChatId: number;
  messages: Array<Message> = [];
  constructor(private chatService: ChatService, private authService: AuthService, private messageService: MessageService, private toastrService: ToastrService) { }
  
  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(){
    this.userId = this.authService.getUserId();
    this.chatService.all().subscribe(res => {
      this.chats = res;
    });
  }

  getMessages(chatId) {
    this.selectedChatId = chatId;
    this.messageService.all(this.selectedChatId).subscribe(res => {
      console.log(res);
      if (res) {
        this.messages = res;
      }
      else {
        this.toastrService.error('Something went wrong, please try again later');
      }
    })
  }

  sendMessage(text) {
    console.log(text);
    if (text) {
      this.messageService.create(this.selectedChatId, text).subscribe(res => {
        if (res['success']) {
          this.messages = [];
          this.getMessages(this.selectedChatId);
        }
        else {
          this.toastrService.error(res['message']);
        }
      })
    }
    else {
      this.toastrService.warning('The message cannot be empty');
    }
  }

}