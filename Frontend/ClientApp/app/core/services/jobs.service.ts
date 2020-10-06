import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import { environment } from 'ClientApp/environments/environment';

import { Job } from '../../hoshin-core/models/Job';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class JobsService {

  constructor(
    private _httpClient: HttpClient
  ) { }

  private jobsUrl = environment.BASE_URL + "/api/job/";

  getAll(){
    return this._httpClient.get<Job[]>(this.jobsUrl + "get");
  }

  getOne(id: Number){
    return this._httpClient.get<Job>(this.jobsUrl + "getone/" + id);
  }

  add(job: Job){
    return this._httpClient.post(this.jobsUrl + "add", job, httpOptions);
  }

  update(job: Job){
    return this._httpClient.put(this.jobsUrl + "update", job, httpOptions);
  }
}
