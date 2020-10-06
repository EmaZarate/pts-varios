import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'ClientApp/environments/environment';
import { Aspect } from './models/Aspect';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

@Injectable({
  providedIn: 'root'
})
export class AspectService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private auditStateUrl = environment.BASE_URL + "/api/aspect/";

  get(standardId, aspectId) : Observable<Aspect>{
    return this._httpClient.get<Aspect>(this.auditStateUrl+"get/"+standardId+"/"+aspectId);
  }


}
