import { Component, OnInit, Injector, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { SectorsService } from '../../../core/services/sectors.service';

import { Sector } from '../../models/Sector';

import * as PERMISSIONS from '../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-read-sectors',
  templateUrl: './read-sectors.component.html',
  styleUrls: ['./read-sectors.component.css']
})
export class ReadSectorsComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  sectors: Sector[] = [];

  canSwitch;
  canEdit;
  canAdd;

  constructor(
    private _sectorsService: SectorsService,
    private _toastrManager: ToastrManager,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.loadSectorsList();
    this.setPermissions();
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  updateSectorState(sector: Sector){
    this.blockUI.start();
    sector.active = !sector.active
    this._sectorsService.update(sector)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(() => {
        this._toastrManager.successToastr('El sector se ha actualizado correctamente', 'Éxito!');
        this.loadSectorsList();
        this.blockUI.stop();
    });
  }

  loadSectorsList(){
    
    this.blockUI.start();
    this._sectorsService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: Sector[]) => {
        
        this.sectors = res;
        this.blockUI.stop();
    });
  }

  setPermissions(){
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canSwitch = this._canSwitch();
  }

  private _canSwitch(){
    return this._authService.hasClaim(PERMISSIONS.SECTOR.DEACTIVATE_SECTOR) && this._authService.hasClaim(PERMISSIONS.SECTOR.ACTIVATE_SECTOR)
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.SECTOR.EDIT_SECTOR);
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.SECTOR.ADD_SECTOR);
  }

}
