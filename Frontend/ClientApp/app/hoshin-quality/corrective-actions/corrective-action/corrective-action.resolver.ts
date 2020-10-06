import { Injectable } from '@angular/core';

import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { CorrectiveActionService } from '../corrective-action.service';


@Injectable({ providedIn: 'root' })
export class CorrectiveActionResolver implements Resolve<Observable<any>> {
    constructor(
        private _correctiveActionService: CorrectiveActionService
    ) { }

    resolve(route: ActivatedRouteSnapshot) {
        const id = route.paramMap.get('id');
        return this._correctiveActionService.getOne(parseInt(id, 10));
    }
}