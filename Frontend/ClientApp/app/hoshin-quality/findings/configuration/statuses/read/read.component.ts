import { Component, OnInit, Injector, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { ToastrManager } from 'ng6-toastr-notifications';
import { BlockUI, NgBlockUI } from 'ng-block-ui';

import { FindingsStatesService } from '../../../findings-states.service';


import * as PERMISSIONS from '../../../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-states-read',
  templateUrl: './read.component.html',
  styleUrls: ['./read.component.css']
})
export class ReadComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  findingsStates = [];
    
  canSwitch;
  canEdit;
  canAdd;
  canDeactivate;

  constructor(
    private _findingsStatesService: FindingsStatesService,
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

  updateStateActive(state){
    this.blockUI.start();
    state.active = !state.active;
    
    this._findingsStatesService.update(state)
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) =>{
      this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!');
      this.loadStatesList();
      this.blockUI.stop();
    });
  }

  loadStatesList(){
    this.blockUI.start();
    this._findingsStatesService.getAll()
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) => {
      this.findingsStates = res;
      this.blockUI.stop();
      ////console.log(res);
    })
  }

  delete(id){
    this._findingsStatesService.remove(id)
     .takeUntil(this.ngUnsubscribe)
     .subscribe(() => {
      })
  }

  setPermissions(){
    this.canSwitch = this._canSwitch();
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canDeactivate = this._canDeactivate();
  }

  private _canSwitch(){
    return this._authService.hasClaim(PERMISSIONS.FINDING_STATES.DEACTIVATE) 
            && this._authService.hasClaim(PERMISSIONS.FINDING_STATES.ACTIVATE)
  }

  private _canDeactivate(){
    return this._authService.hasClaim(PERMISSIONS.FINDING_PARAMETRIZATION_CRITERIA.DEACTIVATE);
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.FINDING_PARAMETRIZATION_CRITERIA.EDIT) 
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.FINDING_PARAMETRIZATION_CRITERIA.ADD);
  }
}