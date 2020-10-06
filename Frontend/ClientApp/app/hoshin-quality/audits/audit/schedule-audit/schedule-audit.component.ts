import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ToastrManager } from 'ng6-toastr-notifications';
import { AuditTypesService } from "../../audit-types.service";
import { PlantsService } from "../.././../../core/services/plants.service";
import { StandardService } from "../../configuration/standard.service";
import { AuditType } from '../../models/AuditTypes';
import { UsersService } from "ClientApp/app/core/services/users.service";
import { Audit } from '../../models/Audit';
import { AuditStateCode } from '../../models/AuditStateCode';
import { AuditService } from "../../audit.service";
import { Router, ActivatedRoute } from '@angular/router';
import { forkJoin, Subject } from 'rxjs';
import * as moment from 'moment';
// import { DateAdapter, MAT_DATE_FORMATS } from '@angular/material';
// import { AppDateAdapter, APP_DATE_FORMATS } from "../../../../shared/util/dates/date.adapter";

@Component({
  selector: 'app-schedule-audit',
  templateUrl: './schedule-audit.component.html',
  styleUrls: ['./schedule-audit.component.css'],
  //   providers: [
  //     {
  //         provide: DateAdapter, useClass: AppDateAdapter
  //     },
  //     {
  //         provide: MAT_DATE_FORMATS, useValue: APP_DATE_FORMATS
  //     }
  // ],
})
export class ScheduleAuditComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  
  auditForm: FormGroup;
  isCreate: boolean;
  isPlannigOrReplannig: boolean;
  plants;
  auditTypes: AuditType[];
  standards;
  sectors;
  auditors;

  //auditTypeSelected = false;
  //auditTypeEXT = false;
  auditTypeINT = false;
  audit: Audit = new Audit();



  get auditType() { return this.auditForm.get('auditType') }
  get standard() { return this.auditForm.get('standard') }
  get plant() { return this.auditForm.get('plant') }
  get sector() { return this.auditForm.get('sector') }
  get auditor() { return this.auditForm.get('auditor') }
  get date() { return this.auditForm.get('date') }
  get documentsAnalysisDate() { return this.auditForm.get('documentsAnalysisDate'); }
  get documentAnalysisDuration() { return this.auditForm.get('documentAnalysisDuration'); }
  get auditInitDate() { return this.auditForm.get('auditInitDate'); }
  get auditInitTime() { return this.auditForm.get('auditInitTime'); }
  get auditFinishDate() { return this.auditForm.get('auditFinishDate'); }
  get auditFinishTime() { return this.auditForm.get('auditFinishTime'); }
  get mettingCloseDate() { return this.auditForm.get('mettingCloseDate'); }
  get mettingCloseTime() { return this.auditForm.get('mettingCloseTime'); }
  get planCloseDate() { return this.auditForm.get('planCloseDate'); }
  get internal() { return this.auditForm.get('internal') }

  constructor(
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _auditTypeService: AuditTypesService,
    private _plantService: PlantsService,
    private _standardService: StandardService,
    private _userService: UsersService,
    private _auditService: AuditService,
    private _router: Router,
    private _route: ActivatedRoute
  ) { }

  ngOnInit() {
    
    this.blockUI.start();
    this.auditForm = this.modelCreate();
    this.sector.disable();
    forkJoin(this._plantService.getAll(), this._auditTypeService.getAllActive(), this._standardService.getAll())
    .takeUntil(this.ngUnsubscribe) 
    .subscribe(response => {
        this.plants = response[0];
        this.auditTypes = response[1];
        this.standards = response[2].filter(x => x.active == true);
        this._route.data
        .takeUntil(this.ngUnsubscribe)
        .subscribe((data) => {
          if (data.typeForm == 'new') {
            this.isCreate = true;
            if ((this._auditService.getCreateAuditDate())) {
              this.auditInitDate.patchValue(this._auditService.getCreateAuditDate())
              this._auditService.setCreateAuditDate(null);
            }
            this.blockUI.stop();
            this.clearValidators();
          }
          else {
            this.isCreate = false;
            this._route.params
            .takeUntil(this.ngUnsubscribe)
            .subscribe((res) => {
              this._auditService.get(res.id)
              .takeUntil(this.ngUnsubscribe)  
              .subscribe((res: any) => {
                  this.audit = res;
                  this.patchAudit();
                  this.blockUI.stop();
                  if (this.audit.auditorID) {
                    this.blockUI.start();
                    this._userService.getAll()
                    .takeUntil(this.ngUnsubscribe)
                    .subscribe(users => {
                      this.auditors = users
                      this.blockUI.stop();
                      this.auditor.patchValue(this.audit.auditor.id);
                      this.internal.patchValue(true);
                    });
                  }
                  else {
                    this.auditor.patchValue(this.audit.externalAuditor)
                  }
                  if (this.audit.auditState.code == "PLA" || this.audit.auditState.code == "PRZ") {
                    this.isPlannigOrReplannig = true
                    this.auditType.disable();
                    this.standard.disable();
                    this.auditor.disable();
                    this.internal.disable();
                  }
                  else {
                    this.clearValidators();
                  }
                });
            });
          }
        });
      });

  }

  clearValidators() {
    this.auditInitTime.setValidators([]);
    this.auditInitTime.updateValueAndValidity();
    this.auditFinishDate.setValidators([]);
    this.auditFinishDate.updateValueAndValidity();
    this.auditFinishTime.setValidators([]);
    this.auditFinishTime.updateValueAndValidity();
    this.documentsAnalysisDate.setValidators([]);
    this.documentsAnalysisDate.updateValueAndValidity();
    this.documentAnalysisDuration.setValidators([]);
    this.documentAnalysisDuration.updateValueAndValidity();
    this.mettingCloseDate.setValidators([]);
    this.mettingCloseDate.updateValueAndValidity();
    this.mettingCloseTime.setValidators([]);
    this.mettingCloseTime.updateValueAndValidity();
    this.planCloseDate.setValidators([]);
    this.planCloseDate.updateValueAndValidity();
  }

  modelCreate() {
    let today = new Date();
    today.setDate(today.getDate() + 1);

    return this.fb.group({
      auditType: ['', Validators.required],
      standard: ['', Validators.required],
      plant: ['', Validators.required],
      sector: ['', Validators.required],
      auditor: ['', Validators.required],
      auditInitDate: [today, Validators.required],
      auditInitTime: ['', Validators.required],
      documentsAnalysisDate: ['', Validators.required],
      documentAnalysisDuration: ['', [Validators.required, Validators.max(24), Validators.min(0.1)]],
      auditFinishDate: ['', Validators.required],
      auditFinishTime: ['', Validators.required],
      mettingCloseDate: ['', Validators.required],
      mettingCloseTime: ['', [Validators.required, Validators.max(24), Validators.min(0.1)]],
      planCloseDate: ['', Validators.required],
      internal: []
    });
  }

  patchAudit() {
    this.auditType.patchValue(this.audit.auditType.id);
    this.standard.patchValue(this.getStandardsId());
    this.plant.patchValue(this.audit.plantID);
    this.changeSelectionPlant(this.audit.plantID);
    this.sector.patchValue(this.audit.sectorID);
    this.documentsAnalysisDate.patchValue(this.audit.documentsAnalysisDate);
    this.documentAnalysisDuration.patchValue(this.audit.documentAnalysisDuration);

    this.auditInitDate.patchValue(this.audit.auditInitDate);
    let auditInitTime = new Date(moment.utc(this.audit.auditInitTime).toISOString());
    this.auditInitTime.patchValue(this.formatTime(auditInitTime));

    this.mettingCloseDate.patchValue(this.audit.closeMeetingDate);
    this.mettingCloseTime.patchValue(this.audit.closeMeetingDuration);

    this.planCloseDate.patchValue(this.audit.closeDate);

    this.auditFinishDate.patchValue(this.audit.auditFinishDate);
    let auditFinishTime = new Date(moment.utc(this.audit.auditFinishTime).toISOString());
    this.auditFinishTime.patchValue(this.formatTime(auditFinishTime));
  }

  formatTime(date) {
    let hours = date.getHours()
    let minutes = date.getMinutes()
    if (hours < 10) {
      hours = '0' + hours
    }
    if (minutes < 10) {
      minutes = '0' + minutes
    }
    return (hours + ':' + minutes)
  }

  changeSelectionPlant(plantID) {
    let plant = this.plants.find(x => x.plantID == plantID);
    this.sectors = plant.sectors;
    this.sector.enable()
    this.plant.patchValue(plant.plantID)
  }

  changeAuditInternal() {
    const that = this;
    let result : any;
    this.auditors = [];
    if (this.internal.value) {
      this.blockUI.start();
      this._userService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe(users => {
        result = users;
        result.forEach(function(user){
          if(user.active && that.isAuditor(user)){
            that.auditors.push(user);
          }
        }); 
        this.blockUI.stop();
        // this.auditor.patchValue(this.audit.auditor.id);
      });
    }
    else {
      this.auditor.patchValue(this.audit.externalAuditor);
    }
  }

  isAuditor(user){
    let b = false;
    user.roles.forEach(function(rol){
      if(rol.toLowerCase() === 'auditor'){
        b = true;
      }
    });
    return b;
  }
  getStandardsId() {
    let standardsId = this.audit.auditStandards.map(x => x.standardID);
    return standardsId;
  }

  validateForm() {
    if (new Date(new Date().setHours(0, 0, 0, 0)).getTime() > new Date(new Date(this.documentsAnalysisDate.value).setHours(0, 0, 0, 0)).getTime()) {
      this._toastrManager.errorToastr("La fecha de Análisis de documentación debe ser mayor o igual a la fecha de hoy", "Error"); return false;
    }

    let auditInitTimeHour = this.auditInitTime.value.substring(0, 2);
    let auditInitTimeMinutes = this.auditInitTime.value.substring(3, 5);

    let auditFinishTimeHour = this.auditFinishTime.value.substring(0, 2);
    let auditFinishTimeMinutes = this.auditFinishTime.value.substring(3, 5);

    if (new Date(new Date(this.documentsAnalysisDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.auditInitDate.value).setHours(0, 0, 0, 0)).getTime()) {
      if (new Date(new Date(this.auditInitDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.mettingCloseDate.value).setHours(0, 0, 0, 0)).getTime()) {
        if (new Date(new Date(this.mettingCloseDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.auditFinishDate.value).setHours(0, 0, 0, 0)).getTime()) {
          if (new Date(new Date(this.auditInitDate.value).setHours(auditInitTimeHour, auditInitTimeMinutes)).getTime() < new Date(new Date(this.auditFinishDate.value).setHours(auditFinishTimeHour, auditFinishTimeMinutes)).getTime()) {
            if (new Date(new Date(this.auditFinishDate.value).setHours(0, 0, 0, 0)).getTime() <= new Date(new Date(this.planCloseDate.value).setHours(0, 0, 0, 0)).getTime()) {
              return true
            }
            this._toastrManager.errorToastr("La fecha de fin de auditoría debe ser menor a la fecha de cierre de plan", "Error"); return false;
          }
          this._toastrManager.errorToastr("La hora de inicio de auditoría se superpone con la hora de fin de auditoría", "Error"); return false;
        }
        this._toastrManager.errorToastr("La fecha de reunión de cierre debe ser menor o igual a la fecha de fin de auditoría", "Error"); return false;
      }
      this._toastrManager.errorToastr("La fecha de inico de auditoría debe ser menor o igual a la fecha de reunión de cierre", "Error"); return false;
    }
    this._toastrManager.errorToastr("La fecha de Análisis de documentación debe ser menor o igual a la fecha de inico de auditoría", "Error"); return false;
  }

  validateAuditInitDate() {
    if (new Date(new Date().setHours(0, 0, 0, 0)).getTime() >= new Date(new Date(this.auditInitDate.value).setHours(0, 0, 0, 0)).getTime()) {
      this._toastrManager.errorToastr("La fecha de inico de auditoría debe ser mayor a la fecha de hoy", "Error"); return false;
    }
    return true
  }

  onSubmit() {
    if (this.auditForm.valid) {
      
      this.blockUI.start();
      this.audit.auditTypeID = this.auditType.value;
      this.audit.auditStandard = this.standard.value;
      this.audit.plantID = this.plant.value;
      this.audit.sectorID = this.sector.value;
      this.audit.auditInitDate = this.auditInitDate.value;

      if (this.internal.value) {
        this.audit.auditorID = this.auditor.value;
        this.audit.externalAuditor = "";
      }
      else {
        this.audit.externalAuditor = this.auditor.value;
        this.audit.auditorID = null;
      }

      if (this.isCreate) {
        if (this.validateAuditInitDate()) {
          this.audit.creationDate = new Date();
          this.audit.auditStateCode = AuditStateCode.PRO;
          this._auditService.add(this.audit)
          .takeUntil(this.ngUnsubscribe)
          .subscribe(() => {
            this.blockUI.stop();
            this._toastrManager.successToastr('La auditoría se ha programado correctamente', 'Éxito');
            this._router.navigate(['/quality/audits/list'])
          });
        }
        else {
          this.blockUI.stop();
        }
      }
      else {
        if (this.audit.auditStateID == 3 || this.audit.auditStateID == 4) {
          if (this.validateForm()) {
            let auditInitTimeHour = this.auditInitTime.value.substring(0, 2);
            let auditInitTimeMinutes = this.auditInitTime.value.substring(3, 5);

            let auditFinishTimeHour = this.auditFinishTime.value.substring(0, 2);
            let auditFinishTimeMinutes = this.auditFinishTime.value.substring(3, 5);

            this.audit.documentsAnalysisDate = new Date(this.documentsAnalysisDate.value);
            this.audit.documentAnalysisDuration = this.documentAnalysisDuration.value;
            this.audit.auditInitDate = new Date(new Date(this.auditInitDate.value).setHours(auditInitTimeHour, auditInitTimeMinutes));
            this.audit.auditInitTime = new Date(new Date(this.auditInitDate.value).setHours(auditInitTimeHour, auditInitTimeMinutes));
            this.audit.auditFinishDate = new Date(new Date(this.auditFinishDate.value).setHours(auditFinishTimeHour, auditFinishTimeMinutes));
            this.audit.auditFinishTime = new Date(new Date(this.auditFinishDate.value).setHours(auditFinishTimeHour, auditFinishTimeMinutes));
            this.audit.closeMeetingDate = new Date(this.mettingCloseDate.value);
            this.audit.closeMeetingDuration = this.mettingCloseTime.value;
            this.audit.closeDate = new Date(this.planCloseDate.value);

            this._auditService.update(this.audit)
            .takeUntil(this.ngUnsubscribe)
            .subscribe(() => {
              this.blockUI.stop();
              this._toastrManager.successToastr('La auditoría se ha reprogramado correctamente', 'Éxito');
              this._router.navigate(['/quality/audits/list'])
            });
          }
          else {
            this.blockUI.stop()
          }
        }
        else {
          if (this.validateAuditInitDate()) {
            this._auditService.update(this.audit)
            .takeUntil(this.ngUnsubscribe)
            .subscribe(() => {
              this.blockUI.stop();
              this._toastrManager.successToastr('La auditoría se ha reprogramado correctamente', 'Éxito');
              this._router.navigate(['/quality/audits/list'])
            });
          }
          else {
            this.blockUI.stop()
          }
        }
      }
    }
    else{
      this._toastrManager.errorToastr('Debe completar todos los campos obligatorios', 'Error');
    }
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
