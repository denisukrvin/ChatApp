import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Message } from '../models/message/message';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private messagePath = environment.apiUrl + '/message';
  constructor(private http: HttpClient) { }

  all(chatId): Observable<Array<Message>> {
    return this.http.get<Array<Message>>(this.messagePath + '/all' + `/?chatId=${chatId}`);
  }

  create(chatId, text): Observable<any> {
    return this.http.post(this.messagePath + '/create', {'ChatId': chatId, 'Text': text});
  }
}