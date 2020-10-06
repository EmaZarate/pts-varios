import { Component, OnInit, OnDestroy } from '@angular/core';
import { ToastrManager } from 'ng6-toastr-notifications';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { TaskStateService } from '../../state-task.service';
import { AuthService } from 'ClientApp/app/core/services/auth.service';
import { Subject } from 'rxjs';
import * as PERMISSIONS from '../../../../core/permissions/index';


@Component({
  selector: 'app-state-task',
  templateUrl: './state-task.component.html',
  styleUrls: ['./state-task.component.css']
})
export class StateTaskComponent implements OnInit, OnDestroy {
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  taskState = [];

  canSwitch;
  canEdit;
  canAdd;
  canDeactivate;

  constructor(private _toastrManager: ToastrManager,
              private _TaskStateService: TaskStateService,
              private _authService: AuthService ) {   }

  ngOnInit() {
    this.loadStatelist();
    this.setPermissions();
  }

  loadStatelist(){
 
    this.blockUI.start();
    this._TaskStateService.getAll()
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res)=>{
      this.taskState = res;
      this.blockUI.stop();
      console.log(res);
    })
  }

  updateStateActive(state){
    this.blockUI.start();
    state.active = !state.active;
    this._TaskStateService.update(state)
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) =>{
      this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!');
      this.loadStatelist();
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
    return this._authService.hasClaim(PERMISSIONS.TASK_STATE.DEACTIVATE)
            && this._authService.hasClaim(PERMISSIONS.TASK_STATE.ACTIVATE);
  }

  private _canDeactivate(){
    return this._authService.hasClaim(PERMISSIONS.TASK_STATE.DEACTIVATE);
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.TASK_STATE.EDIT);
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.TASK_STATE.ADD);
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
