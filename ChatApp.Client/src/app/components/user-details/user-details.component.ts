import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { User } from '../../models/user/user';
import { UserService } from '../../services/user.service';
import { ChatService } from '../../services/chat.service';
import { map, mergeMap } from 'rxjs/operators';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  userId: string;
  user: User = { id: 0, avatar: null, name: 'n/a', description: null };
  constructor(private route: ActivatedRoute, private router: Router, private userService: UserService, private chatService: ChatService, private toastrService: ToastrService) { 
    this.fetchData();
  }

  ngOnInit(): void {
  }

  fetchData(){
    this.route.params.pipe(map(params => {
      const id = params['id'];
      return id;
    }), mergeMap(id => this.userService.get(id))).subscribe(res => {
      if (res){
        this.user = res;
      }
      else {
        this.toastrService.error('User is not found');
      }
    })
  }

  createChat(userId) {
    this.chatService.create(userId).subscribe(res => {
      if (res['success'] && res['data']['chat_id']) {
        let chatId = res['data']['chat_id'];
        this.router.navigate(['/chat/' + chatId]);
      }
      else { 
        this.toastrService.error(res['message']);
      }
    })
  }
}