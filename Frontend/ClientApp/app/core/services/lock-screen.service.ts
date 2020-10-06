import { Injectable, Output, EventEmitter } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Location } from "@angular/common";
import { filter } from 'rxjs/internal/operators/filter';

import { environment } from '../../../environments/environment';
import { Observable } from 'rxjs/Rx';
import { NgBlockUI, BlockUI } from 'ng-block-ui';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})

export class LockScreenService {

  @BlockUI() blockUI: NgBlockUI;
  
  constructor(private router: Router,
    private _location: Location,
    private _httpService: HttpClient) { }

  loadRouting(): void {

    this._location.subscribe((popStateEvent: PopStateEvent) => {
      if (popStateEvent.type === 'popstate') {

        let locked = JSON.parse(localStorage.getItem("locked"))
        if (locked.isLocked) {
          this.router.navigateByUrl('/lock');
        }

        this.router.events
          .pipe(filter(event => event instanceof NavigationEnd))
          .subscribe(({ urlAfterRedirects }: NavigationEnd) => {

            let locked = JSON.parse(localStorage.getItem("locked"))
            if (locked.isLocked) {
              this.router.navigateByUrl('/lock');
            }
            else if (urlAfterRedirects != "/lock") {

              locked = {
                previousUrl: urlAfterRedirects,
                isLocked: false
              }

              localStorage.setItem("locked", JSON.stringify(locked));
            }

          });
      }

    });
  }

  checkPassword(userName, password){
    let isLocked = true;
    return this._httpService.post('/api/auth/hoshin', JSON.stringify({userName,password,isLocked}), httpOptions)
    .map((res: any) => {            
      let responseJWT = JSON.parse(res.jwt);
      localStorage.setItem('auth_token',responseJWT.auth_token);
      this.blockUI.stop();
      return res;
    })
    .catch((error:any) => {
        debugger
        return Observable.throw(error || 'Server error');
    });    
  }

  getLockScreenTime() {
    return environment.LOCK_SCREEN_TIME;
  }
}


