import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from '../../models/user/user';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {
  userId: string;
  user: User;
  constructor(private route: ActivatedRoute, private userService: UserService) { 
    this.route.params.subscribe(res => {
      this.userId = res['id'];
      this.userService.get(this.userId).subscribe(res => {
        this.user = res;
      })
    })
  }

  ngOnInit(): void {
  }

}
