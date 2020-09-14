import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import jwt_decode from "jwt-decode";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginPath = environment.apiUrl + '/auth/login';
  private registerPath = environment.apiUrl + '/auth/register';
  constructor(private http: HttpClient) { }

  login(data): Observable<any> {
    return this.http.post(this.loginPath, data);
  }

  register(data): Observable<any> {
    return this.http.post(this.registerPath, data);
  }

  saveToken(token) {
    localStorage.setItem('token', token);
  }

  getToken() {
    return localStorage.getItem('token');
  }

  getDecodedToken() {
    var token = this.getToken();
    return jwt_decode(token);
  }

  removeToken() {
    localStorage.removeItem('token');
  }

  isAuthenticated() {
    if (this.getToken()) {
      return true;
    }
    return false;
  }

  getUserId() {
    var token = this.getDecodedToken();
    return token['user_id'];
  }

  getUserName() {
    var token = this.getDecodedToken();
    return token['user_name'];
  }
}