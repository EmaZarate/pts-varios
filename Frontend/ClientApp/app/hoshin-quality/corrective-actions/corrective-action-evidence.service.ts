import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'ClientApp/environments/environment';
import { CorrectiveAction } from './models/CorrectiveAction';
import { Observable } from 'rxjs';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class CorrectiveActionEvidenceService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private correctiveActionEvidenceUrl = environment.BASE_URL + "/api/correctiveactionevidence/";

  update(correctiveAction){
    const formData = new FormData();
    correctiveAction.Evidences.forEach(file => {
      formData.append('correctiveActionEvidences', file, file.name);
    });
    correctiveAction.FileNamesToDelete.forEach(fileName => {
      formData.append('fileNamesToDelete', fileName)
    })
    correctiveAction.Evidences = [];
    correctiveAction.FileNamesToDelete = [];
    for (var prop in correctiveAction){
      formData.append(prop, correctiveAction[prop]);
    }
    return this._httpClient.post<CorrectiveAction>(this.correctiveActionEvidenceUrl+"update", formData);
  }
}
