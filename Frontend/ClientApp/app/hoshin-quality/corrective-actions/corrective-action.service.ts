import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'ClientApp/environments/environment';
import { CorrectiveAction } from './models/CorrectiveAction';
import { Observable } from 'rxjs';
import { ActionPlanOutput } from './models/actionPlanOutput';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class CorrectiveActionService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  // private correctiveActionReassignUrl = environment.BASE_URL + '/api/reasigncorrectiveaction/'
  private correctiveActionUrl = environment.BASE_URL + '/api/correctiveaction/';
  private generateCorrectiveActionUrl = environment.BASE_URL + '/api/generatecorrectiveaction/';
  private evaluateCorrectiveActionURL = environment.BASE_URL +  '/api/evaluatecorrectiveaction';
  private extendDueDateCorrectiveActionURL = environment.BASE_URL + '/api/extendDueDateCorrectiveAction';
  private overduePlannigDateURL = environment.BASE_URL + '/api/OverduePlannigDate';
  private overdueEvaluatedDateURL = environment.BASE_URL + '/api/OverdueEvaluateDate';
  private userUrl = environment.BASE_URL + "/api/user/";
  getAll(): Observable<any> {
    return this._httpClient.get<any[]>(this.correctiveActionUrl + 'getAll');
  }

  getOne(id): Observable<any> {
    return this._httpClient.get<any>(this.correctiveActionUrl + 'getOne/' + id );
  }

  add(correctiveAction): Observable<CorrectiveAction> {
    return this._httpClient.post<CorrectiveAction>(this.correctiveActionUrl + 'add', correctiveAction);
  }
  UpdateACReassign(correctiveAction): Observable<CorrectiveAction> {
    return this._httpClient.post<CorrectiveAction>(this.correctiveActionUrl + 'UpdateACReassign', correctiveAction);
  }
  update(correctiveAction) {
    return this._httpClient.post(this.correctiveActionUrl + 'update', correctiveAction, httpOptions);
  }
  getAllUsers(sectorId, plantId){
    return this._httpClient.get(this.userUrl+"get/"+sectorId+"/"+plantId);
  }
  // reassignCorrectiveActionStep(correctiveActionReassignmentsHistory:CorrectiveActionReassignmentsHistory){
  //   return this._httpClient.post<CorrectiveActionReassignmentsHistory>(this.correctiveActionReassignUrl + "requestreassign", correctiveActionReassignmentsHistory, httpOptions);
  // }

  editImpact(impact, correctiveActionID){
    const model = {
       impact: impact.impact,
       maxDateImplementation: impact.maxDateImplementation,
       maxDateEfficiencyEvaluation: impact.maxDateEfficiencyEvaluation,
       correctiveActionID: correctiveActionID
    };
    return this._httpClient.post(this.correctiveActionUrl + 'editImpact' , model, httpOptions );
  }

  delete(id) {
    const req = {
      correctiveActionID: id
    };
    return this._httpClient.post(this.correctiveActionUrl + 'delete', req, httpOptions);
  }

  evaluateCorrectiveAction(correctiveAction) {
    const formData = new FormData();

    correctiveAction.Evidences.forEach(file => {
      formData.append('correctiveActionEvidences', file, file.name);
    });

    correctiveAction.Evidences = [];

    for (const prop in correctiveAction) {
      formData.append(prop, correctiveAction[prop]);
    }

    return this._httpClient.post(this.evaluateCorrectiveActionURL, formData);
  }

  generateAC(actionPlanData: ActionPlanOutput) {
    return this._httpClient.post(this.generateCorrectiveActionUrl, actionPlanData, httpOptions);
  }

  extendDueDate(correctiveAction: CorrectiveAction) {
    
    return this._httpClient.post(this.extendDueDateCorrectiveActionURL, correctiveAction, httpOptions);
  }
overduePlanningDate(requestExtension) {
   return this._httpClient.post(this.overduePlannigDateURL + '/OverduePlanningDate', requestExtension, httpOptions);
}

overdueEvaluatedDate(requestExtension) {
debugger
  return this._httpClient.post(this.overdueEvaluatedDateURL + '/OverdueEvaluateDate', requestExtension, httpOptions);
}

  // extendDueEvaluationDate(correctiveAction: CorrectiveAction) {
  //   return this._httpClient.post(this.correctiveActionUrl + "ExtendDueDateCorrectiveAction", correctiveAction, httpOptions);
  // }
}
