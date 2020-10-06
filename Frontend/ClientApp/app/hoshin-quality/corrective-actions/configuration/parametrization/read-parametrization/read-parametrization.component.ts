import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';

import { ParametrizationService } from '../../../parametrization.service';

//import * as DATA_TYPES from '../../../models/DataTypes';

import * as PERMISSIONS from '../../../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-read-parametrization',
  templateUrl: './read-parametrization.component.html',
  styleUrls: ['./read-parametrization.component.css']
})
export class ReadParametrizationComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  parametrization = [];
  
  canEdit;
  canAdd;
  canDeactivate;

 // datatypes = DATA_TYPES.DataTypes;

  constructor(
    private _parametrizationService: ParametrizationService,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    //this.setPermissions();
    this._parametrizationService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.parametrization = res;
        //console.log(this.datatypes)
        this.blockUI.stop();
      })
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  delete(id){
    this._parametrizationService.remove(id)
      .subscribe(() =>{
      })
  }
}