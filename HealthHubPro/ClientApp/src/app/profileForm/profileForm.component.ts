import { Component, OnInit } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { ProfileFormService } from '../services/profile-form.service';
import { ProfileFormModel } from '../models/ProfileFormModel.model';


@Component({
  selector: 'app-profileForm',
  templateUrl: './profileForm.component.html'
})
export class ProfileFormComponent implements OnInit {
  rolesList: string[] = [];
  formData: ProfileFormModel = new ProfileFormModel();
  constructor(private formBuilder: FormBuilder, public profileFormService: ProfileFormService) { }

  ngOnInit() {
    this.profileFormService.getAllRoles()
    .subscribe({
      next: res => {
        this.rolesList = res;
      }
    })
  }
  onSubmit(form: NgForm) {
    this.profileFormService.postFormData(this.formData)
      .subscribe({
        next: res => {
          this.resetProfileForm(form);
        },
        error: err => {
          console.log(err);
        }
      });
  }
  resetProfileForm(form: NgForm){
    form.form.reset();
    this.formData = new ProfileFormModel();
  }
  onRoleChange(event: any, role: string) {
    if (event.target.checked) {
      this.formData.roles.push(role);
    } else {
      const index = this.formData.roles.indexOf(role);
      if (index !== -1) {
        this.formData.roles.splice(index, 1);
      }
    }
  }
}
