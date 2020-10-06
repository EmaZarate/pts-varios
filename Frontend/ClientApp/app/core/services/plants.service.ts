import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from 'ClientApp/environments/environment';

import { Plant } from '../../hoshin-core/models/Plant';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class PlantsService {

  constructor(
    private _httpClient: HttpClient
  ) { }
  private plantsUrl = environment.BASE_URL + "/api/plant/";

  getAll(){
    return this._httpClient.get<Plant[]>(this.plantsUrl + "get");
  }

  getAllActives(){
    return this._httpClient.get<Plant[]>(this.plantsUrl + "getActives");
  }

  getOne(id: Number){
    return this._httpClient.get<Plant>(this.plantsUrl + "GetOne/" + id);
  }

  add(plant: Plant){
    return this._httpClient.post(this.plantsUrl + "add", plant, httpOptions);
  }

  update(plant: Plant){
    return this._httpClient.put(this.plantsUrl + "update", plant, httpOptions);
  }


}