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
  showSpinner = false;
  profileAvatar: any;
  updatedProfileAvatar: any;
  constructor(private fb: FormBuilder, private profileService: ProfileService, private toastrService: ToastrService) { 
    this.profileForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(30)]],
      description: ['', [Validators.minLength(1), Validators.maxLength(500)]]
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
    this.showSpinner = true;
    // stop here if form is invalid
    if (this.profileForm.invalid) {
      this.showSpinner = false;
      return;
    }

    let profileModel: Profile = { avatar: this.updatedProfileAvatar, name: this.profileForm.get('name').value, description: this.profileForm.get('description').value };
    this.profileService.edit(profileModel).subscribe(res => {
      if (res['success']) {
        this.toastrService.success(res['message']);
      }
      else { 
        this.toastrService.error(res['message']);
      }

      this.showSpinner = false;
    }) 
  }

  onFileSelected(event, element) {
    if (event.target.files && event.target.files[0] && element) 
      {
        var reader = new FileReader();
        reader.readAsDataURL(event.target.files[0]);
        reader.onload = (event) => {
          var result = event.target.result;
          this.updatedProfileAvatar = result;
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