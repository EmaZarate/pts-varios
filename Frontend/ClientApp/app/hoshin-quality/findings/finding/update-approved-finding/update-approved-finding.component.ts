import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControlName, Validators, FormBuilder } from '@angular/forms';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { FindingsService } from '../../../../core/services/findings.service';
import { PlantsService } from '../../../../core/services/plants.service';
import { FindingsTypeService } from '../../../../core/services/findings-type.service';

import { Finding } from '../../models/Finding';

import { setValidatedDate } from 'ClientApp/app/shared/util/dates/set-validated-date.function';
import { FindingEvidence } from '../../models/FindingEvidence';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-update-approved-finding',
  templateUrl: './update-approved-finding.component.html',
  styleUrls: ['./update-approved-finding.component.css'],
})
export class UpdateApprovedFindingComponent implements OnInit, OnDestroy {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  
  _findingEvidences: FindingEvidence[] = [];
  _newFindingEvidences: FindingEvidence[] = [];
  _oldFindingEvidences: FindingEvidence[] = [];

  approveFindingForm : FormGroup;
  finding;
  allPlants = [];

  sectorEmisors = [];
  sectorTreatments = [];
  sectorLocations = [];

  responsibleUsers = [];
  comments = [];
  findingTypes;

  showBtnAprobarConPDCA;

  get findingState() { return this.approveFindingForm.get('findingState'); }
  get description() { return this.approveFindingForm.get('description'); }
  get plantEmisor() { return this.approveFindingForm.get('plantEmisor'); }
  get sectorEmisor() { return this.approveFindingForm.get('sectorEmisor'); }
  get plantTreatment() { return this.approveFindingForm.get('plantTreatment'); }
  get sectorTreatment() { return this.approveFindingForm.get('sectorTreatment'); }
  get plantLocation() { return this.approveFindingForm.get('plantLocation'); }
  get sectorLocation() { return this.approveFindingForm.get('sectorLocation'); }
  get responsibleUser() { return this.approveFindingForm.get('responsibleUser'); }
  get findingType() { return this.approveFindingForm.get('findingType'); }
  get expirationDate() { return this.approveFindingForm.get('expirationDate'); }
  get creationDate() { return this.approveFindingForm.get('creationDate'); }
  get causeAnalysis() { return this.approveFindingForm.get('causeAnalysis'); }
  get containmentAction() { return this.approveFindingForm.get('containmentAction'); }
  get comment() { return this.approveFindingForm.get('comment'); }
  
  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _findingsService: FindingsService,
    private _plantsService: PlantsService,
    private _findingTypesService : FindingsTypeService
  ) { }

 

  ngOnInit() {
    this.blockUI.start();
    this.approveFindingForm = this.modelCreate();
    this.sectorEmisor.disable();
    this.plantEmisor.disable();
    this.sectorTreatment.disable();
    this.plantTreatment.disable();
    this.findingState.disable();
    this.creationDate.disable();
    this.description.disable();
    this.findingType.disable();
    this.responsibleUser.disable();
    this.plantLocation.disable();
    this.sectorLocation.disable();
    this._plantsService.getAll()
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) => {
      this.allPlants = res;
      this._findingTypesService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.findingTypes = res;

        this._findingsService.get(this._route.snapshot.params.id)
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res: any) => {
          this.finding = res;
          this._findingEvidences = res.findingsEvidences.map(element => {
            
            element.id = element.findingEvidenceID;        
            element.name = element.fileName;  
            return element;
          });
          const that = this;
          this.comments = (res.findingComments as Array<any>).sort((a, b) => {if(b.findingCommentID > a.findingCommentID) return -1});
          this.formatDate();
          this.patchFinding();
          this._findingsService.getAllUsers(res.sectorTreatmentID, res.plantTreatmentID)
          .takeUntil(this.ngUnsubscribe)  
          .subscribe((users : any[]) => {
            users.forEach(function(us){
              if(us.active){
                that.responsibleUsers.push(us);
              }
            });
            console.log(this.responsibleUsers);
              this.responsibleUsers = users;
              this.patchUser();
              this.blockUI.stop();
            })
        });
      });
    });
  }

  formatDate(){
    this.comments.forEach((el, index) => {
      let d = new Date(el.date);
      let month = d.getMonth() + 1;
      el.date = d.getDate()+"/"+ month +"/"+d.getFullYear();
    })
  }

  patchUser(){
    this.responsibleUser.patchValue(this.finding.responsibleUserID);
    this.plantEmisor.patchValue(parseInt(this.finding.plantEmitterID));
    let pl = this.allPlants.find(x => x.plantID == parseInt(this.finding.plantEmitterID));

    this.sectorEmisors = pl.sectors;

    this.sectorEmisor.patchValue(parseInt(this.finding.sectorEmitterID));
  }

  patchFinding(){
    this.findingType.patchValue(this.finding.findingTypeID);
    this.findingState.patchValue(this.finding.findingStateName);
    this.description.patchValue(this.finding.description);
    this.plantEmisor.patchValue(this.finding.plantEmitterID);
    this.plantTreatment.patchValue(this.finding.plantTreatmentID);
    this.plantLocation.patchValue(this.finding.plantLocationID);

    this.changeSelectionTreatment(this.finding.plantTreatmentID);
    this.changeSelectionLocation(this.finding.plantLocationID);

    this.sectorEmisor.patchValue(this.finding.sectorEmitterID);
    this.sectorTreatment.patchValue(this.finding.sectorTreatmentID);
    this.sectorLocation.patchValue(this.finding.sectorLocationID);

    this.creationDate.patchValue(this.finding.createdDate.substring(0, 10));
    setValidatedDate(this.expirationDate, this.finding.expirationDate);
    this.causeAnalysis.patchValue(this.finding.causeAnalysis);
    this.containmentAction.patchValue(this.finding.containmentAction);


  }

  modelCreate(){
    return this.fb.group({
      description: ['', Validators.required],
      plantEmisor:['', Validators.required],
      sectorEmisor:['', [Validators.required]],
      plantTreatment:['', Validators.required],
      sectorTreatment:['', Validators.required],
      plantLocation:['', Validators.required],
      sectorLocation:['', Validators.required],
      responsibleUser:['', Validators.required],
      findingType:['', Validators.required],
      expirationDate:['', Validators.required],
      creationDate:[''],
      causeAnalysis:['', Validators.required],
      containmentAction:[''],
      findingState: [''],
      comment: ['']
    });
  }

  changeSelectionSectorTreatment(id){
    let plantId = this.plantTreatment.value;
    this.blockUI.start();
    const that = this;
    this._findingsService.getAllUsers(id, plantId)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any[]) => {
        this.responsibleUser.enable();
        this.responsibleUsers = res;
        res.forEach(function(user){
          if(user.active){
            that.responsibleUsers.push(user);
          }
        });
        console.log(this.responsibleUsers);
        let sector = this.sectorTreatments.find(x => x.id = id);

        this.responsibleUser.patchValue(sector.resposibleUserPlantSector);
        //console.log(res);
        this.blockUI.stop();
      });
  }
  changeSelectionTreatment(val){
    let pl = this.allPlants.find(x => x.plantID == val);
    this.plantTreatment.patchValue(val);
    this.sectorTreatments = pl.sectors;
  }

  changeSelectionLocation(val){
    let pl = this.allPlants.find(x => x.plantID == val);
    this.plantLocation.patchValue(val);
    this.sectorLocations = pl.sectors;
  }


  onSubmit(){
    if(!this.approveFindingForm.valid){
      return;
    }
    
    this.blockUI.start();
    let find = new Finding();
    find.description = this.description.value;
    find.expirationDate = this.expirationDate.value != null ? new Date(this.expirationDate.value).toUTCString() : new Date().toUTCString();
    find.findingTypeId = this.findingType.value;
    find.plantLocationId = this.plantEmisor.value;
    find.plantTreatmentId = this.plantTreatment.value;
    find.responsibleUserId = this.responsibleUser.value;
    find.sectorLocationId = this.sectorEmisor.value;
    find.sectorTreatmentId = this.sectorTreatment.value;
    find.causeAnalysis = this.causeAnalysis.value;
    find.containmentAction = this.containmentAction.value;
    find.comment = this.comment.value;
    find.findingEvidences = this._findingEvidences.filter(x => x.isInsert);
    find.fileNamesToDelete = this._findingEvidences.filter(x => x.isDelete).map(y => y.name);
    
    find.workflowId = this.finding.workflowId;
    find.findingStateID = this.finding.findingStateID;
    find.emitterUserID = this.finding.emitterUserID;
    find.findingId = this.finding.id;
    find.eventData = "UpdateApprovedFinding";
  
    this._findingsService.updateApprovedFindingStep(find)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this._toastrManager.successToastr('El hallazgo se ha actualizado correctamente', 'Éxito');
        this._router.navigate(['/quality/finding']);
        this.blockUI.stop();
      });
  }

  getAttachments(event) : void {
    this._findingEvidences = event;
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
