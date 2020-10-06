import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'ClientApp/environments/environment';

import { FindingsStates } from './models/FindingsStates';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

@Injectable({
  providedIn: 'root'
})
export class FindingsStatesService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private findingsStatesUrl = environment.BASE_URL + "/api/findingsstates/";

  getAll(): Observable<any[]>{
    return this._httpClient.get<any[]>(this.findingsStatesUrl+"get").pipe(
      map(res => res)
    );
  }
  get(id) : Observable<FindingsStates>{
    return this._httpClient.get<FindingsStates>(this.findingsStatesUrl+"get/"+id);
  }
  add(paramCrit) : Observable<FindingsStates>{
    return this._httpClient.post<FindingsStates>(this.findingsStatesUrl+"add", paramCrit, httpOptions);
  }
  update(paramCrit) : Observable<FindingsStates>{
    return this._httpClient.put<FindingsStates>(this.findingsStatesUrl+"update", paramCrit, httpOptions);
  }
  remove(id){
    return this._httpClient.delete(this.findingsStatesUrl+id, httpOptions);
  }
}
