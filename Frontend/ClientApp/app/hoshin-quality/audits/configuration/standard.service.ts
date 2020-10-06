import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable, Subject } from 'rxjs';
import { map } from 'rxjs/operators';
import { Standard } from "../models/Standard";
import { Aspect } from '../models/Aspect';
import { forEach } from '@angular/router/src/utils/collection';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

@Injectable({
  providedIn: 'root'
})
export class StandardService {

  private static addAspect= new Subject<any[]>();
  addAspect$ = StandardService.addAspect.asObservable();
  _aspects : Aspect[] = [];

  constructor(
    private _httpClient: HttpClient
  ) { }

  private standardUrl = environment.BASE_URL + "/api/standard/";

  clearAspectsArray(){
    this._aspects = [];
  }

  addAspect(aspect:Aspect) {
    this._aspects.push(aspect);
    StandardService.addAspect.next(this._aspects);
  }
  
  validateAspect(aspectId, title, code) {

    let aspect = this._aspects.filter(e => e.aspectID != aspectId);
    let isValid = 0;
    
    if (aspect.length) {
      aspect.forEach((value, index) => {

        if (value.code == code) {
          isValid = 1;
          return
        }
      })      
    }
    return isValid;
  }

  getLastAspectInMemory() {
    let last = this._aspects.filter(x => x.aspectID <= 0);
    let lastAspect = last.sort((n1, n2) => n1.aspectID - n2.aspectID)[0];
    return lastAspect || null;
  }

  getAspectByIdInMemory(aspectId) {
    return this._aspects.find(x => x.aspectID == aspectId);
  }

  updateAspectInMemory(aspectId,title,code,active) {
    let aspectUpdate = this._aspects.find(x => x.aspectID == aspectId);
    aspectUpdate.code = code;
    aspectUpdate.title = title;
    aspectUpdate.active = active;
  }

  getAllAspectInMemory() {
    return Observable.of(this._aspects);
  }

  setAspectInMemory(aspects) {
    this._aspects = aspects;
  }

  getAllAspectsByStandard(id:number) {
    return this._httpClient.get<any[]>(this.standardUrl + "getAllAspectByStandard").pipe(
      map(res => res)
    );
  }

  getAll(): Observable<any[]> {
    return this._httpClient.get<any[]>(this.standardUrl + "get").pipe(
      map(res => res)
    );
  }

  get(id): Observable<Standard> {
    return this._httpClient.get<Standard>(this.standardUrl + "get/" + id);
  }

  add(standard): Observable<string> {
    
    standard.aspects = this._aspects;
    return this._httpClient.post<string>(this.standardUrl + "add", standard, httpOptions);
  }

  update(standard): Observable<string> {
    
    standard.aspects = this._aspects;
    return this._httpClient.put<string>(this.standardUrl + "update", standard, httpOptions);
  }

}
