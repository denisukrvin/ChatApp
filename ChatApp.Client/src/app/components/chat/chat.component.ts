import { Component, OnInit, AfterViewChecked  } from '@angular/core';
import { MessageService } from '../../services/message.service';
import { Message } from '../../models/message/message';
import { ToastrService } from 'ngx-toastr';
import * as signalR from '@microsoft/signalr';
import { ActivatedRoute } from '@angular/router';
import { map, mergeMap } from 'rxjs/operators';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, AfterViewChecked {

  messagesList: Array<Message> = [];
  selectedChatId: number;
  currentUserId: number;
  chatMessageText: string = '';
  messagesBlock: HTMLElement;
  private hubConnection: signalR.HubConnection;

  constructor(private messageService: MessageService, private toastrService: ToastrService, private route: ActivatedRoute,
    private authService: AuthService) { }
  
  ngOnInit(): void {
    this.fetchData();
  }

  fetchData(){
    this.route.params.pipe(map(params => {
      const id = params['id'];
      this.selectedChatId = parseInt(id);
      return id;
    }), mergeMap(id => this.messageService.all(id))).subscribe(res => {
      if (res){
        this.currentUserId = this.authService.getUserId();
        this.messagesList = res;
        this.initializeSignalRConnection(this.selectedChatId);
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
          this.messagesBlock = document.getElementById("messages-block");    
          this.messagesBlock.scrollTop = this.messagesBlock.scrollHeight; 
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

  ngAfterViewChecked() {
    this.messagesBlock = document.getElementById("messages-block");
    this.messagesBlock.scrollTop = this.messagesBlock.scrollHeight;
  }

}