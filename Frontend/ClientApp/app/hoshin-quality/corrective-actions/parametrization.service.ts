import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { Parametrization } from './models/Parametrization';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class ParametrizationService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private paramUrl = environment.BASE_URL + "/api/parametrizationCorrectiveAction/";


  getAll(): Observable<any[]>{
    return this._httpClient.get<any[]>(this.paramUrl+"get").pipe(
      map(res => res)
    );
  
  }
  get(id) : Observable<Parametrization>{
    return this._httpClient.get<Parametrization>(this.paramUrl+"get/"+id);
  }
  add(param) : Observable<Parametrization>{
    return this._httpClient.post<Parametrization>(this.paramUrl+"add", param, httpOptions);
  }
  update(param) : Observable<Parametrization>{
    return this._httpClient.put<Parametrization>(this.paramUrl+"update", param, httpOptions);
  }
  remove(id){
    return this._httpClient.delete(this.paramUrl+id, httpOptions);
  }
}