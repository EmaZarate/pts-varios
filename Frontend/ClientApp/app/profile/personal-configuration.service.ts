import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { of } from 'rxjs';
import { environment } from './../../environments/environment';

var alerts =
//  [{
//   typeAlert:"Hallazgo",
//   alerts:[
//     { "alertID": 1, "description": "Alerta hallazgo vencido", "alertType": "Hallazgo" }, 
//     { "alertID": 2, "description": "Alerta hallazgo próximo a vencer", "alertType": "Hallazgo" }
//   ]
// },
// {
//   typeAlert:"Auditoria",
//   alerts:[
//     { "alertID": 3, "description": "Alert auditoria 1", "alertType": "Auditoria" },
//     { "alertID": 4, "description": "Alert auditoria 2", "alertType": "Auditoria" }
//   ]
// }
// ]

{
  "Hallazgo":
    [
      { "alertID": 1, "description": "Alerta hallazgo vencido", "alertType": "Hallazgo" },
      { "alertID": 2, "description": "Alerta hallazgo próximo a vencer", "alertType": "Hallazgo"}
    ],
  "Auditoria":
    [
      { "alertID": 3, "description": "Alert auditoria 1", "alertType": "Auditoria"},
      { "alertID": 4, "description": "Alert auditoria 2", "alertType": "Auditoria" }
    ],
  "Generico": [{ "alertID": 5, "description": "Alerta Generica", "alertType": "Generico" }]
}

var alertUser = 
[
  {
      "alertUsersID": 1,
      "alertID": 1,
      "generateAlert": true,
      "description": "Alerta hallazgo vencido",
      "alertType": "Hallazgo"
  },
  {
      "alertUsersID": 2,
      "alertID": 2,
      "generateAlert": false,
      "description": "Alerta hallazgo próximo a vencer",
      "alertType": "Hallazgo"
  }
]

//   var alert = [
//   {
//     ID:1,
//     Description: "Alerta de halalzgo 1",
//     AlertType:"Hallazgo"
//   },
//   {
//     ID:2,
//     Description: "Alerta de hallazgo 2",
//     AlertType:"Hallazgo"
//   },
// ]

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json'})
};

@Injectable({
  providedIn: 'root'
})
export class PersonalConfigurationService {

  constructor(private _httpClient: HttpClient) { }

  private alertUrl = environment.BASE_URL + "/api/alert/";
  private alertUserUrl = environment.BASE_URL + "/api/alertUser/";

  alerts(){
    return this._httpClient.get(this.alertUrl+"get", httpOptions);
    
  }

  alertsUser(userId){
    return this._httpClient.get(this.alertUserUrl + "get/" + userId, httpOptions);    
  }

  saveAlerts(alerts) {
    return this._httpClient.post(this.alertUserUrl + "insertOrUpdate/", alerts, httpOptions); 
  }

}
