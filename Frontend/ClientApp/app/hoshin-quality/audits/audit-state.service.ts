import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'ClientApp/environments/environment';
import { AuditState } from "../audits/models/AuditState";
import { map } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class AuditStateService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private auditStateUrl = environment.BASE_URL + "/api/auditState/";

  getAll(): Observable<any[]>{
    return this._httpClient.get<any[]>(this.auditStateUrl+"get").pipe(
      map(res => res)
    );
  }
  get(id) : Observable<AuditState>{
    return this._httpClient.get<AuditState>(this.auditStateUrl+"get/"+id);
  }
  add(paramCrit) : Observable<AuditState>{
    return this._httpClient.post<AuditState>(this.auditStateUrl+"add", paramCrit, httpOptions);
  }
  update(auditState) : Observable<AuditState>{
    return this._httpClient.put<AuditState>(this.auditStateUrl+"update", auditState, httpOptions);
  }

}
