import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class RoleGuardService implements CanActivate {

  constructor(
    private _authService: AuthService,
    private _router: Router
  ) { }

  canActivate(route: ActivatedRouteSnapshot){
    const expectedClaim = route.data.expectedClaim;
    if(expectedClaim){
      const result = this._authService.hasClaim(expectedClaim);
      if(!result) this._router.navigate(['/home']);
      return result;

    }
    return true;
  }
}
