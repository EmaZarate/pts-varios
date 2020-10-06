import { Component, OnInit, ElementRef, ViewChildren, OnDestroy } from '@angular/core';
import { AuditState } from '../../../models/AuditState';
import { FormGroup, FormControlName, FormBuilder, Validators } from '@angular/forms';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Router, ActivatedRoute } from '@angular/router';
import { AuditStateService } from "../../../audit-state.service";
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
  private ngUnsuscribe: Subject<void> = new Subject<void>();

  auditStateForm: FormGroup;

  _auditState = new AuditState();

  get code() { return this.auditStateForm.get('code') }
  get name() { return this.auditStateForm.get('name') }
  get color() { return this.auditStateForm.get('color') }
  get active() { return this.auditStateForm.get('active') }

  isCreate: boolean;

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private fb: FormBuilder,
    private _auditStateService: AuditStateService,
    private _authService: AuthService

  ) { }

  ngOnInit() {

    this.blockUI.start();
    this.auditStateForm = this.modelCreate();
    
    this._route.data
    .takeUntil(this.ngUnsuscribe)
    .subscribe((data) => {
      debugger
      if(data.typeForm == 'new'){
        this.isCreate = true;
        this.blockUI.stop();
      }
      else{
        this.isCreate = false;        
        this._route.params
        .takeUntil(this.ngUnsuscribe)
        .subscribe((res)=> {
          this._auditStateService.get(res.id)
                .takeUntil(this.ngUnsuscribe)
                .subscribe((res: AuditState)=>{
                  this._auditState = res;
                  this.code.patchValue(this._auditState.code);
                  this.name.patchValue(this._auditState.name);
                  this.color.patchValue(this._auditState.color);
                  this.active.patchValue(this._auditState.active)
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
      active:[false]
    });
  }

  onSubmit() {

    this.blockUI.start();
    if (this.auditStateForm.valid) {
      //console.log(this.findingsStatesForm);
      this._auditState.code = this.code.value;
      this._auditState.name = this.name.value;
      this._auditState.color = this.color.value;
      this._auditState.active = this.active.value;

      if (this.isCreate) {
        this._auditState.active = true
        this._auditStateService.add(this._auditState)
          .takeUntil(this.ngUnsuscribe)
          .subscribe((res) => {
            this._toastrManager.successToastr('El estado se ha creado correctamente', 'Éxito!')
            this._router.navigate(['/quality/audits/config/state']);
            this.blockUI.stop();
          })
      }
      else 
      {
          this._auditStateService.update(this._auditState)
          .takeUntil(this.ngUnsuscribe)
          .subscribe((res) => {
            this._toastrManager.successToastr('El estado se ha actualizado correctamente', 'Éxito!')
            this._router.navigate(['/quality/audits/config/state']);
            this.blockUI.stop();
          }, (err) => {console.log(err)});
          
      }
    }
    else
    {
        this.blockUI.stop();
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
    this.ngUnsuscribe.next();
    this.ngUnsuscribe.complete();
  }

}
