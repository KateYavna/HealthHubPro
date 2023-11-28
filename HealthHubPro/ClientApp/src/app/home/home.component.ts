import { Component, OnInit, ViewChild } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  isUserAuthenticated: Boolean = true;

  constructor() { }
  ngOnInit(): void {
    const token = localStorage.getItem("jwt");
    if(token){
      this.isUserAuthenticated = true;
    }
    else{
      this.isUserAuthenticated = false;
    }
  }
  logOut(){
    localStorage.removeItem("jwt");
    this.isUserAuthenticated = false;
  }
}
