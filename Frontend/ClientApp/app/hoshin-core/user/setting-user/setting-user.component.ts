import { Component, OnInit } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Router } from '@angular/router';

@Component({
  selector: 'app-setting-user',
  templateUrl: './setting-user.component.html',
  styleUrls: ['./setting-user.component.css']
})
export class SettingUserComponent implements OnInit {

  @BlockUI() blockUI: NgBlockUI;

  alertSelected = [];
  allAlert = [];

  constructor(private _toastrManager: ToastrManager,
    private _router: Router) { }

  ngOnInit() {
    /*this.blockUI.start();

    this._alertService.getAll()
      .subscribe((res) => {
        this.allAlert = res;
        this.blockUI.stop();
      });*/
  }


  checkboxClicked(ev, alert,typeAlert) {
    if (ev.target.checked) {
      alert.typeAlert = typeAlert;
      if (alert.alertId != null) {
        alert.id = alert.alertId;
      }
      this.alertSelected.push(alert);
    }
    else {
      let indexAlertToDelete = this.alertSelected.findIndex(x => x.alertId == alert.alertId || x.id == alert.alertId);
      this.alertSelected.splice(indexAlertToDelete, 1);
    }    
  }

}
