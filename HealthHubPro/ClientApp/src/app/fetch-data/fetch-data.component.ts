import { Component } from '@angular/core';
import { Login } from '../models/Login.model';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProfileFormService } from '../services/profile-form.service';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
 
login: Login = new Login();
invalidLogin: boolean = false;

 constructor(
    private router: Router,
    private service: ProfileFormService,
 ) {}
onSubmit(loginSubmit: NgForm){
  this.service.login(this.login)
  .subscribe({
    next: res => {
      const token = (<any>res).token;
      localStorage.setItem("jwt",token);
      this.router.navigateByUrl('');
    },
    error: err => {
      this.invalidLogin = true;
      console.log(err);
    }
  });
}
}

