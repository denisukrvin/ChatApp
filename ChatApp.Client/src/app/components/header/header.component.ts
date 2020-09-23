import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  
  userName: string = 'n/a';
  userAvatar: string = '';

  constructor(public authService: AuthService, private router: Router) { }

  ngOnInit(): void {
    this.userName = this.authService.getUserName();
    this.userAvatar = this.authService.getUserAvatar();
  }

  logout() {
    this.authService.removeToken();
    this.router.navigate(['/login']);
  }
}