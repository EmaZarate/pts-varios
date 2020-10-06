import { Component, OnInit, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';
import { AspectStateService } from "../../../aspect-state.service";
import { ToastrManager } from 'ng6-toastr-notifications';
import { AuthService } from 'ClientApp/app/core/services/auth.service';
import * as PERMISSIONS from '../../../../../core/permissions/index';

@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
  styleUrls: ['./read.component.css']
})
export class ReadComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsibscribe: Subject<void> = new Subject<void>();
  aspectsStates;
    
  canSwitch;
  canEdit;
  canAdd;
  canDeactivate;

  constructor(
    private _aspectStateService: AspectStateService,
    private _toastrManager: ToastrManager,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.loadStatesList();
    this.setPermissions();
  }

  ngOnDestroy(){
    this.ngUnsibscribe.next();
    this.ngUnsibscribe.complete();
  }

  updateStateActive(state){
    this.blockUI.start();
    state.active = !state.active;
    this._aspectStateService.update(state)
    .takeUntil(this.ngUnsibscribe)
    .subscribe((res) =>{
      this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!');
      this.loadStatesList();
      this.blockUI.stop();
    });
  }

  loadStatesList(){
    this.blockUI.start();
    this._aspectStateService.getAll()
    .takeUntil(this.ngUnsibscribe)
    .subscribe((res) => {
      this.aspectsStates = res;
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
    return this._authService.hasClaim(PERMISSIONS.ASPECT_STATE.DEACTIVATE) 
            && this._authService.hasClaim(PERMISSIONS.ASPECT_STATE.ACTIVATE)
  }

  private _canDeactivate(){
    return this._authService.hasClaim(PERMISSIONS.ASPECT_STATE.DEACTIVATE);
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.ASPECT_STATE.EDIT) 
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.ASPECT_STATE.ADD);
  }

}
