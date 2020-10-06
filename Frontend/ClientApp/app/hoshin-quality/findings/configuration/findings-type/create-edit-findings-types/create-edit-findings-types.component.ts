import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CollectionViewer } from '@angular/cdk/collections';
import { DataSource } from '@angular/cdk/table';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { ParametrizationCriteriaService } from '../../../parametrization-criteria.service';
import { FindingsTypeService } from '../../../../../core/services/findings-type.service';

import * as DATA_TYPES from '../../../models/DataTypes';
import { FindingType } from '../../../models/FindingType';

import { deleteFromArrayByProperty } from 'ClientApp/app/shared/util/arrays/delete-from-array-by-property.function';
declare var $:any;

@Component({
  selector: 'app-create-edit-findings-types',
  templateUrl: './create-edit-findings-types.component.html',
  styleUrls: ['./create-edit-findings-types.component.css']
})
export class CreateEditFindingsTypesComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsuscribe: Subject<void> = new Subject<void>();
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  datatypes = DATA_TYPES.DataTypes;

  allCriterias = [];
  criterios = [];

  findingTypesForm: FormGroup;
  paramCriteriasForm: FormGroup;

  selectedDataType;
  criterioSelectedBefore: any;

  _findingType : FindingType;

  

  get name() { return this.findingTypesForm.get('name'); }
  get code() { return this.findingTypesForm.get('code'); }
  get active() { return this.findingTypesForm.get('active'); }

  get criteriosSelected() { return this.paramCriteriasForm.get('criterias'); }
  get value() { return this.paramCriteriasForm.get('value'); }

  isCreate: boolean;

  dataSource: ParametrizationCriteriaDataSource;
  displayedColumns: string[] = ['Id', 'Name', 'Value', 'buttons'];
  constructor(
    private fb: FormBuilder,
    private _findingsTypeService: FindingsTypeService,
    private _parametrizationCriteriaService : ParametrizationCriteriaService,
    private _toastrManager: ToastrManager,
    private _route : ActivatedRoute,
    private _router: Router
  ) { }

  ngOnInit() {
    this._findingsTypeService.clearParamArray();
    this.blockUI.start();
    this.findingTypesForm = this.modelFindingTypesCreate();
    this.paramCriteriasForm = this.modelParamCritSelectedCreate();

    this.dataSource = new ParametrizationCriteriaDataSource(this._findingsTypeService);
    this.dataSource.loadSolicitudes();
    this._parametrizationCriteriaService.getAll()
      .takeUntil(this.ngUnsuscribe)
      .subscribe((res) => {
        this.criterios = res.filter(x => true);
        this.allCriterias = res.filter(x => true);


    if(this._route.snapshot.params.id != null){
      this.isCreate = false;
      this._findingsTypeService.get(this._route.snapshot.params.id)
        .takeUntil(this.ngUnsuscribe)
        .subscribe((res : any) => {
          debugger
          this._findingType = res;
          this.name.patchValue(this._findingType.name);
          this.code.patchValue(this._findingType.code);
          this.active.patchValue(this._findingType.active);


          this._findingType.parametrizations.forEach((el: any) =>{

            let critSel = this.criterios.find(e => e.id == el.id);
            let index = this.criterios.indexOf(critSel);
            this.criterios.splice(index, 1);
            let crit = {id: el.id ,name: critSel.name, value:el.value };
            this._findingsTypeService.addParametrizationCriteria(crit);


          });
          this.dataSource.loadSolicitudes();
          this.paramCriteriasForm.reset();
          this.blockUI.stop();
        });

    }
    else{
      this._findingType = new FindingType();
      this.isCreate = true;
      this.blockUI.stop();
    }
  });
  }

  modelFindingTypesCreate(){
    return this.fb.group({
      name: ['', Validators.required],
      code:['', Validators.required],
      active: false
    });
  }

  modelParamCritSelectedCreate(){
    return this.fb.group({
      criterias: [''],
      value:['']
    });
  }

  changeSelection(ev){
    let val = this.criterios.find(e => e.id == ev);
    if(val == null){
      this.selectedDataType = null;
    }
    this.selectedDataType = val.dataType;
  }

  addEditParamOnSubmit() {
    if(this.paramCriteriasForm.value.criterias == null) {
      this._toastrManager.errorToastr("Seleccione un criterio para agregar", "Se produjo un error", {position: 'bottom-right'});
      return;
    } 

    let $btnAddEdit = $('#btnAddEditParametrizationCriteria');
    
    if ($btnAddEdit.hasClass('add')) {
      this.addCriteria();
    }
    else if ($btnAddEdit.hasClass('edit')) {
      this.acceptEditParam();
    }
  }

  addCriteria(){
    let critSel = this.criterios.find(e => e.id == this.criteriosSelected.value);
    let index = this.criterios.indexOf(critSel);
    this.criterios.splice(index, 1);
    let crit = {id: this.criteriosSelected.value,name: critSel.name, value:this.value.value };
    this._findingsTypeService.addParametrizationCriteria(crit);
    this.dataSource.loadSolicitudes();
    this.paramCriteriasForm.reset();
  }

  acceptEditParam() {
    let param = this.allCriterias.find(e=> e.id == this.criteriosSelected.value);

    if (this.criteriosSelected.value == this.criterioSelectedBefore) {
      let critSel = this._findingsTypeService.paramCrit.find(e => e.id == this.criteriosSelected.value);
      critSel.value = this.value.value;
    }
    else {
      let param2 = this._findingsTypeService.paramCrit.find(e=> e.id == this.criterioSelectedBefore);
      param2.id = this.criteriosSelected.value;
      param2.name = param.name;
      param2.value = this.value.value;
    }
    
    deleteFromArrayByProperty(this.criterios, 'id', this.criteriosSelected.value);
    this.dataSource.loadSolicitudes();
    this.paramCriteriasForm.reset();
    this.addEditParametrizationCriteria_setMode('add');
  }

  cancelEditParam() {
    this.addEditParametrizationCriteria_setMode('add');
    deleteFromArrayByProperty(this.criterios, 'id', this.criteriosSelected.value);
    this.paramCriteriasForm.reset();
  }

  editParam(id, value) {
    this.addEditParametrizationCriteria_setMode('edit');
    deleteFromArrayByProperty(this.criterios, 'id', this.criterioSelectedBefore);
    
    let param = this.allCriterias.find(e=> e.id == id);
    this.criterios.push(param);
    this.changeSelection(id);

    this.criteriosSelected.patchValue(id);
    this.value.patchValue(value);
    this.criterioSelectedBefore = id;
  }

  deleteParam(id){
    this._findingsTypeService.removeParametrizationCriteria(id);
    let p = this.allCriterias.find(e => e.id == id);
    this.criterios.push(p);
    this.dataSource.loadSolicitudes();
    
    if ($('#btnAddEditParametrizationCriteria').hasClass('edit')) {
      this.cancelEditParam();
    }
  }

  addEditParametrizationCriteria_setMode(mode: string) {

    let $btnAddEdit = $('#btnAddEditParametrizationCriteria');
    let $icon = $btnAddEdit.find('i.material-icons');
    let $btnCancelEdit = $('#btnCancelEditParametrizationCriteria');

    if (mode == 'add') {
      $btnAddEdit.addClass('add').removeClass('edit');
      $icon.html('add');
      $btnCancelEdit.hide();

    } else if (mode == 'edit') {
      $btnAddEdit.addClass('edit').removeClass('add');
      $icon.html('done');
      $btnCancelEdit.show();
    }
  }
  
  onSubmit(){
    if(this.findingTypesForm.invalid){
      return;
    }

    this.blockUI.start();
    this._findingType.name = this.name.value;
    this._findingType.code = this.code.value;
    this._findingType.active = this.active.value;

    if(this.isCreate){
      this._findingsTypeService.add(this._findingType)
        .takeUntil(this.ngUnsuscribe)
        .subscribe((res) => {
          this._toastrManager.successToastr('El tipo de hallazgo se ha creado correctamente', 'Éxito');
          this._router.navigate(['/quality/finding/config/types']);
          this.blockUI.stop();
        });
    }
    else{
      this._findingsTypeService.update(this._findingType)
        .takeUntil(this.ngUnsuscribe)
        .subscribe((res) => {
          this._toastrManager.successToastr('El tipo de hallazgo se ha actualizado correctamente', 'Éxito');
          this._router.navigate(['/quality/finding/config/types']);
          this.blockUI.stop();
        })
    }
  }

  ngOnDestroy(){
    this.ngUnsuscribe.next();
    this.ngUnsuscribe.complete();
  }
  
}

export class ParametrizationCriteriaDataSource extends DataSource<any> {
  
  
  private lessonsSubject = new BehaviorSubject<any[]>([]);
  private loadingSubject = new BehaviorSubject<boolean>(false);
  public loading$ = this.loadingSubject.asObservable();
  public length;
  constructor(
    private _findingsTypeService: FindingsTypeService
  ) {
    super();
    this.length = 0;
  }

  connect(collectionViewer: CollectionViewer): Observable<any[]> {
    return this.lessonsSubject.asObservable();
  }

  disconnect() {
    this.lessonsSubject.complete();
    this.loadingSubject.complete();
  }

  loadSolicitudes() {
    this.loadingSubject.next(true);
    this._findingsTypeService.getAllParametrizationCriteria()
      .subscribe((res) => {
        this.length = res.length;
        this.lessonsSubject.next(res);
      })
  }
}
