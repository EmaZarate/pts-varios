import { Component, OnInit, ElementRef, ViewChildren, OnDestroy } from '@angular/core';
import {StateTaskC} from '../../models/statetask';

import { FormGroup, FormControlName, FormBuilder, Validators } from '@angular/forms';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Router, ActivatedRoute } from '@angular/router';
import { TaskStateService } from '../../state-task.service';
import { Subject } from 'rxjs';
import * as PERMISSIONS from "../../../../core/permissions/index";
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-task-state-new-edit',
  templateUrl: './task-state-new-edit.component.html',
  styleUrls: ['./task-state-new-edit.component.css']
})
export class TaskStateNewEditComponent implements OnInit, OnDestroy {
  colour: string;

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  taskStateForm: FormGroup;
  _taskState = new StateTaskC();

  get code(){ return this.taskStateForm.get('code'); }
  get name(){ return this.taskStateForm.get('name'); }
  get color(){return this.taskStateForm.get('color'); }
  get active(){ return this.taskStateForm.get('active'); }

  isCreate: boolean;

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private fb: FormBuilder,
    private  _TaskStateService: TaskStateService,
    private _authService: AuthService

  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.taskStateForm = this.modelCreate();
    this.colour = this.taskStateForm.get('color').value;

    this._route.data
    .takeUntil(this.ngUnsubscribe)
    .subscribe((data) => {

      if (data.typeForm === 'new') {
        this.isCreate = true;
        this.blockUI.stop();
      } else{
        this.isCreate = false;
        this._route.params
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this._TaskStateService.get(res.id)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res: StateTaskC) => {
            this._taskState = res;
            this.code.patchValue(this._taskState.code);
            this.color.patchValue(this._taskState.color);
            this.name.patchValue(this._taskState.name);
            this.active.patchValue(this._taskState.active);
            this.blockUI.stop();
          });
        });
      }
    });
    this.setPermissions();
  }

  modelCreate() {
    return this.fb.group({
      code: ['', Validators.required],
      name: ['', Validators.required],
      color: ['', Validators.required],
      active: [true]
    });
  }

  onSubmit(){

    this.blockUI.start();
    if (this.taskStateForm.valid){
      this._taskState.code = this.code.value;
      this._taskState.active = this.active.value;
      this._taskState.color = this.color.value;
      this._taskState.name = this.name.value;

      if (this.isCreate){
        this._taskState.active = true;
        this._TaskStateService.add(this._taskState)
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this._toastrManager.successToastr('El estado se ha creado correctamente', 'Exito!');
          this._router.navigate(['/quality/tasks/config/task-states']);
          this.blockUI.stop();
        });
      } else {
          this._TaskStateService.update(this._taskState)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!');
            this._router.navigate(['/quality/tasks/config/task-states']);
            this.blockUI.stop();
          });
      }
    } else {this.blockUI.stop(); }
  }
  setPermissions() {
    if(!this._canEdit()){
      this.name.disable();
      this.active.disable();
      this.code.disable();
    }
  }
  private _canEdit() {
    return this._authService.hasClaim(PERMISSIONS.ROLE.ADD_ROLE);
  }
  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
