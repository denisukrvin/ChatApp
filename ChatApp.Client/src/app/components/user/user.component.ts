import { Component, OnInit } from '@angular/core';
import { User } from '../../models/user/user';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  users: Array<User>;
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.all().subscribe(res => {
      this.users = res;
    });
  }

}