import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { User } from '../models/user/user';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private userPath = environment.apiUrl + '/user';
  constructor(private http: HttpClient) { }

  all(): Observable<Array<User>> {
    return this.http.get<Array<User>>(this.userPath + '/all');
  }

  get(id): Observable<User> {
    return this.http.get<User>(this.userPath + '/get' + `/?userId=${id}`);
  }
}