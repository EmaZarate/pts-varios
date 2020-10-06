import { Component, OnInit, OnDestroy } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { AuthService } from 'ClientApp/app/core/services/auth.service';
import { Subject } from 'rxjs';
import {FishboneService} from '../../../Fishbone.service';
import * as PERMISSIONS from '../../../../../core/permissions/index';

@Component({
  selector: 'app-read-fishbone-categories',
  templateUrl: './read-fishbone-categories.component.html',
  styleUrls: ['./read-fishbone-categories.component.css']
})

export class ReadFishboneCategoriesComponent implements OnInit, OnDestroy {


  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  private ngUnsibscribe: Subject<void> = new Subject<void>();
  fishBone = [];

  canSwitch;
  canEdit;
  canAdd;
  canDeactivate;

  constructor(
    private _toastrManager: ToastrManager,
    private _fishBoneService: FishboneService,
    private _authService: AuthService) { }

  ngOnInit() {
    this.loadSpineFish();
    this.setPermissions();
  }

  loadSpineFish()
  {
    this.blockUI.start();
    this._fishBoneService.getAll()
    .takeUntil(this.ngUnsibscribe)
    .subscribe((res)=>{
      this.fishBone = res;  
      this.blockUI.stop();    
    })
  }
 
  updateSpineActive(spine){
    this.blockUI.start();
    spine.active = !spine.active;
    this._fishBoneService.update(spine)
    .takeUntil(this.ngUnsubscribe)  
    .subscribe((res) =>{
      this._toastrManager.successToastr('La espina de pescado se ha actualizado correctamente','Éxito!');
      this.loadSpineFish();
      this.blockUI.stop();
    },
    error =>{
      spine.active = !spine.active
    })
  }

  setPermissions(){
    this.canSwitch = this._canSwitch();
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canDeactivate = this._canDeactivate();
  }

  private _canSwitch(){
    return this._authService.hasClaim(PERMISSIONS.FISHBONE_CATEGORY.DEACTIVATE) 
            && this._authService.hasClaim(PERMISSIONS.FISHBONE_CATEGORY.ACTIVATE)
  }

  private _canDeactivate(){
    return this._authService.hasClaim(PERMISSIONS.FISHBONE_CATEGORY.DEACTIVATE);
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.FISHBONE_CATEGORY.EDIT) 
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.FISHBONE_CATEGORY.ADD);
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
