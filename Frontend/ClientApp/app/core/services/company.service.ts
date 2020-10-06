import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';

import { Company } from '../../hoshin-core/models/Company';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private companyUrl = environment.BASE_URL + "/api/company/";

  getAll(){
    return this._httpClient.get<Company[]>(this.companyUrl + "get");
  }

  getOne(id: Number){
    return this._httpClient.get<Company>(this.companyUrl + "getone/" + id);
  }

  add(company: Company){
    return this._httpClient.post(this.companyUrl + "add", company, httpOptions);
  }

  update(company: Company){
    return this._httpClient.put(this.companyUrl + "update", company, httpOptions);
  }
}
