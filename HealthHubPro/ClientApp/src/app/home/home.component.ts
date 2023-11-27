import { Component, OnInit } from '@angular/core';
import { JwtHelperService} from '@auth0/angular-jwt';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  isUserAuthenticated: Boolean = true;

  constructor(private jwtHelper: JwtHelperService) { }
  ngOnInit(): void {
    const token = localStorage.getItem("jwt");
    if(token){
      this.isUserAuthenticated = true;
    }
    else{
      this.isUserAuthenticated = false;
    }
    console.log(this.isUserAuthenticated);
    console.log(token);
  }

  logOut(){
    localStorage.removeItem("jwt");
    this.isUserAuthenticated = false;
  }
}
