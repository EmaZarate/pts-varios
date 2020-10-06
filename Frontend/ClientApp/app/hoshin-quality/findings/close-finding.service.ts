import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class CloseFindingService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private findingUrl = environment.BASE_URL + "/api/finding/";
  private closeFindingUrl = environment.BASE_URL + "/api/closefinding/";

  get(id) : Observable<any> {
    return this._httpClient.get<any>(this.findingUrl + "get/" + id);
  }
  close(closeFinding) : Observable<any> {
    return this._httpClient.post(this.closeFindingUrl + "closefinding", closeFinding, httpOptions);
  }
}
