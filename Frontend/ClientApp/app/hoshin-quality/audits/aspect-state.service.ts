import { Injectable } from '@angular/core';
import { of, Observable } from 'rxjs';
import { AspectsStates } from "./models/AspectsStates";
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'ClientApp/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

@Injectable({
  providedIn: 'root'
})
export class AspectStateService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private aspectStatesUrl = environment.BASE_URL + "/api/aspectStates/";

  asp = new AspectsStates();

  getAll(): Observable<any>{
    debugger
    return this._httpClient.get(this.aspectStatesUrl + "get");
  }

  update(state): Observable<AspectsStates>{
    debugger
     return this._httpClient.put<AspectsStates>(this.aspectStatesUrl + "update",state, httpOptions )
  }

  get(id){
    return this._httpClient.get(this.aspectStatesUrl + "get/" + id);
  }

  add(AspectsStates): Observable<AspectsStates> {
    debugger
    return this._httpClient.post<AspectsStates>(this.aspectStatesUrl + "add",AspectsStates, httpOptions )
  }
}
