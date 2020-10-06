import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControlName, Validators, FormBuilder } from '@angular/forms';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { ParametrizationCriteriaService } from '../../../parametrization-criteria.service';

import { ParametrizationCriteria } from '../../../models/ParametrizationCriteria';
import * as DATA_TYPES from '../../../models/DataTypes';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-create-edit',
  templateUrl: './create-edit.component.html',
  styleUrls: ['./create-edit.component.css']
})
export class CreateEditComponent implements OnInit, OnDestroy {

  datatypes = DATA_TYPES.DataTypes;

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  parametrizarionCriteriaForm: FormGroup;

  _paramCriteria = new ParametrizationCriteria();

  get name() { return this.parametrizarionCriteriaForm.get('name'); }
  get datatype() { return this.parametrizarionCriteriaForm.get('datatype'); }

  isCreate: boolean;
  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _parametrizationCriteriaService: ParametrizationCriteriaService
  ) { }

  // datatypes = [
  //   {value: '1', name: 'Entero'},
  //   {value: '2', name: 'Decimal'},
  //   {value: '3', name: 'Cadena'}
  // ];
  
  ngOnInit() {
    this.blockUI.start();
    this.parametrizarionCriteriaForm = this.modelCreate();

    this._route.data
    .takeUntil(this.ngUnsubscribe)
    .subscribe((data) => {
      if(data.typeForm == 'new'){
        this.isCreate = true;
        this.blockUI.stop();
      }else{
        this.isCreate = false;
        this._route.params
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res)=> {
          this._parametrizationCriteriaService.get(res.id)
               .takeUntil(this.ngUnsubscribe)
                .subscribe((res: ParametrizationCriteria)=>{
                  this._paramCriteria = res;
                  this.name.patchValue(this._paramCriteria.name);
                  this.datatype.patchValue(this._paramCriteria.dataType);
                  this.blockUI.stop();
                })
        });
      }
      
    });
  }

  ngAfterViewInit(){

  }
  modelCreate(){
    return this.fb.group({
      name: ['', Validators.required],
      datatype:['', Validators.required]
    });
  }

  onSubmit(){
    if(this.parametrizarionCriteriaForm.valid){

      this.blockUI.start();
      this._paramCriteria.name = this.name.value;
      this._paramCriteria.dataType = this.datatype.value;
      
      if(this.isCreate){
        this._parametrizationCriteriaService.add(this._paramCriteria)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            //console.log(res);
            this._toastrManager.successToastr('El criterio de parametrización se ha creado correctamente', 'Éxito');
            this._router.navigate(['/quality/finding/config/parametrization-criteria']);
            this.blockUI.stop();
          })
      }
      else{
        this._parametrizationCriteriaService.update(this._paramCriteria)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) =>{
            //console.log(res);
            this._toastrManager.successToastr('El criterio de parametrización se ha actualizado correctamente', 'Éxito');
            this._router.navigate(['/quality/finding/config/parametrization-criteria']);
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
