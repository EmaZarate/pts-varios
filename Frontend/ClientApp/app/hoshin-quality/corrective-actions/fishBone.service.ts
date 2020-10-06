import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'ClientApp/environments/environment';
//import { Fishbone } from './models/Fishbone';
import {FishBoneC} from './models/fishBoneState'
const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
}
@Injectable({
  providedIn: 'root'
})

export class FishboneService {

  constructor(private _httpClient: HttpClient) { }

  private fishBoneUrl = environment.BASE_URL + "/api/fishBone/";
  private editCorrectiveActionFishboneUrl = `${environment.BASE_URL}/api/editcorrectiveactionfishbone`;

  getAll(): Observable<any[]> {
    return this._httpClient.get<FishBoneC[]>(this.fishBoneUrl + "get").pipe(map(res => res));
  }

  getAllActive(): Observable<any[]> {
    return this._httpClient.get<FishBoneC[]>(this.fishBoneUrl + "getAllActive").pipe(map(res => res));
  }

  add(fishBone): Observable<FishBoneC> {
    return this._httpClient.post<FishBoneC>(this.fishBoneUrl + "add", fishBone, httpOptions)
  }
  get(id): Observable<FishBoneC> {
    return this._httpClient.get<FishBoneC>(this.fishBoneUrl + "get/" + id)
  }

  update(fishBone): Observable<FishBoneC> {
    return this._httpClient.put<FishBoneC>(this.fishBoneUrl + "update", fishBone, httpOptions)
  }

  editCorrectiveActionFishbone(correctiveActionFishbone, correctiveActionId) {
    return this._httpClient.post(`${this.editCorrectiveActionFishboneUrl}/${correctiveActionId}`, correctiveActionFishbone, httpOptions);
  }

}