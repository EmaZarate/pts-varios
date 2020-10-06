import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from '../../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};


@Injectable({
  providedIn: 'root'
})
export class RolesService {

  constructor(
    private _httpClient: HttpClient
  ) { }
  private rolesUrl = environment.BASE_URL + "/api/role/";

  public checkIfExists(name){
    return this._httpClient.get(this.rolesUrl+"check/"+name);
  }

  public checkIfExistsBasic(){
    return this._httpClient.get(this.rolesUrl+"checkBasic/");
  }

  public addRole(role){
    return this._httpClient.post(this.rolesUrl+"add", role, httpOptions);
  }

  public getOne(id){
    return this._httpClient.get(this.rolesUrl+"get/"+id);
  }

  public updateRole(role, id){
    return this._httpClient.put(this.rolesUrl+"update/"+id, role, httpOptions);
  }

  public getAll(){
    return this._httpClient.get(this.rolesUrl+"getall");
  }
  public getAllRolesActive(){
    return this._httpClient.get(this.rolesUrl+"getAllRolesActive");
  }
}
