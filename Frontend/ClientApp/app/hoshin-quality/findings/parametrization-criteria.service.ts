
import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

import { ParametrizationCriteria } from './models/ParametrizationCriteria';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class ParametrizationCriteriaService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private paramCriteriaUrl = environment.BASE_URL + "/api/parametrizationcriteria/";


  getAll(): Observable<any[]>{
    return this._httpClient.get<any[]>(this.paramCriteriaUrl+"get").pipe(
      map(res => res)
    );
      
  }
  get(id) : Observable<ParametrizationCriteria>{
    return this._httpClient.get<ParametrizationCriteria>(this.paramCriteriaUrl+"get/"+id);
  }
  add(paramCrit) : Observable<ParametrizationCriteria>{
    return this._httpClient.post<ParametrizationCriteria>(this.paramCriteriaUrl+"add", paramCrit, httpOptions);
  }
  update(paramCrit) : Observable<ParametrizationCriteria>{
    return this._httpClient.put<ParametrizationCriteria>(this.paramCriteriaUrl+"update", paramCrit, httpOptions);
  }
  remove(id){
    return this._httpClient.delete(this.paramCriteriaUrl+id, httpOptions);
  }
}
