import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../services/profile.service';
import { ToastrService } from 'ngx-toastr';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Profile } from '../../models/profile/profile';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  profileForm: FormGroup;
  submitted = false;
  profileAvatar: any;
  constructor(private fb: FormBuilder, private profileService: ProfileService, private toastrService: ToastrService) { 
    this.profileForm = this.fb.group({
      name: ['n/a', [Validators.required, Validators.minLength(2), Validators.maxLength(30)]],
      description: ['n/a', [Validators.minLength(1), Validators.maxLength(500)]]
    })
   }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.profileService.get().subscribe(res => {
      if (res) {
        this.profileAvatar = res.avatar;
        this.profileForm.patchValue(res);    
      }
      else {
        this.toastrService.error('Something went wrong, please try again later');
      }
    })
  }

  edit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.profileForm.invalid) {
      return;
    }

    let profileModel: Profile = { avatar: this.profileAvatar, name: this.profileForm.get('name').value, description: this.profileForm.get('description').value };
    this.profileService.edit(profileModel).subscribe(res => {
      if (res['success']) {
        location.reload();
        this.toastrService.success('Updated!');
      }
      else { 
        this.toastrService.error(res['message']);
      }
    }) 
  }

  onFileSelected(event, element) {
    if (event.target.files && event.target.files[0] && element) 
      {
        var reader = new FileReader();
        reader.readAsDataURL(event.target.files[0]);
        reader.onload = (event) => {
          var result = event.target.result;
          this.profileAvatar = result;
          //this.profileForm.get('avatar').setValue(event.target.result);
          element.setAttribute('src', result);
        }
     }
   }

  get name() {
    return this.profileForm.get('name');
  }
  
  get description() {
    return this.profileForm.get('description');
  }
}