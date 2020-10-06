import { Component, OnInit, ViewChildren, ElementRef, ViewChild, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControlName, Validators, FormBuilder } from '@angular/forms';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { FindingsService } from '../../../../core/services/findings.service';
import { PlantsService } from '../../../../core/services/plants.service';
import { FindingsTypeService } from '../../../../core/services/findings-type.service';

import { PARAMETRIZATION_CRITERIAS } from '../../models/ParamCriteriaEnum';
import { Finding } from '../../models/Finding';
import { FindingEvidence } from '../../models/FindingEvidence';
import { Subject } from 'rxjs';

declare const $: any;

enum EVENT_TYPE {
  'Aprobar' = 1,
  'AprobarConPDCA' = 2,
  'Rechazar' = 3
};

@Component({
  selector: 'app-approve-finding',
  templateUrl: './approve-finding.component.html',
  styleUrls: ['./approve-finding.component.css'],
})

export class ApproveFindingComponent implements OnInit, OnDestroy {

  @ViewChild('file') file;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  approveFindingForm: FormGroup;
  approveWithACForm: FormGroup;
  finding;
  _findingEvidences: FindingEvidence[];
  _newFindingEvidences: FindingEvidence[] = [];
  _oldFindingEvidences: FindingEvidence[] = [];

  allPlants = [];

  isSelectReviewer:boolean = false;

  sectorLocations = [];
  sectorTreatments = [];
  evaluatorSectors = [];
  responsibleUsers = [];
  evaluatorUsers = [];
  findingTypes;


  showBtnAprobarConCA;

  get description() { return this.approveFindingForm.get('description'); }
  get plantLocation() { return this.approveFindingForm.get('plantLocation'); }
  get sectorLocation() { return this.approveFindingForm.get('sectorLocation'); }
  get plantTreatment() { return this.approveFindingForm.get('plantTreatment'); }
  get sectorTreatment() { return this.approveFindingForm.get('sectorTreatment'); }
  get responsibleUser() { return this.approveFindingForm.get('responsibleUser'); }
  get findingType() { return this.approveFindingForm.get('findingType'); }
  get expirationDate() { return this.approveFindingForm.get('expirationDate'); }

  get evaluatorSector() { return this.approveWithACForm.get('evaluatorSector'); }
  get evaluatorPlant() { return this.approveWithACForm.get('evaluatorPlant'); }
  get reviewer() { return this.approveWithACForm.get('reviewer'); }

  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _findingsService: FindingsService,
    private _plantsService: PlantsService,
    private _findingTypesService: FindingsTypeService,
  ) { }



  ngOnInit() {
    this.blockUI.start();
    this.approveFindingForm = this.modelCreate();
    this.approveWithACForm = this.modelCreateWithAC();
    this.sectorLocation.disable();
    this.sectorTreatment.disable();
    this._findingsService.get(this._route.snapshot.params.id)
     .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this.finding = res;
        this._findingEvidences = res.findingsEvidences.map(element => {
          element.id = element.findingEvidenceID;
          element.name = element.fileName;
          return element;
        });
        this._plantsService.getAll()
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res) => {
            this.allPlants = res;
            this._findingTypesService.getAllActives()
             .takeUntil(this.ngUnsubscribe)
              .subscribe((res) => {
                this.findingTypes = res;
                this.patchFinding();
                if (this.finding.plantTreatmentID != 0 && this.finding.sectorTreatmentID != 0) {
                  this._findingsService.getAllUsers(this.finding.sectorTreatmentID, this.finding.plantTreatmentID)
                  .takeUntil(this.ngUnsubscribe) 
                  .subscribe((users: any[]) => {
                      this.responsibleUsers = users;
                      this.patchUser();
                      //console.log(users);
                      this.blockUI.stop()
                    });
                }
                else{
                  this.blockUI.stop()
                }
              });
          });
      });
  }

  patchUser() {
    this.responsibleUser.patchValue(this.finding.responsibleUserID);
  }

  patchFinding() {
    this.findingType.patchValue(this.finding.findingTypeID);

    this.description.patchValue(this.finding.description);

    if(this.finding.plantTreatmentID != 0 && this.finding.sectorTreatmentID != 0) {
    this.plantLocation.patchValue(this.finding.plantLocationID);
    this.plantTreatment.patchValue(this.finding.plantTreatmentID);

    this.changeSelectionLocation(this.finding.plantLocationID);
    this.changeSelectionTreatment(this.finding.plantTreatmentID);


    this.sectorLocation.patchValue(this.finding.sectorLocationID);
    this.sectorTreatment.patchValue(this.finding.sectorTreatmentID);
    }
    this.changeSelectionFindingType(this.finding.findingTypeID);
  }

  modelCreate() {
    return this.fb.group({
      description: ['', Validators.required],
      plantLocation: ['', Validators.required],
      sectorLocation: ['', [Validators.required]],
      plantTreatment: ['', Validators.required],
      sectorTreatment: ['', Validators.required],
      responsibleUser: ['', Validators.required],
      findingType: ['', Validators.required],
      expirationDate: ['', Validators.required]
    });
  }

  modelCreateWithAC(){
    return this.fb.group({
      evaluatorPlant: ['', Validators.required],
      evaluatorSector: ['', Validators.required],
      reviewer: ['', Validators.required]
    })
  }
  changeSelectionEvaluatorPlant(val){
    let pl = this.allPlants.find(x => x.plantID == val);
    this.evaluatorPlant.patchValue(val);
    this.evaluatorSectors = pl.sectors;
    this.evaluatorSector.enable();
  }

  changeSelectionEvaluatorSector(val){
    this.blockUI.start();
    let plantId = this.evaluatorPlant.value;
    this.evaluatorUsers = [];
    const that = this;
    this._findingsService.getAllUsers(val, plantId)
     .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any[]) => {
        this.responsibleUser.enable();
        res.forEach(function(user){
          if(user.active){
            that.evaluatorUsers.push(user);
          }
        });
        console.log(this.evaluatorUsers);
        this.reviewer.enable();
        let sector = this.evaluatorSectors.find(x => x.id = val);
        this.reviewer.enable();
        this.blockUI.stop();
      });
  }
  changeSelectionLocation(val) {
    let pl = this.allPlants.find(x => x.plantID == val);
    this.plantLocation.patchValue(val);
    this.sectorLocations = pl.sectors;
    this.sectorLocation.enable();
  }

  changeSelectionTreatment(val) {
    let pl = this.allPlants.find(x => x.plantID == val);
    this.plantTreatment.patchValue(val);
    this.sectorTreatments = pl.sectors;
    this.sectorTreatment.enable();
  }

  changeSelectionFindingType(val) {
    let findtype = this.findingTypes.find(x => x.id == val);
    this.setSugestedExpirationDate(findtype.parametrizations);
    this.showHideButtons(findtype.parametrizations);
  }

  changeSelectionSectorTreatment(id){
    const plantId = this.plantTreatment.value;
    this.blockUI.start();
    this._findingsService.getAllUsers(id, plantId)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any[]) => {
        this.responsibleUser.enable();
        this.responsibleUsers = res;
        const sector = this.sectorTreatments.find(x => x.id = id);

        this.responsibleUser.patchValue(sector.resposibleUserPlantSector);
        this.blockUI.stop();
      });
  }

  setSugestedExpirationDate(val) {
    let days = (val.find(x => x.parametrizationCriteria.name == PARAMETRIZATION_CRITERIAS.DiasVencimientoSugerido))
    if (days != undefined) {
      const today = new Date();
      today.setDate(today.getDate() + parseInt(days.value))
      this.expirationDate.patchValue(today);
    }
  }
  showHideButtons(val) {
    if ((val.find(x => x.parametrizationCriteria.name == PARAMETRIZATION_CRITERIAS.GenerarAC)) != undefined) {
      //GENERA AC
      this.showBtnAprobarConCA = false;
    }
    else {
      //NO GENERA AC
      this.showBtnAprobarConCA = true;
    }
  }

  onRejectFinding() {
    if (!this.approveFindingForm.valid) { return; }

    this._router.navigate(['/quality/finding/', this.finding.id, 'reject']);
  }

  onSubmit(eventType) {
    debugger
    if (!this.approveFindingForm.valid) { return; }

    this.blockUI.start();
    const find = new Finding();
    find.description = this.description.value;
    find.expirationDate = this.expirationDate.value != null ? new Date(this.expirationDate.value).toUTCString() : new Date().toUTCString();
    find.findingTypeId = this.findingType.value;
    find.plantLocationId = this.plantLocation.value;
    find.plantTreatmentId = this.plantTreatment.value;
    find.responsibleUserId = this.responsibleUser.value;
    find.sectorLocationId = this.sectorLocation.value;
    find.sectorTreatmentId = this.sectorTreatment.value;
    find.workflowId = this.finding.workflowId;
    find.findingStateID = this.finding.findingStateID;
    find.emitterUserID = this.finding.emitterUserID;
    find.findingId = this.finding.id;
    find.findingEvidences = this._findingEvidences.filter(x => x.isInsert);
    find.fileNamesToDelete = this._findingEvidences.filter(x => x.isDelete).map(y => y.name);

    if (new Date(new Date().setHours(0, 0, 0, 0)).getTime() > new Date(new Date(this.expirationDate.value).setHours(0, 0, 0, 0)).getTime()) {
      this._toastrManager.errorToastr("La fecha de vencimiento debe ser mayor a la fecha de hoy", "Error");
      this.blockUI.stop();
      return false;
    }

    if (eventType == EVENT_TYPE.Aprobar) {
      if (this.showBtnAprobarConCA) {
        //Approve without PDCA
        find.eventData = 'Approve';
      }
      else {
        //Approve with PDCA
        if (!this.approveWithACForm.valid) { this.blockUI.stop(); return; }
        find.reviewerUserID = this.reviewer.value;
        find.eventData = 'ApproveWithPDCA';
      }
    }
    if (eventType === EVENT_TYPE.AprobarConPDCA) {
      if (!this.approveWithACForm.valid) { this.blockUI.stop(); return; }
      find.reviewerUserID = this.reviewer.value;
      find.eventData = 'ApproveWithPDCA';
    }

    if (eventType === EVENT_TYPE.Rechazar) {
      find.eventData = 'Rejected';
    }
    this._findingsService.approveFindingStep(find)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this._toastrManager.successToastr('El hallazgo se ha aprobado correctamente', 'Éxito');
        this._router.navigate(['/quality/finding']);
        this.blockUI.stop();
      });
  }

  scollTop(){
    $(".main-panel.ps.ps--active-y").scrollTop(0);
  }

  getAttachments(event): void {
    this._findingEvidences = event;
  }

  onSubmitWithAC() {
    if (!this.approveFindingForm.valid) { return; }
    this.isSelectReviewer = true;
    this.evaluatorSector.disable();
    this.reviewer.disable();
    this.scollTop();
  }

  backEditFinding(){
    this.isSelectReviewer = false;
    this.reviewer.patchValue('');
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
