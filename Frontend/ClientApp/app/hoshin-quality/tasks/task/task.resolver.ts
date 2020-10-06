import { Injectable } from '@angular/core';

import { Resolve, ActivatedRouteSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { TaskService } from '../taskService.service';


@Injectable({ providedIn: 'root' })
export class TaskResolver implements Resolve<Observable<any>> {
    constructor(
        private _taskService: TaskService
    ) { }

    resolve(route: ActivatedRouteSnapshot) {
        const id = route.paramMap.get('id');
        return this._taskService.get(parseInt(id, 10));
    }
}