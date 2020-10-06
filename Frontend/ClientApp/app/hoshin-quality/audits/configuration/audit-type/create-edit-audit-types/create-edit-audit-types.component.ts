import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { AuditTypesService } from '../../../audit-types.service';

import { AuditType } from '../../../models/AuditTypes';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-create-edit-audit-types',
  templateUrl: './create-edit-audit-types.component.html',
  styleUrls: ['./create-edit-audit-types.component.css']
})
export class CreateEditAuditTypesComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  isCreate: boolean;

  auditTypesForm: FormGroup;

  _auditType = new AuditType;

  get code() { return this.auditTypesForm.get('code') }
  get name() { return this.auditTypesForm.get('name') }
  get active() { return this.auditTypesForm.get('active') }

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private fb: FormBuilder,
    private _auditTypesService: AuditTypesService
  ) { }

  ngOnInit() {
    
    this.blockUI.start();
    this.auditTypesForm = this.modelCreate();

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
          this._auditTypesService.get(res.id)
            .takeUntil(this.ngUnsubscribe)
            .subscribe((res: AuditType) => {
              this._auditType = res;
              this.code.patchValue(this._auditType.code);
              this.name.patchValue(this._auditType.name);
              this.active.patchValue(this._auditType.active);
              this.blockUI.stop();
            })
        })
      }
    })
  }

  modelCreate(){
    return this.fb.group({
      code: ['', Validators.required],
      name: ['', Validators.required],
      active: [true, Validators.required]
    })
  }

  onSubmit(){
    
    this.blockUI.start();
    if(this.auditTypesForm.valid){
      this._auditType.code = this.code.value;
      this._auditType.name = this.name.value;
      this._auditType.active = this.active.value;

      if(this.isCreate){
        this._auditTypesService.add(this._auditType)
        .takeUntil(this.ngUnsubscribe) 
        .subscribe((res) => {
            this._toastrManager.successToastr('El tipo de auditoría se ha creado correctamente', 'Éxito!');
            this._router.navigate(['/quality/audits/config/types']);
            this.blockUI.stop();
          })
      }
      else{
        this._auditTypesService.update(this._auditType)
        .takeUntil(this.ngUnsubscribe)  
        .subscribe((res) => {
            this._toastrManager.successToastr('El tipo de auditoría se ha actualizado correctamente', 'Éxito!');
            this._router.navigate(['/quality/audits/config/types']);
            this.blockUI.stop();
          })
      }
    }
    else{
      
      this.blockUI.stop();
    }
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
