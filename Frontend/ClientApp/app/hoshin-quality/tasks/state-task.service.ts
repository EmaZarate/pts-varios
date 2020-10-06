import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'ClientApp/environments/environment';
import {StateTaskC} from './models/stateTask';

const httpOptions = {
        headers: new HttpHeaders({ 'Content-Type': 'application/json' })
      };
@Injectable({
        providedIn: 'root'
      })
export class TaskStateService {

        constructor(private _httpClient: HttpClient) {}
        private stateTaskUrl = environment.BASE_URL + '/api/TaskState/';
        getAll(): Observable<any[]> {
                return this._httpClient.get<any[]>(this.stateTaskUrl + 'get').pipe(map(res => res));
              }
         add(taskState): Observable<StateTaskC> {
                 return this._httpClient.post<StateTaskC>(this.stateTaskUrl + 'add', taskState, httpOptions);
         }
         get(id): Observable<StateTaskC> {
                 return this._httpClient.get<StateTaskC>(this.stateTaskUrl + 'get/' + id);
                }

         update(taskState): Observable<StateTaskC> {
                 
                 return this._httpClient.put<StateTaskC>(this.stateTaskUrl + 'update', taskState, httpOptions);
         }
}