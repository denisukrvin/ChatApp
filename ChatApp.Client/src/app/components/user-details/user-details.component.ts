import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../models/user/user';
import { UserService } from '../../services/user.service';
import { ChatService } from '../../services/chat.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  userId: string;
  user: User;
  constructor(private route: ActivatedRoute, private router: Router, private userService: UserService, private chatService: ChatService) { 
    this.route.params.subscribe(res => {
      this.userId = res['id'];
      this.userService.get(this.userId).subscribe(res => {
        this.user = res;
      })
    })
  }

  ngOnInit(): void {
  }

  createChat(userId) {
    this.chatService.create(userId).subscribe(res => {
      console.log(res);
      if (res['success'] && res['data']['chat_id']) {
        this.router.navigate(['']);
      }
      else { 
        alert(res['message']);
      }
    })
  }
}