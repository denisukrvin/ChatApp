import { Component, OnInit } from '@angular/core';
import { Chat } from '../../models/chat/chat';
import { AuthService } from '../../services/auth.service';
import { ChatService } from '../../services/chat.service';
import { MessageService } from '../../services/message.service';
import { Message } from '../../models/message/message';
import { ToastrService } from 'ngx-toastr';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit {

  chatsList: Array<Chat> = [];
  messagesList: Array<Message> = [];
  currentUserId: number;
  selectedChatId: number;
  chatMessageText: string = '';
  private hubConnection: signalR.HubConnection;

  constructor(private chatService: ChatService, private authService: AuthService, private messageService: MessageService, private toastrService: ToastrService) { }
  
  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(){
    this.currentUserId = this.authService.getUserId();
    this.chatService.all().subscribe(res => {
      this.chatsList = res;
    });
  }

  showChatAndGetMessages(chatId) {
    this.selectedChatId = chatId;
    this.initializeSignalRConnection(chatId);

    this.messageService.all(this.selectedChatId).subscribe(res => {
      if (res) {
        this.messagesList = res;
      }
      else {
        this.toastrService.error('Something went wrong, please try again later');
      }
    })
  }

  sendMessage() {
    if (this.chatMessageText) {
      this.messageService.create(this.selectedChatId, this.chatMessageText).subscribe(res => {
        if (res['success']) {
          this.chatMessageText = '';
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

  initializeSignalRConnection(chatId: number) {   
    if (this.hubConnection) {
      this.hubConnection.stop();
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl('https://localhost:44330/chathub?chatId=' + chatId, {skipNegotiation: true, transport: signalR.HttpTransportType.WebSockets})
                            .configureLogging(signalR.LogLevel.Information)
                            .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))

      this.hubConnection.on('ReceiveMessage', (message) => {
        this.messagesList.push(message);
      });
  }

}