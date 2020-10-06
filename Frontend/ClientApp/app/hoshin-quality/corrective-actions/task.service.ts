import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'ClientApp/environments/environment';
import { Task } from './models/Task';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class TaskConfigService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private taskUrl = environment.BASE_URL + '/api/Task/';

  Get(id): Observable<Task> {
    return this._httpClient.get<Task>(this.taskUrl + 'Get/' + id);
  }

  GetAllByCorrectiveActionID(id): Observable<Task[]> {
    return this._httpClient.get<any[]>(this.taskUrl + 'GetAllByCorrectiveActionID/' + id);
  }

  GetAllTaks(): Observable<Task[]>{
    return this._httpClient.get<any[]>(this.taskUrl + 'Get');
  }

  GetAllTaksForAC(): Observable<Task[]>{
    return this._httpClient.get<any[]>(this.taskUrl + 'GetAllForAC');
  }

  Add(task): Observable<Task> {
    return this._httpClient.post<Task>(this.taskUrl + 'Add', task, httpOptions);
  }

  Update(task): Observable<Task> {
    return this._httpClient.put<Task>(this.taskUrl + 'Update', task, httpOptions);
  }

  Delete(id) {
    const req = {
      taskID: id
    };
    return this._httpClient.post(this.taskUrl + 'Delete', req, httpOptions);
  }
}
