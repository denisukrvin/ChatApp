import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private toastrService: ToastrService) {
    this.loginForm = this.fb.group({
      'email': ['', Validators.required],
      'password': ['', Validators.required]
    })
   }

  ngOnInit(): void {
  }

  login() {
    this.authService.login(this.loginForm.value).subscribe(res => {
      if (res['success'] && res['data']['token']) {
        this.authService.saveToken(res['data']['token']);
        this.router.navigate(['']);
      }
      else { 
        this.toastrService.error(res['message']);
      }
    })
  }

  get email() {
    return this.loginForm.get('email');
  }

  get password() {
    return this.loginForm.get('password');
  }
}