import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';
import { ToastrManager } from 'ng6-toastr-notifications';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { CorrectiveActionStateService } from '../../../corrective-action-state.service';

import * as PERMISSIONS from '../../../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-read-state',
  templateUrl: './read-state.component.html',
  styleUrls: ['./read-state.component.css']
})
export class ReadStateComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  correctiveActionStates = [];
  canSwitch;
  canEdit;
  canAdd;
  canDeactivate;

  constructor(
    private _correctiveActionStateService: CorrectiveActionStateService,
    private _toastrManager: ToastrManager,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.loadStatesList();
    this.setPermissions();
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  loadStatesList(){
    this.blockUI.start();
    this._correctiveActionStateService.getAll()
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) => {
      this.correctiveActionStates = res;
      this.blockUI.stop();
    })
  }

  setPermissions(){
    this.canSwitch = this._canSwitch();
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canDeactivate = this._canDeactivate();
  }

  private _canSwitch(){
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION_STATE.DEACTIVATE) 
            && this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION_STATE.ACTIVATE)
  }

  private _canDeactivate(){
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION_STATE.DEACTIVATE);
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION_STATE.EDIT) 
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.CORRECTIVE_ACTION_STATE.ADD);
  }

  updateStateActive(state){
    this.blockUI.start();
    state.active = !state.active;
    this._correctiveActionStateService.update(state)
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) => {
      this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!');
      this.loadStatesList();
      this.blockUI.stop();
    })
  }

}
