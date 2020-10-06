import { Component, OnInit, Output, EventEmitter, Input, ViewChild, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormGroupDirective } from '@angular/forms';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { PlantsService } from '../../../../core/services/plants.service';
import { FindingsService } from '../../../../core/services/findings.service';
import { Task } from '../../models/Task';
import { TaskConfigService } from '../../task.service';
import Swal from 'sweetalert2/dist/sweetalert2';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { ToastrManager } from 'ng6-toastr-notifications';
import { MatPaginator, MatTableDataSource } from '@angular/material';
import { MatPaginatorIntl } from '@angular/material';
import { TaskStateCode } from '../../models/TaskStateCode';
import { CorrectiveAction } from '../../models/CorrectiveAction';
import { CorrectiveActionService } from '../../corrective-action.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-action-plan',
  templateUrl: './action-plan.component.html',
  styleUrls: ['./action-plan.component.css']
})

export class ActionPlanComponent implements OnInit, OnDestroy {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  @Output() outputSubmitDatesAndImpact = new EventEmitter<any>();
  @Output() outputSubmitImpact = new EventEmitter<any>();
  @Input() correctiveAction: CorrectiveAction;
  actionPlanForm: FormGroup;
  _task = new Task();
  dataSource = new MatTableDataSource();
  TaskStateCode = TaskStateCode;
  displayedColumns: string[] = ['Id', 'Description', 'User', 'Date', 'Evidence', 'buttons'];
  isFirstRequest: boolean = true;
  isAR: boolean;
  firstTimeSetMaxDate = true;
  maxDate;
  get taskID() { return this.actionPlanForm.get('taskID') }
  get taskStateID() { return this.actionPlanForm.get('taskStateID') }
  get description() { return this.actionPlanForm.get('description'); }
  get plantTreatment() { return this.actionPlanForm.get('plantTreatment'); }
  get sectorTreatment() { return this.actionPlanForm.get('sectorTreatment'); }
  get responsibleUser() { return this.actionPlanForm.get('responsibleUser'); }
  get implementationPlannedDate() { return this.actionPlanForm.get('implementationPlannedDate'); }
  get requireEvidence() { return this.actionPlanForm.get('requireEvidence'); }
  get impact() { return this.actionPlanForm.get('impact'); }
  get maxDateImplementation() { return this.actionPlanForm.get('maxDateImplementation'); }
  get maxDateEfficiencyEvaluation() { return this.actionPlanForm.get('maxDateEfficiencyEvaluation'); }

  constructor(
    private formBuilder: FormBuilder,
    private _plantsService: PlantsService,
    private _findingsService: FindingsService,
    private _taskService: TaskConfigService,
    private _toastrManager: ToastrManager
  ) { }

  isEditing = false;
  allPlants = [];
  sectorTreatments = [];
  responsibleUsers = [];

  ngOnInit() {
    this.isAR = navigator.languages[0] == "en" ? false : true;
    this.blockUI.start();
    this.actionPlanForm = this.modelCreate();
    this._plantsService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.allPlants = res;
        this.blockUI.stop();
      });
    this.maxDateImplementation.disable();
    this.getTasks();

    this.actionPlanForm.valueChanges
      .pipe(
        debounceTime(400),
        distinctUntilChanged(),
      )
      .takeUntil(this.ngUnsubscribe)
      .subscribe(this.emitEventDataChanges);

    this.impact.valueChanges.pipe(
      debounceTime(400),
      distinctUntilChanged(),
    )
      .takeUntil(this.ngUnsubscribe)
      .subscribe(this.emitEventDataChanges);

    this.impact.patchValue(this.correctiveAction.impact);
  }

  emitImpact = () => {
    if (!this.isFirstRequest) {
      this.outputSubmitImpact.emit(this.impact.value);
    }
    this.isFirstRequest = false;
  }

  emitEventDataChanges = () => {
    let efficiencyDate = this.maxDateEfficiencyEvaluation.value;
    let implementationDate = this.maxDateImplementation.value;
    try{
      efficiencyDate = new Date(this.maxDateEfficiencyEvaluation.value).toISOString();
      implementationDate = new Date(this.maxDateImplementation.value).toISOString();
    }catch{}
    
    if(this.maxDateImplementation.value){
      if(efficiencyDate >= implementationDate){
        let propertiesActionPlanForm = {
          isValid: (this.dataSource.data.length > 0 && this.impact.value != "" && this.impact.value != null),
          impact: this.impact.value,
          tasks: this.dataSource.data,
          maxDateImplementation: this.maxDateImplementation.value,
          maxDateEfficiencyEvaluation: this.maxDateEfficiencyEvaluation.value
        }
        this.outputSubmitImpact.emit(propertiesActionPlanForm);
      }else{
        this.maxDateEfficiencyEvaluation.patchValue(this.maxDate);
        this._toastrManager.errorToastr("La fecha de evaluacion debe ser mayor o igual a la fecha probable de implementacion total.", "Error");
      }
    }
    
    
  }

  modelCreate() {
    // this.maxDate = new Date();
    // this.maxDate.setDate(this.maxDate.getDate() + 1);
    let today = new Date();
    today.setDate(today.getDate() + 1);

    return this.formBuilder.group({
      taskID: [''],
      taskStateID: [''],
      description: ['', Validators.required],
      plantTreatment: ['', Validators.required],
      sectorTreatment: ['', Validators.required],
      responsibleUser: ['', Validators.required],
      implementationPlannedDate: [today, Validators.required],
      requireEvidence: [false, Validators.required],
      impact: [''],
      maxDateImplementation: [''],
      maxDateEfficiencyEvaluation: ['']
    });
  }

  changeSelectionTreatment(val) {
    let pl = this.allPlants.find(x => x.plantID == val);
    this.plantTreatment.patchValue(val);
    this.sectorTreatments = pl.sectors;
    this.sectorTreatment.enable();
  }

  changeSelectionSectorTreatment(id) {
    const that = this;
    let plantId = this.plantTreatment.value;
    this.responsibleUsers = [];
    this.blockUI.start();
    this._findingsService.getAllUsers(id, plantId)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any[]) => {
        this.responsibleUser.enable();
        res.forEach(function (user) {
          if (user.active) {
            that.responsibleUsers.push(user);
          }
        });
        let sector = this.sectorTreatments.find(x => x.id == id);
        this.sectorTreatment.patchValue(sector.id);
        this.blockUI.stop();
      });
  }

  getTasks() {
    this._taskService.GetAllByCorrectiveActionID(this.correctiveAction.correctiveActionID).subscribe((res) => {
      this.dataSource = new MatTableDataSource(res);
      this.dataSource.paginator = this.paginator;
      let dateArray = new Array()
      for (let index = 0; index < res.length; index++) {
        dateArray.push(res[index].implementationPlannedDate)
      }
      var sorted = dateArray.sort();
      
      this.maxDateImplementation.patchValue(sorted[sorted.length - 1]);
      if(sorted.length > 0){
        this.maxDate = sorted[sorted.length -1];
      }else if(this.maxDateImplementation.value){
        this.maxDate= this.maxDateImplementation.value;}
      // }else{
      //   this.maxDate = new Date();
      //   this.maxDate.setDate(this.maxDate.getDate() + 1);
      // }
      if (this.correctiveAction.maxDateEfficiencyEvaluation && this.correctiveAction.maxDateEfficiencyEvaluation < sorted[sorted.length -1]) {
        this.maxDateEfficiencyEvaluation.patchValue(sorted[sorted.length - 1]);
      }
      else {
        this.maxDateEfficiencyEvaluation.patchValue(this.correctiveAction.maxDateEfficiencyEvaluation);
      }
    })
  }

  editTask(id) {
    this.scrollTopFromEdit();
    this._taskService.Get(id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this.taskID.patchValue(res.taskID);
        this.taskStateID.patchValue(res.taskStateID)
        this.description.patchValue(res.description);
        this.implementationPlannedDate.patchValue(res.implementationPlannedDate);
        this.requireEvidence.patchValue(res.requireEvidence);
        this.changeSelectionTreatment(res.responsibleUser.plantID);
        this.changeSelectionSectorTreatment(res.responsibleUser.sectorID);
        this.responsibleUser.patchValue(res.responsibleUserID);
        this.isEditing = true;
      })
  }

  deleteTask(id) {
    Swal.fire({
      text: '¿Desea borrar esta tarea?',
      type: 'question',
      showCancelButton: true,
      confirmButtonText: 'Si',
      cancelButtonText: 'No ',
      focusCancel: true
    }).then((result) => {
      if (result.value) {
        this.blockUI.start();
        this._taskService.Delete(id)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            this.scrollTopFromDelete();
            this.getTasks();
            this.blockUI.stop();
          });
      }
    });
  }

  onSubmit(formData: any, formDirective: FormGroupDirective) {
    this.blockUI.start();
    if (this.actionPlanForm.valid) {
      if (this.validateDate(this.implementationPlannedDate.value)) {
        this._task.entityID = this.correctiveAction.correctiveActionID;
        this._task.description = this.description.value;
        this._task.responsibleUserID = this.responsibleUser.value;
        this._task.implementationPlannedDate = this.implementationPlannedDate.value;
        this._task.requireEvidence = this.requireEvidence.value;

        this._taskService.Add(this._task)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            this.firstTimeSetMaxDate = false;
            this.getTasks();
            let impactTemp = this.impact.value;
            formDirective.resetForm();
            this.actionPlanForm.reset();
            this.impact.patchValue(impactTemp);
            let today = new Date();
            today.setDate(today.getDate() + 1);
            this.implementationPlannedDate.setValue(today);
            this.requireEvidence.patchValue(false);
            this.blockUI.stop();
          });
      } else {
        this._toastrManager.errorToastr("La fecha de vencimiento debe ser mayor a la fecha de hoy.", "Error");
        this.blockUI.stop();
      }
    } else {
      this._toastrManager.errorToastr("Ingrese todos los campos obligatorios.");
      this.blockUI.stop();
    }
  }

  onEditSubmit(form: any, formDirective: FormGroupDirective) {
    this.blockUI.start();
    if (this.actionPlanForm.valid) {
      if (this.validateDate(form.implementationPlannedDate)) {
        this._task.taskID = form.taskID;
        this._task.taskStateID = form.taskStateID;
        this._task.description = form.description;
        this._task.responsibleUserID = form.responsibleUser;
        this._task.implementationPlannedDate = form.implementationPlannedDate;
        this._task.requireEvidence = form.requireEvidence;

        this._taskService.Update(this._task)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            this.isEditing = false;
            let impactTemp = this.impact.value;
            this.getTasks();
            formDirective.resetForm();
            this.actionPlanForm.reset();
            this.impact.patchValue(impactTemp);
            let today = new Date();
            today.setDate(today.getDate() + 1);
            this.implementationPlannedDate.setValue(today);
            this.requireEvidence.patchValue(false);
            this.blockUI.stop();
          });
      } else {
        this._toastrManager.errorToastr("La fecha de vencimiento debe ser mayor a la fecha de hoy.", "Error");
        this.blockUI.stop();
      }
    } else {
      this._toastrManager.errorToastr("Ingrese todos los campos obligatorios.");
      this.blockUI.stop();
    }
  }

  validateDate(date) {
    if (new Date(new Date().setHours(0, 0, 0, 0)).getTime() >= new Date(new Date(date).setHours(0, 0, 0, 0)).getTime()) {
      this.blockUI.stop();
      return false;
    }
    return true;
  }

  scrollTopFromEdit() {
    const mainDiv = document.getElementsByClassName('wizard-navigation')[0];
    mainDiv.scrollIntoView(true);
  }

  scrollTopFromDelete() {
    const mainDiv = document.getElementsByClassName('mat-table')[0];
    mainDiv.scrollIntoView(true);
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim().toLowerCase();
    this.dataSource.filter = filterValue;
  }

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}

export class PaginatorEspañol extends MatPaginatorIntl {
  itemsPerPageLabel = 'Items por Página';
  nextPageLabel = 'Siguiente';
  previousPageLabel = 'Previa';
  firstPageLabel = "Primera Página";
  lastPageLabel = "Última Página"

  getRangeLabel = function (page, pageSize, length) {
    if (length === 0 || pageSize === 0) {
      return '0 de ' + length;
    }
    length = Math.max(length, 0);
    const startIndex = page * pageSize;
    //Si el índice de inicio excede la longitud de la lista, no intente 
    //arreglar el índice final hasta el final
    const endIndex = startIndex < length ?
      Math.min(startIndex + pageSize, length) : startIndex + pageSize;
    return startIndex + 1 + ' - ' + endIndex + ' de ' + length;
  };
} 
