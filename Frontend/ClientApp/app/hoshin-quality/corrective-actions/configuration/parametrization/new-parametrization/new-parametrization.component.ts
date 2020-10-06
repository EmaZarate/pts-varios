import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControlName, Validators, FormBuilder } from '@angular/forms';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { ParametrizationService } from '../../../parametrization.service';

import { Parametrization } from '../../../models/Parametrization';
import { Subject } from 'rxjs';

//import * as DATA_TYPES from '../../../models/DataTypes';
//corrective actions no necesita tipo de dato    

@Component({
  selector: 'app-new-parametrization',
  templateUrl: './new-parametrization.component.html',
  styleUrls: ['./new-parametrization.component.css']
})
export class NewParametrizationComponent implements OnInit, OnDestroy {


  @BlockUI() blockUI: NgBlockUI;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  private ngUnsubscribe: Subject<void> = new Subject<void>();

  parametrizationForm: FormGroup;

  _param = new Parametrization();

  get name() { return this.parametrizationForm.get('name'); }
  get value() { return this.parametrizationForm.get('value'); }
  get code() { return this.parametrizationForm.get('code'); }

  isCreate: boolean;

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _parametrizationService: ParametrizationService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.parametrizationForm = this.modelCreate();

    this._route.data
    .takeUntil(this.ngUnsubscribe)
    .subscribe((data) => {
      if (data.typeForm == 'new') {
        this.isCreate = true;
        this.blockUI.stop();
      } else {
        this.isCreate = false;
        this._route.params
        .takeUntil(this.ngUnsubscribe)
        .subscribe((route) => {
          this.code.disable()
          this._parametrizationService.get(route.id)
            .takeUntil(this.ngUnsubscribe)
            .subscribe((res: Parametrization) => {
              this._param = res;
              this.name.patchValue(this._param.name);
              this.value.patchValue(this._param.value);
              this.code.patchValue(this._param.code);
              this.blockUI.stop();
            })
        });
      }
    });
  }

  modelCreate(){
    return this.fb.group({
      name: ['', Validators.required],
      value: ['', Validators.required],
      code: ['', Validators.required],
    });
  }

  onSubmit(){
    if(this.parametrizationForm.valid){

      this.blockUI.start();
      this._param.name = this.name.value;
      this._param.code = this.code.value;
      this._param.value = this.value.value;
      
      if(this.isCreate){
        this._parametrizationService.add(this._param)
        .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            this._toastrManager.successToastr('El criterio de parametrización se ha creado correctamente', 'Éxito');
            this._router.navigate(['/quality/corrective-actions/config/parametrization']);
            this.blockUI.stop();
          })
      }
      else{
        this._parametrizationService.update(this._param)
        .takeUntil(this.ngUnsubscribe)
          .subscribe((res) =>{
            this._toastrManager.successToastr('El criterio de parametrización se ha actualizado correctamente', 'Éxito');
            this._router.navigate(['/quality/corrective-actions/config/parametrization']);
            this.blockUI.stop();
          })
      }
    }
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
}
}