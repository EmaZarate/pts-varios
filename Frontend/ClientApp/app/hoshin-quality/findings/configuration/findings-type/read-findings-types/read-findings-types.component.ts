import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { FindingsTypeService } from '../../../../../core/services/findings-type.service';

import * as PERMISSIONS from '../../../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';


@Component({
  selector: 'app-read-findings-types',
  templateUrl: './read-findings-types.component.html',
  styleUrls: ['./read-findings-types.component.css']
})
export class ReadFindingsTypesComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  findingTypes = [];

  canSwitch;
  canEdit;
  canAdd;
  canDeactivate;

  constructor(
    private _findingTypesService : FindingsTypeService,
    private _toastrManager: ToastrManager,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.loadFindingTypesList();
    this.setPermissions();
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  updateFindingTypeActive(findingType){
    this.blockUI.start();
    findingType.active = !findingType.active;
    this._findingTypesService.update(findingType)
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) =>{
      this._toastrManager.successToastr('El tipo de hallazgo se ha actualizado correctamente', 'Éxito!');
      this.blockUI.stop();
      this.loadFindingTypesList();
    });
  }

  loadFindingTypesList(){
    this.blockUI.start();
    this._findingTypesService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res : any[]) => {
        this.findingTypes = res;
        this.blockUI.stop();
      });
  }

  delete(id){
    this.blockUI.start();
    this._findingTypesService.delete(id)
     .takeUntil(this.ngUnsubscribe)
      .subscribe(() => {
        this._toastrManager.successToastr('El tipo de hallazgo se ha actualizado correctamente', 'Éxito!');
        this.findingTypes[this.findingTypes.findIndex(x => x.id == id)].active = false;
        this.blockUI.stop();
      })
  }

  setPermissions(){
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canSwitch = this._canSwitch();
    this.canDeactivate = this._canDeactivate();
  }

  private _canSwitch(){
    return this._authService.hasClaim(PERMISSIONS.FINDING_TYPES.DEACTIVATE) && this._authService.hasClaim(PERMISSIONS.FINDING_TYPES.ACTIVATE)
  }

  private _canDeactivate(){
    return this._authService.hasClaim(PERMISSIONS.FINDING_TYPES.DEACTIVATE);
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.FINDING_TYPES.EDIT_CONFIGURE_TYPES) 
            && this._authService.hasClaim(PERMISSIONS.FINDING_TYPES.EDIT)
            && this._authService.hasClaim(PERMISSIONS.FINDING_PARAMETRIZATION_CRITERIA.READ); //NEED THIS TO GET PARAMCRIT FOR EDITING TYPE
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.FINDING_TYPES.ADD);
  }

}
