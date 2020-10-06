import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControlName } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { FindingsStatesService } from '../../../findings-states.service';

import { FindingsStates } from '../../../models/FindingsStates';
import { Subject } from 'rxjs';
import { AuthService } from 'ClientApp/app/core/services/auth.service';
import * as PERMISSIONS from "../../../../../core/permissions/index";

@Component({
  selector: 'app-findings-states-create-edit',
  templateUrl: './create-edit.component.html',
  styleUrls: ['./create-edit.component.css']
})
export class CreateEditComponent implements OnInit, OnDestroy {
  color: string;

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  canEditLessColor = false;
  findingsStatesForm: FormGroup;

  _findingState = new FindingsStates();

  get code() { return this.findingsStatesForm.get('code') }
  get name() { return this.findingsStatesForm.get('name') }
  get colour() { return this.findingsStatesForm.get('colour') }
  get active() { return this.findingsStatesForm.get('active') }

  isCreate: boolean;
  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private fb: FormBuilder,
    private _findingStateService: FindingsStatesService,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.findingsStatesForm = this.modelCreate();
    this.color = this.findingsStatesForm.get('colour').value;

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
        .subscribe((res)=> {
          this._findingStateService.get(res.id)
                .takeUntil(this.ngUnsubscribe)
                .subscribe((res: FindingsStates)=>{
                  this._findingState = res;
                  this.code.patchValue(this._findingState.code);
                  this.name.patchValue(this._findingState.name);
                  this.colour.patchValue(this._findingState.colour);
                  this.active.patchValue(this._findingState.active);
                  this.blockUI.stop();
                })
        });
      }
    });
    this.setPermissions();
  }

  modelCreate(){
    return this.fb.group({
      code: ['', Validators.required],
      name: ['', Validators.required],
      colour: ['', Validators.required],
      active: [true, Validators.required]
    });
  }

  onSubmit(){
    this.blockUI.start();
    if(this.findingsStatesForm.valid){
      //console.log(this.findingsStatesForm);
      this._findingState.code = this.code.value;
      this._findingState.name = this.name.value;
      this._findingState.colour = this.colour.value;
      this._findingState.active = this.active.value;
      if(this.isCreate){
        this._findingStateService.add(this._findingState)
         .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            //success
            ////console.log(res);
            this._toastrManager.successToastr('El estado se ha creado correctamente', 'Éxito!')
            this._router.navigate(['quality/finding/config/states']);
            this.blockUI.stop();
          })
      }
      else{
        this._findingStateService.update(this._findingState)
         .takeUntil(this.ngUnsubscribe)
          .subscribe((res) =>{
            //success
            ////console.log(res);
            this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!')
            this._router.navigate(['quality/finding/config/states']);
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
