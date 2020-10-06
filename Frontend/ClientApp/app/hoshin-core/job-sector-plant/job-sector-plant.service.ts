import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'ClientApp/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class JobSectorPlantService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private jobSectorPlantUrl = environment.BASE_URL + "/api/assignjobssectorsplant/";

  submitAssociation(data){
    //console.log(data);
    return this._httpClient.post(this.jobSectorPlantUrl, data, httpOptions);
  }
}
