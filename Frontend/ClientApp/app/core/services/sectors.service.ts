import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from 'ClientApp/environments/environment';

import { Sector } from '../../hoshin-core/models/Sector';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class SectorsService {

  constructor(
    private _httpClient: HttpClient
  ) { }
  private sectorsUrl = environment.BASE_URL + "/api/sector/";

  getAll(){
    return this._httpClient.get<Sector[]>(this.sectorsUrl + "get");
  }

  getOne(id: Number){
    return this._httpClient.get<Sector>(this.sectorsUrl + "GetOne/" + id);
  }

  add(sector: Sector){
    return this._httpClient.post(this.sectorsUrl + "add", sector, httpOptions);
  }

  update(sector: Sector){
    
    return this._httpClient.put(this.sectorsUrl + "update", sector, httpOptions);
  }

}