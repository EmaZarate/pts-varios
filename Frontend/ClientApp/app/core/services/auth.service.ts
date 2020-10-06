import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs/Subject';

import { CoreModule } from '../core.module';

import { environment } from '../../../environments/environment';

import { LoginService } from './login.service';

import * as jwt_decode from "jwt-decode";


@Injectable({
  providedIn: CoreModule
})

export class AuthService {
  

  private static errorInLogin = new Subject();
  errorInLogin$ = AuthService.errorInLogin.asObservable();
    constructor(
      private loginService: LoginService,
      private router: Router
    ) { 
      if(window.addEventListener){
        window.addEventListener("message", this.handleMessage.bind(this), false);
    }else{
      (<any>window).attachEvent('onmessage', this.handleMessage.bind(this));
    }
    }

    authWindow: Window;
    failed: boolean;
    error: string;
    errorDescription: string;

    canActivate(){
      if(!this.loginService.isLoggedIn()){
        this.router.navigate(['/login']);
        return false;
      }
      return true;
    }

    getAccesToken(){
      let token = localStorage.getItem('auth_token');

      if(!token){
        return "this is a test";
      }
      else{
        return token;
      }
    }

    getUserLogged() {
      let user = this.getDecodedAccessToken();      
      return user ? user : null;
    }

    hasClaim(claim:string) : boolean {
      const user = this.getDecodedAccessToken();
      const claims = user["module/permission"];

      return claims.includes(claim);
    }

    
    private getDecodedAccessToken(){
      try{
        let token = localStorage.getItem('auth_token');
        return jwt_decode(token);
      }
      catch(Error){
        return null
      }
    }
  
    private getTokenExpirationDate(){
      let token = this.getDecodedAccessToken();
  
      if(token.exp == undefined) return null;
  
      const date = new Date(0);
      date.setUTCSeconds(token.exp);
      return date;
    }
    
    isTokenExpired(): boolean{
      let token = this.getDecodedAccessToken();
      if(!token) return true;
  
      const date = this.getTokenExpirationDate();
      if(date === undefined) return false;
      return !(date.valueOf() > new Date().valueOf());
    }

    handleMessage(event: Event) {
      event.preventDefault();
      const message = event as MessageEvent;

        // Only trust messages from the below origin.
        //
        if ((message.origin !== environment.BASE_URL)) return;
      
      const result = JSON.parse(message.data);

      if (!result.status)
      {
        this.failed = true;
        this.error = result.error;
        this.errorDescription = result.errorDescription;
      }
      else
      {
        this.failed = false;

        this.loginService.loginExternalAuthUser(result.accessToken,result.provider)
          .subscribe(           
          result => {
            if (result) {
              AuthService.errorInLogin.next(null);
              this.router.navigate(['/home']);
            }
          },
          error => {
            AuthService.errorInLogin.next(error);
            this.failed = true;
            this.error = error;
          });      
      }
    }

}
