import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'ClientApp/environments/environment';

import { AuditType } from './models/AuditTypes';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuditTypesService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private auditTypesUrl = environment.BASE_URL + "/api/audittype/";

  getAll(): Observable<AuditType[]>{
    return this._httpClient.get<AuditType[]>(this.auditTypesUrl+"get").pipe(
      map(res => res)
    );
  }
  getAllActive(): Observable<AuditType[]>{
    return this._httpClient.get<AuditType[]>(this.auditTypesUrl+"getActives").pipe(
      map(res => res)
    );
  }
  get(id): Observable<AuditType>{
    return this._httpClient.get<AuditType>(this.auditTypesUrl+"get/"+id);
  }
  add(auditType): Observable<AuditType>{
    return this._httpClient.post<AuditType>(this.auditTypesUrl+"add", auditType, httpOptions);
  }
  update(auditType): Observable<AuditType>{
    return this._httpClient.put<AuditType>(this.auditTypesUrl+"update", auditType, httpOptions);
  }
}
