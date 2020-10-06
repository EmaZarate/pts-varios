import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/internal/Subject';
import { ToastrManager } from 'ng6-toastr-notifications';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { AuditStateService } from "../../../audit-state.service";

import * as PERMISSIONS from '../../../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-read-state',
  templateUrl: './read-state.component.html',
  styleUrls: ['./read-state.component.css']
})
export class ReadStateComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsibscribe: Subject<void> = new Subject<void>();
  auditStates = [];
  canSwitch;
  canEdit;
  canAdd;
  canDeactivate;

  constructor(
    private _auditStateService: AuditStateService,
    private _toastrManager: ToastrManager,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.loadStatesList();
    this.setPermissions();
  }

 
  loadStatesList(){
    this.blockUI.start();
    this._auditStateService.getAll()
    .takeUntil(this.ngUnsibscribe)
    .subscribe((res) => {      
      this.auditStates = res;
      this.blockUI.stop();
    })
  }

  updateStateActive(state){
    this.blockUI.start();
    state.active = !state.active;    
    this._auditStateService.update(state)
    .takeUntil(this.ngUnsibscribe)
    .subscribe((res) =>{
      this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!');
      this.loadStatesList();
      this.blockUI.stop();
    });
  }

  setPermissions(){
    this.canSwitch = this._canSwitch();
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canDeactivate = this._canDeactivate();
  }

  private _canSwitch(){
    return this._authService.hasClaim(PERMISSIONS.AUDIT_STATE.DEACTIVATE) 
            && this._authService.hasClaim(PERMISSIONS.AUDIT_STATE.ACTIVATE)
  }

  private _canDeactivate(){
    return this._authService.hasClaim(PERMISSIONS.AUDIT_STATE.DEACTIVATE);
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.ASPECT_STATE.EDIT) 
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.ASPECT_STATE.ADD);
  }

  ngOnDestroy(){
    this.ngUnsibscribe.next();
    this.ngUnsibscribe.complete();
  }

}
