import { Component, OnInit, OnDestroy } from '@angular/core';
import { PersonalConfigurationService } from "../../personal-configuration.service";
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { AuthService } from "../../../core/services/auth.service";
import { ToastrManager } from 'ng6-toastr-notifications';
import { debug } from 'util';
import { Subject } from 'rxjs';
import { UsersService } from 'ClientApp/app/core/services/users.service';



@Component({
  selector: 'app-personal-configuration',
  templateUrl: './personal-configuration.component.html',
  styleUrls: ['./personal-configuration.component.css']
})
export class PersonalConfigurationComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  alertsForType = new Array();
  alertsUser;
  userLogged
  rolesList: any;


  constructor(
    private _personalConfigurationService: PersonalConfigurationService,
    private _authService: AuthService,
    private _toastrManager: ToastrManager,
    private _userService: UsersService
  ) { }

  ngOnInit() {
    this.blockUI.start('Cargando Datos...')
    this.userLogged = this._authService.getUserLogged();
    this._userService.getRoles(this.userLogged.id)
      .subscribe(data => {
        this.rolesList = data;
        this.getAlerts();
      })

  }

  getAlerts() {
    this._personalConfigurationService.alerts()
      .takeUntil(this.ngUnsubscribe)
      .subscribe(alerts => {
        this.mapAlerts(alerts)
        this._personalConfigurationService.alertsUser(this.userLogged.id)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((alertsUser) => {
            this.alertsUser = alertsUser
            this.checkAlerts(this.alertsUser)
            this.scrollFromTop();
            this.blockUI.stop();

          })
      });
  }
  setAlertByRole() {
    const that = this;
    let res = this.searchAlert();
    if (res.alerts) {
      res.alerts.forEach(function (alert) {
        if (alert.description.toLowerCase() == "alerta prevencimientos ac") {
          alert.canSee = true;
        } else if (alert.description.toLowerCase() == "alerta vencimientos ac") {
          alert.canSee = true;
        };

      });
      if (that.searchRole("jefe de sector")) {
        res.alerts.forEach(function (alert) {
          if (((alert.description.toLowerCase() == "alerta vencimientos ac") ||
            (alert.description.toLowerCase() == "alertas generales ac jefe sector") ||
            (alert.description.toLowerCase() == "alertas prevencimientos ac jefe sector") ||
            (alert.description.toLowerCase() == "alertas vencimientos ac jefe sector"))) {
              alert.canSee = true;
          }
        });
      };
        if (that.searchRole("colaborador jefe sector")) {
          res.alerts.forEach(function (alert) {
            if ((alert.description.toLowerCase() == "alertas generales ac colaborador sector") ||
            (alert.description.toLowerCase() == "alertas prevencimientos ac colaborador sector") ||
            (alert.description.toLowerCase() == "alertas vencimientos ac colaborador sector")) {
              alert.canSee = true;
            }
          });
        };
    }
  }
  changeAlert() {
    console.log(this.alertsForType)
  }
  searchRole(role) {
    let result = false;
    if (this.rolesList) {
      this.rolesList.forEach(function (rol) {
        if (rol.toLowerCase() == role) {
          result = true;
        }
      });
    };
    return result;
  }
  searchAlert(): any {
    let result;
    if (this.alertsForType) {
      this.alertsForType.forEach(function (alert) {
        if (alert.typeAlert.toLowerCase() == "acción correctiva") {
          result = alert;
        }
      });
    }
    return result;
  }
  setOtherAlert(){
      if (this.alertsForType) {
        this.alertsForType.forEach(function (alert) {
          if (alert.typeAlert.toLowerCase() !== "acción correctiva") {
            alert.alerts.forEach(function(item){
              item.canSee = true;
            })
          }
        });
      }
    }
  mapAlerts(alerts) {
    let alertsAux = alerts
    let count = 0
    Object.entries(alertsAux).forEach(([key, value]) => {
      this.alertsForType[count] = {
        typeAlert: key,
        alerts: value,
        index: count
      };
      this.alertsForType[count].alerts.forEach(element => {
        element.GenerateAlert = false;
        element.alertUsersID = 0;
        element.UsersID = this.userLogged.id;
        element.canSee = false;
      });
      count = count + 1;
    });
    this.alertsForType = this.alertsForType.filter(x => x.typeAlert.toLowerCase() !== 'genérico');
    this.alertsForType = this.alertsForType.filter(x => x.typeAlert.toLowerCase() !== 'auditoría');
    this.setOtherAlert();
    this.setAlertByRole();
  }

  checkAlerts(alertsUser) {
    alertsUser.forEach(alertUser => {
      this.alertsForType.forEach(alerts => {
        alerts.alerts.forEach(alert => {
          if (alertUser.alertID == alert.alertID) {
            alert.alertUsersID = alertUser.alertUsersID
            alert.GenerateAlert = alertUser.generateAlert;
          }
        });
      });
    });
  }

  saveAlerts() {
    this.blockUI.start();
    let alertsDictionary;
    alertsDictionary = this.alertsForType.reduce(function (map, obj) {
      map[obj.typeAlert] = obj.alerts;
      return map
    }, {});
    this._personalConfigurationService.saveAlerts(alertsDictionary)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this._toastrManager.successToastr('Las alertas se han actualizado correctamente', 'Éxito');
        this.getAlerts();
      }
        , (error) => {
          this.blockUI.stop();
        });
  }

  scrollFromTop() {
    const mainDiv = document.getElementsByClassName('container-fluid')[0];
    mainDiv.scrollIntoView(true);
  }

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
