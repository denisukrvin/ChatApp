import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { Chat } from '../models/chat/chat';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private chatPath = environment.apiUrl + '/chat';
  constructor(private http: HttpClient, private authService: AuthService) { }

  all(): Observable<Array<Chat>> {
    return this.http.get<Array<Chat>>(this.chatPath + '/all');
  }

  create(userId): Observable<any> {
    return this.http.post(this.chatPath + '/create', {'UserId': userId});
  }
}