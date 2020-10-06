import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from 'ClientApp/environments/environment';

import { Finding } from './models/Finding';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class EditExpirationdateFindingService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private findingUrl = environment.BASE_URL + "/api/finding/";
  private editExpirationdateFindingUrl = environment.BASE_URL + "/api/EditExpirationDateFinding/";

  get(id) : Observable<Finding> {
    return this._httpClient.get<Finding>(this.findingUrl + "get/" + id);
  }
  editExpirationdateFinding(editExpirationdateFinding) : Observable<any> {
    return this._httpClient.post(this.editExpirationdateFindingUrl + "EditExpirationDateFinding", editExpirationdateFinding, httpOptions);
  }
}
