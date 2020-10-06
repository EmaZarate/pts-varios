import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'ClientApp/environments/environment';
import { Audit } from './models/Audit';
import { AuditStandardAspectFinding } from './models/AuditStandardAspectFinding';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuditStandardAspectService {

  private auditStandardAspectUrl = environment.BASE_URL + "/api/auditStandardAspect/";

  constructor(
    private _httpClient: HttpClient
  ) { }

  getAllForAudit(id): Observable<Audit> {
    return this._httpClient.get<Audit>(this.auditStandardAspectUrl + "GetAllforAudit/" + id);
  }

  addFinding(finding: AuditStandardAspectFinding) {
    return this._httpClient.post(this.auditStandardAspectUrl + "addfinding", finding, httpOptions);
  }

  delete(id) {
    return this._httpClient.delete(this.auditStandardAspectUrl + "deletefinding/" + id, httpOptions);
  }

  setWithoutFindings(auditId, standardId, aspectId) {
    const req = {
      AuditID: auditId,
      StandardID: standardId,
      AspectID: aspectId
    };

    return this._httpClient.post(this.auditStandardAspectUrl + "setwithoutfindings", req, httpOptions);
  }

  setNoAudited(auditId, standardId, aspectId, description) {
    const req = {
      AuditID: auditId,
      StandardID: standardId,
      AspectID: aspectId,
      Description: description
    };

    return this._httpClient.post(this.auditStandardAspectUrl + "setnoaudited", req, httpOptions);
  }
}
