import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';

import { environment } from '../../../environments/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from "../../shared/models/user";



const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  private user = new BehaviorSubject<any>(null);
  user$ = this.user.asObservable();  

  constructor(
    private _httpClient: HttpClient
  ) { }

  private _user:any

  private usersUrl = environment.BASE_URL + "/api/user/";

  setUser(user: any) {
    this._user = user;
    this.user.next(this._user);
  }

  add(newUser){
    return this._httpClient.post(this.usersUrl+"add", newUser, httpOptions);
  }

  get(id):Observable<User>{
    return this._httpClient.get<User>(this.usersUrl+"getone/"+id);
  }

  update(updateUser){
    return this._httpClient.put(this.usersUrl+"update", updateUser, httpOptions);
  }

  getAll(){
    return this._httpClient.get(this.usersUrl+"get");
  }
  getRoles(userId){
    return this._httpClient.get<String[]>(this.usersUrl+"getroles/"+userId);
  }

  getAllBySectorPlant(sectorId, plantId){
    return this._httpClient.get(this.usersUrl+"get/"+sectorId+"/"+plantId);
  }

  resetPassword(user){
    return this._httpClient.post(this.usersUrl + "resetpassword", user, httpOptions);
  }
}
