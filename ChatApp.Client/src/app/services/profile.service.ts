import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profile } from '../models/profile/profile';

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  private profilePath = environment.apiUrl + '/profile';
  constructor(private http: HttpClient) { }

  get(): Observable<Profile> {
    return this.http.get<Profile>(this.profilePath + '/get');
  }

  edit(model: Profile): Observable<any> {
    return this.http.post(this.profilePath + '/edit', model);
  }
}