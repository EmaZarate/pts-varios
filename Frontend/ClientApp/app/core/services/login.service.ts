import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs/Rx';
import { Subject } from 'rxjs/Subject';
import { CoreModule } from 'ClientApp/app/core/core.module';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: CoreModule
})
export class LoginService {

  private isLogging = new Subject<boolean>();
  isLogging$ = this.isLogging.asObservable();

  private _authNavStatusSource = new BehaviorSubject<boolean>(false);
  authNavStatus$ = this._authNavStatusSource.asObservable();

  private loggedIn = false;



  constructor(private _httpService: HttpClient) { 
    this.loggedIn = !!localStorage.getItem('auth_token');
    this._authNavStatusSource.next(this.loggedIn);
  }

  loginExternalAuthUser(accessToken:string, endpoint:string){
    this.isLogging.next(true);
    let headers = new Headers();
    headers.append('Content-Type', 'application/json');
    let body = JSON.stringify({ accessToken });  
    let url = 'api/auth/'+endpoint;
    return this._httpService
      .post(
        url, body, httpOptions)
      .map((res:any) => {
        localStorage.setItem('auth_token', res.auth_token);
        this.loggedIn = true;
        this._authNavStatusSource.next(true);
        this.isLogging.next(false);
        return true;
      })
      .catch(this.handleError)
      .finally(() => this.isLogging.next(false));
  }
  
  loginHoshinUser(userName, password){
    
    this.isLogging.next(true);
    return this._httpService.post('/api/auth/hoshin', JSON.stringify({userName, password}), httpOptions)
    .map((res: any) => {
      
      let responseJWT = JSON.parse(res.jwt);
      localStorage.setItem('auth_token',responseJWT.auth_token);
      this.loggedIn = true;
      this._authNavStatusSource.next(true);
      this.isLogging.next(false);
      return res;
    })
    .catch(this.handleError)
    .finally(() => {
      this.isLogging.next(false);
    });
  }

  logout(){
    localStorage.removeItem('auth_token');
    this.loggedIn = false;
    this._authNavStatusSource.next(false);
  }

  isLoggedIn(){
    return this.loggedIn;
  }


  protected handleError(error: any) {
    //console.log("Error entro aca");
    var applicationError = error.headers.get('Application-Error');

    // either applicationError in header or model error in body
    if (applicationError) {
      return Observable.throw(applicationError);
    }

    return Observable.throw(error || 'Server error');
  }
}
