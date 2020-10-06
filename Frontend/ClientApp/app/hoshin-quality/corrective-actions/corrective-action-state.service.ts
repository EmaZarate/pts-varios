import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'ClientApp/environments/environment';
import { CorrectiveActionState } from "../corrective-actions/models/CorrectiveActionState";
import { map } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

@Injectable({
  providedIn: 'root'
})
export class CorrectiveActionStateService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private correctiveActionStateUrl = environment.BASE_URL + "/api/correctiveActionState/";

  getAll(): Observable<any[]>{
    return this._httpClient.get<any[]>(this.correctiveActionStateUrl+"get").pipe(
      map(res => res)
    );
  }
  get(id) : Observable<CorrectiveActionState>{
    return this._httpClient.get<CorrectiveActionState>(this.correctiveActionStateUrl+"get/"+id);
  }
  add(correctiveAction) : Observable<CorrectiveActionState>{
    return this._httpClient.post<CorrectiveActionState>(this.correctiveActionStateUrl+"add", correctiveAction);
  }
  update(correctiveAction) : Observable<CorrectiveActionState>{
    return this._httpClient.put<CorrectiveActionState>(this.correctiveActionStateUrl+"update", correctiveAction);
  }
}
