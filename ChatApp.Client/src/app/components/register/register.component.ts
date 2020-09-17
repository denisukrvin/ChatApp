import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ToastrService } from 'ngx-toastr';

// import custom validator to validate that password and confirm password fields match
import { MustMatch } from '../../helpers/must-match.validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  submitted = false;
  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router, private toastrService: ToastrService) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30)]],
      email: ['', [Validators.required, Validators.email, Validators.minLength(6), Validators.maxLength(30)]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]],
      passwordConfirm: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20)]]
    }, {
      validator: MustMatch('password', 'passwordConfirm')
    });
   }

  ngOnInit(): void {
  }

  register() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

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