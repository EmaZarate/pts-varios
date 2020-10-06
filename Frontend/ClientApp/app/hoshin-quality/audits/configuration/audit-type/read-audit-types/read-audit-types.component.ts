import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import * as PERMISSIONS from '../../../../../core/permissions/index';

import { AuthService } from 'ClientApp/app/core/services/auth.service';
import { AuditTypesService } from '../../../audit-types.service';

@Component({
  selector: 'app-read-audit-types',
  templateUrl: './read-audit-types.component.html',
  styleUrls: ['./read-audit-types.component.css']
})
export class ReadAuditTypesComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  auditTypes = [];

  canSwitch;
  canEdit;
  canAdd;

  constructor(
    private _toastrManager: ToastrManager,
    private _authService: AuthService,
    private _auditTypeService: AuditTypesService
  ) { }

  ngOnInit() {
    this.loadAuditTypesList();
    this.setPermissions();
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  loadAuditTypesList(){
    this.blockUI.start();
    this._auditTypeService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any[]) => {
        this.auditTypes = res;
        this.blockUI.stop();
      })
  }

  setPermissions(){
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canSwitch = this._canSwitch();
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.AUDITTYPES.ADD);
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.AUDITTYPES.EDIT);
  }

  private _canSwitch(){
    return this._authService.hasClaim(PERMISSIONS.AUDITTYPES.ACTIVATE) && this._authService.hasClaim(PERMISSIONS.AUDITTYPES.DEACTIVATE);
  }

  updateAuditTypeActive(auditType){
    this.blockUI.start();
    auditType.active = !auditType.active;
    this._auditTypeService.update(auditType)
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) => {
      this._toastrManager.successToastr('El tipo de auditoría se ha actualizado correctamente', 'Éxito!');
      this.blockUI.stop();
      this.loadAuditTypesList();
    })
  }

}
