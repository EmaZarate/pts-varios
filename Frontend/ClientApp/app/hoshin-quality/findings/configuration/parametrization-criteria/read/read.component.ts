import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';

import { ParametrizationCriteriaService } from '../../../parametrization-criteria.service';

import * as DATA_TYPES from '../../../models/DataTypes';

import * as PERMISSIONS from '../../../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-read',
  templateUrl: './read.component.html',
  styleUrls: ['./read.component.css']
})
export class ReadComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  parametrizationCriterias = [];
  
  canEdit;
  canAdd;
  canDeactivate;

  datatypes = DATA_TYPES.DataTypes;

  constructor(
    private _parametrizationCriteriaService: ParametrizationCriteriaService,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.setPermissions();
    this._parametrizationCriteriaService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.parametrizationCriterias = res;
        //console.log(this.datatypes)
        this.blockUI.stop();
      })
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  delete(id){
    this._parametrizationCriteriaService.remove(id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(() =>{
      })
  }

  setPermissions(){
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canDeactivate = this._canDeactivate();
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
