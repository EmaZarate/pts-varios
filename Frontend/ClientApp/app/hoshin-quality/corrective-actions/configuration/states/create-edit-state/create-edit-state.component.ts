import { Component, OnInit, ElementRef, ViewChildren, OnDestroy } from '@angular/core';
import { CorrectiveActionState } from '../../../models/CorrectiveActionState';
import { FormGroup, FormControlName, FormBuilder, Validators } from '@angular/forms';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Router, ActivatedRoute } from '@angular/router';
import { CorrectiveActionStateService } from '../../../corrective-action-state.service';
import { Subject } from 'rxjs';
import * as PERMISSIONS from "../../../../../core/permissions/index";
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-create-edit-state',
  templateUrl: './create-edit-state.component.html',
  styleUrls: ['./create-edit-state.component.css']
})
export class CreateEditStateComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  correctiveActionStateForm: FormGroup;

  _correctiveActionState = new CorrectiveActionState();

  get code() { return this.correctiveActionStateForm.get('code') }
  get name() { return this.correctiveActionStateForm.get('name') }
  get color() { return this.correctiveActionStateForm.get('color') }
  get active() { return this.correctiveActionStateForm.get('active') }

  isCreate: boolean;

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private fb: FormBuilder,
    private _correctiveActionStateService: CorrectiveActionStateService,
    private _authService: AuthService

  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.correctiveActionStateForm = this.modelCreate();

    this._route.data
    .takeUntil(this.ngUnsubscribe)
    .subscribe((data) => {
      if(data.typeForm == 'new'){
        this.isCreate = true;
        this.blockUI.stop();
      }
      else{
        this.isCreate = false;
        this._route.params
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this._correctiveActionStateService.get(res.id)
                .takeUntil(this.ngUnsubscribe)
                .subscribe((res: CorrectiveActionState) => {
                  this._correctiveActionState = res;
                  this.code.patchValue(this._correctiveActionState.code);
                  this.name.patchValue(this._correctiveActionState.name);
                  this.color.patchValue(this._correctiveActionState.color);
                  this.active.patchValue(this._correctiveActionState.active);
                  this.blockUI.stop();
                })
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
    })
  }

  onSubmit() {
    this.blockUI.start();
    if(this.correctiveActionStateForm.valid){
      this._correctiveActionState.code = this.code.value;
      this._correctiveActionState.name = this.name.value;
      this._correctiveActionState.color = this.color.value;
      this._correctiveActionState.active = this.active.value;

      if(this.isCreate){
        this._correctiveActionState.active = true;
        this._correctiveActionStateService.add(this._correctiveActionState)
        .takeUntil(this.ngUnsubscribe)  
        .subscribe((res) => {
            this._toastrManager.successToastr('El estado se ha creado correctamente', 'Éxito!');
            this._router.navigate(['/quality/corrective-actions/config/states']);
            this.blockUI.stop();
          })
      }
      else{
        this._correctiveActionStateService.update(this._correctiveActionState)
        .takeUntil(this.ngUnsubscribe)  
        .subscribe((res) => {
            this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!');
            this._router.navigate(['/quality/corrective-actions/config/states']);
            this.blockUI.stop();
          })
      }
    }
  }
  setPermissions() {
    if(!this._canEdit()){
      this.name.disable();
      this.code.disable();
      this.active.disable();
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
