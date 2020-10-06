import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import * as signalR from '@aspnet/signalr';
import { environment } from '../../../environments/environment';

import { Finding } from '../../hoshin-quality/findings/models/Finding';
import { FindingReassignmentsHistory } from "../../hoshin-quality/findings/models/FindingReassignmentsHistory";
import { _MatChipListMixinBase } from '@angular/material';
import { FindingEvidence } from '../../hoshin-quality/findings/models/FindingEvidence';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};


const httpOptionsFormData = {
  headers: new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded'})
};

@Injectable({
  providedIn: 'root'
})
export class FindingsService {

  finding: Finding;
  chat;

  private emmitFinding = new BehaviorSubject<Finding>(new Finding());
  finding$: Observable<Finding> = this.emmitFinding.asObservable();

  private emmitChat = new BehaviorSubject<any>(new Object());
  chat$: Observable<Finding> = this.emmitChat.asObservable();
  

  constructor(
    private _httpClient: HttpClient
  ) { }

  private hubConnection: signalR.HubConnection

  private findingUrl = environment.BASE_URL + "/api/finding/";
  private userUrl = environment.BASE_URL + "/api/user/";
  private findingApproveUrl = environment.BASE_URL+"/api/ApproveFinding/";
  private findingCreateUrl = environment.BASE_URL + "/api/createfinding/";
  private findingUpdateApprovedUrl = environment.BASE_URL + "/api/updateapprovedfinding/"
  private findingReassignUrl = environment.BASE_URL + "/api/reassignfinding/"

  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl(environment.BASE_URL + '/findings')
      //.withUrl('http://localhost:5627' + '/notify')
      //.withUrl('http://localhost:5000' + '/findings')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public addTransferChartDataListener = () => {
    this.hubConnection.on('transferfindingsdata', (data) => {
      this.finding = data;
      this.emmitFinding.next(this.finding);
      
      console.log(this.finding);
    });
  }
  

  public send = (messeger) => {
    this.hubConnection.invoke('SendMessage', messeger).catch(err => console.log(err));
  }

  disconnect(){
    this.hubConnection.stop();
  }

  public addTransferChatDataListener = () => {
    this.hubConnection.on('ReceiveMessage', (data) => {
      this.chat = data;
      this.emmitChat.next(this.chat);
      
      console.log(this.chat);
    });
  }

  getAll(): Observable<Finding[]>{
    return this._httpClient.get<any[]>(this.findingUrl + "get");
  }

  get(id){
    return this._httpClient.get(this.findingUrl+"get/"+id);
  }

  getAllApprovedInProgress(){
    return this._httpClient.get<any[]>(this.findingUrl + "getapprovedinprogress");
  }

  getAllUsers(sectorId, plantId){
    return this._httpClient.get(this.userUrl+"get/"+sectorId+"/"+plantId);
  }
  getOneUser(id){
    return this._httpClient.get(this.userUrl+"getone/"+id);
  }

  approveFindingStep(finding){
    const formData = new FormData();
    finding.findingEvidences.forEach(file => {
      formData.append('findingEvidences', file, file.name);
    });
    finding.fileNamesToDelete.forEach(fileName => {
      formData.append('fileNamesToDelete', fileName)
    })
    finding.findingEvidences = [];
    finding.fileNamesToDelete = [];
    for (var prop in finding){
      formData.append(prop, finding[prop]);
    }
    return this._httpClient.post(this.findingApproveUrl+"ApproveFinding", formData);
  }


  updateApprovedFindingStep(finding){
    const formData = new FormData();
    finding.findingEvidences.forEach(file => {
      // formData.append('findingEvidences[]', file, file.name);
      formData.append('findingEvidences', file, file.name);
    });
    finding.fileNamesToDelete.forEach(fileName => {
      formData.append('fileNamesToDelete', fileName)
    })
    finding.findingEvidences = [];
    finding.fileNamesToDelete = [];
    for (var prop in finding){
      formData.append(prop, finding[prop]);
    }

    return this._httpClient.post<Finding>(this.findingUpdateApprovedUrl+"updateapprovedfinding", formData);
  }

  reassignFindingStep(findingReassignmentsHistory:FindingReassignmentsHistory){
    return this._httpClient.post<FindingReassignmentsHistory>(this.findingReassignUrl + "requestreassign", findingReassignmentsHistory, httpOptions);
  }

  getLastReassignment(id:number){
    return this._httpClient.get(this.findingReassignUrl + "getlast/"+id);
  }

  approveOrRejectReassignment(reassignment: any, id_user:number){
    return this._httpClient.post<any>(this.findingReassignUrl + "approveorrejectreassignment/"+id_user ,reassignment,httpOptions)
  }

  rejectFindingStep(finding){
    return this._httpClient.post(this.findingApproveUrl+"ApproveFinding", finding, httpOptions);
  }

  add(finding) : Observable<Finding> {
    debugger
    var url = "http://localhost:7001/api/file"
    const formData = new FormData();

    var UploadFileCommand = {
      findingEvidences: finding.findingEvidences[0],
      OutputFileName: "prueba"
    }

    // finding.findingEvidences.forEach(file => {
    //   // formData.append('findingEvidences[]', file, file.name);
    //   formData.append('findingEvidences', file, file.name);
    // });

    formData.append('findingEvidences', UploadFileCommand.findingEvidences, UploadFileCommand.findingEvidences.name)
    finding.findingEvidences = [];
    UploadFileCommand.findingEvidences = null;
    // for (var prop in finding){
    //   formData.append(prop, finding[prop]);
    // }
    
    for (var prop in UploadFileCommand){
      formData.append(prop, UploadFileCommand[prop]);
    }


    return this._httpClient.post<Finding>(url, formData);
  }

  update(finding) {
    return this._httpClient.put(this.findingUrl+ "update", finding, httpOptions);
  }
}


