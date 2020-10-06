import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClaimsService {

  constructor(
    private _httpClient: HttpClient
  ) { }
  private claimsUrl = environment.BASE_URL + "/api/claim/";

  public getAll(){
    return this._httpClient.get(this.claimsUrl+"get");
  }
}
