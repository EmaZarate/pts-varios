import { Injectable } from '@angular/core';

import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { FishboneService } from '../Fishbone.service';


@Injectable({ providedIn: 'root' })
export class FishboneCategoriesResolver implements Resolve<Observable<any>> {
    constructor(
        private fishboneCategoryService: FishboneService
    ) { }

    resolve(route: ActivatedRouteSnapshot) {
        return this.fishboneCategoryService.getAll();
    }
}