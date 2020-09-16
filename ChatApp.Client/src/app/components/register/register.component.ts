import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private toastrService: ToastrService) {
    this.registerForm = this.fb.group({
      'name': ['', Validators.required],
      'email': ['', Validators.required],
      'password': ['', Validators.required],
      'passwordConfirm': ['', Validators.required]
    })
   }

  ngOnInit(): void {
  }

  register() {
    this.authService.register(this.registerForm.value).subscribe(res => {
      if (res['success'] && res['data']['token']) {
        this.authService.saveToken(res['data']['token']);
        this.router.navigate(['']);
      }
      else { 
        this.toastrService.error(res['message']);
      }
    })
  }

  get name() {
    return this.registerForm.get('name');
  }

  get email() {
    return this.registerForm.get('email');
  }

  get password() {
    return this.registerForm.get('password');
  }

  get passwordConfirm() {
    return this.registerForm.get('passwordConfirm');
  }
}