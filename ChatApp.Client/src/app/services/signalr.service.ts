import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Message } from '../models/message/message';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;
  private _receivedMessage: Subject<Message> = new Subject<Message>();
  public receivedMessageObs = this._receivedMessage.asObservable();
  constructor() { }

  public startConnection(chatId: number) {
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
  }

  public addReceiveMessageListener = () => {
    this.hubConnection.on('ReceiveMessage', (message) => {
      this._receivedMessage.next(message);
    });
  }

}