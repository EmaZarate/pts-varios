import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'ClientApp/environments/environment';
import { Audit } from './models/Audit';
import { ApproveRejectAudit } from './models/ApproveRejectAudit';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class AuditService {

  audit = new Audit();
  
  createAuditDate: Date

  constructor(
    private _httpClient: HttpClient
  ) { }
  
  private auditUrl = environment.BASE_URL + "/api/audit/";
  private approveRejectAuditUrl = environment.BASE_URL + "/api/approveRejectAudit/"
  private approveRejectReportUrl = environment.BASE_URL + "/api/approverejectreport/";
  
  update(audit): Observable<Audit>{    
    return this._httpClient.put<Audit>(this.auditUrl + "update",audit, httpOptions)
 }

  approveRejectAudit(approveRejectAudit): Observable<ApproveRejectAudit>{    
    return this._httpClient.put<ApproveRejectAudit>(this.approveRejectAuditUrl + "approveRejectAudit",approveRejectAudit, httpOptions)
  }

  getAll(): Observable<any>{    
    return this._httpClient.get(this.auditUrl + "get");
  }
  
  get(id): Observable<Audit>{
    return this._httpClient.get<Audit>(this.auditUrl + "get/" + id);
  }

  planning(audit): Observable<Audit>{
    return this._httpClient.post<Audit>(this.auditUrl + "planning",audit, httpOptions)
  }

  add(audit): Observable<Audit> {    
    return this._httpClient.post<Audit>(this.auditUrl + "add",audit, httpOptions)
  }

  getCreateAuditDate() {
    return this.createAuditDate
  }

  setCreateAuditDate(auditDate) {
    this.createAuditDate = auditDate;
  }
  
  emitReport(audit){
    return this._httpClient.post<Audit>(this.auditUrl+"emitreport", audit, httpOptions);
  }
  
  approveRejectAuditReport(approveRejectData) {
    return this._httpClient.post(this.approveRejectReportUrl+"approvereject", approveRejectData, httpOptions);
  }

  delete(auditId, workflowId){
    const req = {
      auditID: auditId,
      workflowId: workflowId
    };

    return this._httpClient.post(this.auditUrl+"delete", req, httpOptions);
  }
}
