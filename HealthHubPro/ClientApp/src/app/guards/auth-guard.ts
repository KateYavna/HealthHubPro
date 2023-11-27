import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { JwtHelperService} from '@auth0/angular-jwt';

@Injectable()
export class AuthGuard implements CanActivate{
   
    constructor(private router: Router, private jwtHelper: JwtHelperService) { }
    canActivate(){
        const token = localStorage.getItem("jwt");
        if(token){
            return true;
        }
        this.router.navigate(['login']);
        return false;
    }
}
