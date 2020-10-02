import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { MessageGroup } from '../models/message/message-group';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private messagePath = environment.apiUrl + '/message';
  constructor(private http: HttpClient) { }

  all(chatId: number, lastMessageId: number = 0): Observable<Array<MessageGroup>> {
    return this.http.get<Array<MessageGroup>>(this.messagePath + '/all' + `/?chatId=${chatId}` + `&lastMessageId=${lastMessageId}`);
  }

  create(chatId, text): Observable<any> {
    return this.http.post(this.messagePath + '/create', {'ChatId': chatId, 'Text': text});
  }
}