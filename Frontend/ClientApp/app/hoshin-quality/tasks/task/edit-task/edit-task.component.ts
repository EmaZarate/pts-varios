import { Component, OnInit, ElementRef, ViewChildren, OnDestroy } from '@angular/core';
import { EditTask } from '../../models/editTask';
import {
  FormGroup,
  FormControlName,
  FormBuilder,
  Validators
} from '@angular/forms';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Router, ActivatedRoute } from '@angular/router';
import { TaskService } from '../../taskService.service';
import { TaskStateService } from '../../state-task.service';
import { EditTaskEvidence } from '../../models/editTaskEvidence';
import { StateTaskC } from '../../models/stateTask';
import { EditTaskUpdate } from '../../models/EditTaskUpdate';
import { MyFile } from 'ClientApp/app/shared/components/upload-large-files/upload-large-files.component';
import * as moment from 'moment';
import { Subject } from 'rxjs';
@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css']
})
export class EditTaskComponent implements OnInit, OnDestroy {
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[];
  _newAttachments: Array<MyFile> = [];
  editTaskStateForm: FormGroup;
  _editTask = new EditTask();
  _taskState: StateTaskC[] = [];
  _editTaskEvidence: EditTaskEvidence[] = [];
  _newEditTaskEvidence: EditTaskEvidence[] = [];
  _oldEditTaskEvidence: EditTaskEvidence[] = [];
  UserFullName;
  taskID;
  stateTaskID;
  codeStateTask;
  taskstatecode;
  btnComplete;
  get name() {
    return this.editTaskStateForm.get('name');
  }
  get entityType() {
    return this.editTaskStateForm.get('entityType');
  }
  get entityID() {
    return this.editTaskStateForm.get('entityID');
  }
  get user() {
    return this.editTaskStateForm.get('user');
  }
  get stateTask() {
    return this.editTaskStateForm.get('stateTask');
  }
  get implementationEffectiveDate() {
    return this.editTaskStateForm.get('implementationEffectiveDate');
  }
  get requireEvidence() {
    return this.editTaskStateForm.get('requireEvidence');
  }
  get observation() {
    return this.editTaskStateForm.get('observation');
  }
  get typeButtonSubmit() {
    return this.editTaskStateForm.get('completeBtn');
  }
  get completeTask() {
    return this.editTaskStateForm.get('completeTask');
  }
  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private _fb: FormBuilder,
    private _taskEditService: TaskService,
    private _taskStateService: TaskStateService
  ) {}

  ngOnInit() {
    this.blockUI.start();
    this.functionReload();
  }

  functionReload() {
    this.editTaskStateForm = this.modelCreate();
    this.disableFields();
    this._route.params
    .takeUntil(this.ngUnsubscribe)
    .subscribe(res => {
      this._taskEditService.get(res.id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(res => {
        this._editTask = res;
        this.UserFullName = res.responsibleUser;
        this._editTaskEvidence = res.taskEvidences;
        this._editTaskEvidence = res.taskEvidences.map(element => {
          element.id = element.evidenceID;
          element.name = element.fileName;
          return element;
        });
        this.setTaskEditServiceValues(this._editTask);
        this._taskStateService.getAll()
        .takeUntil(this.ngUnsubscribe)
        .subscribe(res => {
          this._taskState = res;
          for (const state of this._taskState) {
            if (state.taskStateID === this._editTask.taskStateID) {
              this.stateTask.patchValue(state.name);
              this.taskstatecode = state.code;
              this.blockUI.stop();
            }
            if (this.taskstatecode === 'COM') {
              this.observation.disable();
              this.completeTask.disable();
              this.btnComplete = true;
            }
          }
        });
      });
    });
  }

  setTaskEditServiceValues(res) {

this.taskID = this._editTask.taskID;
this.name.patchValue(this._editTask.description);
this.entityType.patchValue('Acciones Correctivas');
this.entityID.patchValue(this._editTask.entityID);
const fecha = this._editTask.implementationPlannedDate;
const Fecha = moment(fecha, 'YYYY/MM/DD HH:mm').format('MM/DD/YYYY');
this.user.patchValue(
      this.UserFullName.name + ' ' + this.UserFullName.surname
    );
this.implementationEffectiveDate.patchValue(Fecha);
this.requireEvidence.patchValue(this._editTask.requireEvidence);
this.observation.patchValue(this._editTask.observation);
this._editTaskEvidence = this._editTask.taskEvidences;
this._newAttachments = new Array<MyFile>();
if (this._editTask.observation === 'null') {this.observation.patchValue(''); }
  }

  disableFields() {
    this.name.disable();
    this.entityType.disable();
    this.entityID.disable();
    this.user.disable();
    this.stateTask.disable();
    this.implementationEffectiveDate.disable();
    this.requireEvidence.disable();
  }

  getIDStateTask(code) {
    let stateID;
    for (const state of this._taskState) {
      if (state.code === code) {
        stateID = state.taskStateID;
        return stateID;
      }
    }
  }

  functionOnSubmit(value) {
    if(this.editTaskStateForm.get('observation').value != null) {
      const datataskupdate = new EditTaskUpdate();

      datataskupdate.Observation = this.observation.value;
      datataskupdate.TaskEvidences = this._editTaskEvidence.filter(
        x => x.isInsert
      );
      datataskupdate.DeleteEvidencesUrls = this._editTaskEvidence
        .filter(x => x.isDelete)
        .map(y => y.name);
      datataskupdate.TaskID = this.taskID;
  
      if (this.taskstatecode === 'NIN') {
        datataskupdate.TaskStateCode = 'ECU';
        datataskupdate.TaskStateID = this.getIDStateTask('ECU');
      }
      if (this.taskstatecode === 'ECU') {
        datataskupdate.TaskStateID = this.getIDStateTask('ECU');
      }
      if (value === 'complete') {
  
        datataskupdate.TaskStateID = this.getIDStateTask('COM');
        datataskupdate.TaskStateCode = 'COM';
        datataskupdate.ImplementationEffectiveDate = new Date().toISOString();
        datataskupdate.EntityID = this._editTask.entityID;
        datataskupdate.EntityType = this._editTask.entityType;
        if (this.requireEvidence.value) {
          const evindence: number = this._editTaskEvidence.length;
          let evidenceDeleted = 0;
          for (const s of this._editTaskEvidence) {
            if (s.isDelete) {
              evidenceDeleted = evidenceDeleted + 1;
            }
          }
          if (evidenceDeleted >= evindence) {
            this._toastrManager.warningToastr(
              'Debe agregar al menos una evidencia'
            );
            this.blockUI.stop();
            return;
          }
          this._taskEditService
              .update(datataskupdate)
              .takeUntil(this.ngUnsubscribe)
              .subscribe(this.navigateToList);
          this.btnComplete = true;
          return;
        }
  
        this._taskEditService
            .update(datataskupdate)
            .takeUntil(this.ngUnsubscribe)
            .subscribe(this.navigateToList);
        this.btnComplete = true;
        return;
      }
      this._taskEditService
          .update(datataskupdate)
          .takeUntil(this.ngUnsubscribe)
          .subscribe(this.navigateToList);
    }else {
      this._toastrManager.errorToastr("Debe ingresar comentarios en el campo observación");
      this.blockUI.stop();
    }
  }

  navigateToList = () => {
    this._toastrManager.successToastr(
      'La tarea se actualizó correctamente',
      'Éxito'
    );
    this._router.navigate(['/quality/tasks/list']);
    this.blockUI.stop();
  }

  onSubmit(event) {
    this.blockUI.start();
    const value = event._elementRef.nativeElement.value;
    this.functionOnSubmit(value);
  }

  modelCreate() {
    return this._fb.group({
      name: ['', Validators.required],
      entityType: ['Acciones Correctivas', Validators.required],
      entityID: ['', Validators.required],
      user: ['', Validators.required],
      stateTask: ['', Validators.required],
      implementationEffectiveDate: ['', Validators.required],
      evidenceRequire: ['', Validators.required],
      observation: ['', ''],
      requireEvidence: ['', Validators.required],
      completeTask: ['', ''] });
  }

  getAttachments(event): void {
    this._editTaskEvidence = event;
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
