import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { AuthService } from '../core/services/auth.service';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Router } from '@angular/router';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    @BlockUI() blockUI: NgBlockUI;
    constructor(
        private _authService: AuthService,
        private _toastrManager: ToastrManager,
        private _router: Router

    ) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).do(evt => {
            console.log("Everything is fine");
        }, (err: any) => {

            if (err instanceof HttpErrorResponse) {
                
                this.blockUI.stop();
                
                if (err.status == 401) {
                    //Unauthorized
                    //this._toastrManager.errorToastr(err.message, "Unauthorized");
                    localStorage.removeItem('auth_token');
                    this._router.navigate(['/login'])
                }
                if (err.status == 500) {
                    this._toastrManager.errorToastr(err.error.message, 'Error');
                }
                if (err.status == 403) {
                    this._toastrManager.errorToastr(err.error.message, 'Error');
                }

                //Status code 436 son las excepciones custom ejemplo UserNotFoundException
                if (err.status == 436) {
                    this._toastrManager.errorToastr(err.error.message, 'Error');
                }

                console.log(err);
            }
        })
    }
}