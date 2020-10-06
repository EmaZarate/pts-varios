import { Component, OnInit, Injector, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { PlantsService } from '../../../core/services/plants.service';

import { Plant } from '../../models/Plant';

import * as PERMISSIONS from '../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-read-plants',
  templateUrl: './read-plants.component.html',
  styleUrls: ['./read-plants.component.css']
})
export class ReadPlantsComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  plants: Plant[] = [];

  
  canSwitch;
  canEdit;
  canAdd;

  constructor(
    private _plantsService: PlantsService,
    private _toastrManager: ToastrManager,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.loadPlantsList();
    this.setPermissions();
  }

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  updatePlantState(plant: Plant) {
    this.blockUI.start();
    plant.active = !plant.active
    this._plantsService.update(plant)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(() => {
        this._toastrManager.successToastr('La planta se ha actualizado correctamente', 'Éxito!');
        this.loadPlantsList();
        this.blockUI.stop();
      });
  }

  loadPlantsList(){
    
    this.blockUI.start();
    this._plantsService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: Plant[]) => {
        
        this.plants = res;
        this.blockUI.stop();
      });
  }

  setPermissions(){
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canSwitch = this._canSwitch();
  }

  private _canSwitch(){
    return this._authService.hasClaim(PERMISSIONS.PLANT.DEACTIVATE_PLANT) && this._authService.hasClaim(PERMISSIONS.PLANT.ACTIVATE_PLANT)
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.PLANT.EDIT_PLANT);
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.PLANT.ADD_PLANT);
  }
    

}
