import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Subject, Observable } from 'rxjs';

import { environment } from '../../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class FindingsTypeService {
  

  private static addParamCriteria = new Subject<any[]>();
  addParamCriteria$ = FindingsTypeService.addParamCriteria.asObservable();
  paramCrit = [];

  constructor(
    private _httpClient: HttpClient
  ) { }
  private findingtypesUrl = environment.BASE_URL + "/api/findingtypes/";

  clearParamArray(){
    this.paramCrit = [];
  }

  addParametrizationCriteria(res) {
    this.paramCrit.push(res);
    FindingsTypeService.addParamCriteria.next(this.paramCrit);
  }

  removeParametrizationCriteria(id){
    let param = this.paramCrit.find(e=> e.id == id);
    let i = this.paramCrit.indexOf(param);
    this.paramCrit.splice(i, 1);
  }
  
  getAllParametrizationCriteria() {
    return Observable.of(this.paramCrit);
  }

  get(id){
    return this._httpClient.get(this.findingtypesUrl+"get/"+id);
  }

  getAll(){
    return this._httpClient.get(this.findingtypesUrl + "get");
  }

  getAllActives(){
    return this._httpClient.get(this.findingtypesUrl + "getActives")
  }

  getAllForAudit() {
    return this._httpClient.get(this.findingtypesUrl + "getforaudit");
  }

  add(findingType){
    findingType.parametrization = this.paramCrit;
    return this._httpClient.post(this.findingtypesUrl+"add", findingType, httpOptions);
  }

  update(findingType){
    findingType.parametrization = this.paramCrit;
    return this._httpClient.put(this.findingtypesUrl+"update", findingType, httpOptions);
  }

  delete(id){
    return this._httpClient.delete(this.findingtypesUrl+"delete/"+id, httpOptions);
  }
}
