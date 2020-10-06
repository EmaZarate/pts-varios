import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'ClientApp/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}

@Injectable({
  providedIn: 'root'
})
export class CorrectiveActionWorkgroupService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private correctiveActionWorkgroupUrl = environment.BASE_URL + "/api/editCorrectiveActionWorkgroup/";
  
  add(users) {
    return this._httpClient.post(this.correctiveActionWorkgroupUrl+"add", users, httpOptions);
  }

}
