import { Component, OnInit, ViewChildren, ElementRef, ViewChild, OnDestroy } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { FindingsService } from '../../../../core/services/findings.service';
import { PlantsService } from '../../../../core/services/plants.service';
import { FindingsTypeService } from '../../../../core/services/findings-type.service';

import { _MatChipListMixinBase } from '@angular/material';
import { Finding } from '../../models/Finding';
import { FindingEvidence } from '../../models/FindingEvidence';
import { Subject } from 'rxjs';
import { TTBody } from 'primeng/primeng';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-create-finding',
  templateUrl: './create-finding.component.html',
  styleUrls: ['./create-finding.component.css']
})
export class CreateFindingComponent implements OnInit, OnDestroy {

  @ViewChild('file') file;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  
  findingForm: FormGroup;

  _finding = new Finding();
  _findingEvidences: FindingEvidence[] = [];

  get description() { return this.findingForm.get('description'); }
  get plantLocation() { return this.findingForm.get('plantLocation'); }
  get sectorLocation() { return this.findingForm.get('sectorLocation'); }
  get plantTreatment() { return this.findingForm.get('plantTreatment'); }
  get sectorTreatment() { return this.findingForm.get('sectorTreatment'); }
  get responsibleUser() { return this.findingForm.get('responsibleUser'); }
  get findingType() { return this.findingForm.get('findingType'); }

  constructor(
    private _router : Router,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _findingsService: FindingsService,
    private _plantsService: PlantsService,
    private _findingTypesService : FindingsTypeService,
    private _authService: AuthService

  ) { }

  allPlants = [];

  responsibleUsers = [];

  findingTypes = [];

  sectorLocations = [];
  sectorTreatments = [];

  ngOnInit() {
    
    this.blockUI.start();
    this.findingForm = this.modelCreate();  
    this.sectorLocation.disable();
    this.sectorTreatment.disable();
    this.responsibleUser.disable();
    this._plantsService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.allPlants = res;
        this.blockUI.stop();
      });
    this._findingTypesService.getAllActives()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any[]) => {
        this.findingTypes = res;
        this.blockUI.stop();
      });
  }

  modelCreate(){
    
    return this.fb.group({
      description: ['', Validators.required],
      plantLocation:['', Validators.required],
      sectorLocation:['', Validators.required],
      plantTreatment:['', Validators.required],
      sectorTreatment:['', Validators.required],
      responsibleUser:['', Validators.required],
      findingType:['', Validators.required],
    });
  }

  changeSelectionLocation(val){
    let pl = this.allPlants.find(x => x.plantID == val);
    this.plantLocation.patchValue(val);
    this.sectorLocations = pl.sectors;
    this.sectorLocation.enable();
  }

  changeSelectionTreatment(val){
    let pl = this.allPlants.find(x => x.plantID == val);
    this.plantTreatment.patchValue(val);
    this.sectorTreatments = pl.sectors;
    this.sectorTreatment.enable();
  }

  changeSelectionSectorTreatment(id){
    let plantId = this.plantTreatment.value;
    this.blockUI.start();
    this.responsibleUsers = [];
    const that = this;
    this._findingsService.getAllUsers(id, plantId)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any[]) => {
        this.responsibleUser.enable();
        res.forEach(function(user){
          if(user.active){
            that.responsibleUsers.push(user);
          }
        });
        console.log(this.responsibleUsers);
        this.blockUI.stop();
      });
  }

  onSubmit(){
    if(this.findingForm.valid){
      debugger
      this.blockUI.start();
      this._finding.findingEvidences = this._findingEvidences;
      this._finding.description = this.description.value;
      this._finding.plantLocationId = this.plantLocation.value;
      this._finding.sectorLocationId = this.sectorLocation.value;
      this._finding.plantTreatmentId = this.plantTreatment.value;
      this._finding.sectorTreatmentId = this.sectorTreatment.value;
      this._finding.responsibleUserId = this.responsibleUser.value;
      this._finding.findingTypeId = this.findingType.value;
      this._finding.findingStateID = "10";//En espera de aprobacion
      //this._finding.EmitterUserID = logedUserID
      var userLogged = this._authService.getUserLogged();
      this._finding.emitterUserID = userLogged.id;
      this._finding.findingStateID = '10';
      this._findingsService.add(this._finding)
       .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          //success
          this._toastrManager.successToastr('El hallazgo se ha creado correctamente', 'Éxito');
          this._router.navigate(['/quality/finding']);
          //console.log(res);
          this.blockUI.stop();
        });
    }else {
      this._toastrManager.errorToastr('Ingrese todos los campos obligatorios', 'Error');
    }
   
  }

  getAttachments(event) : void {
    this._findingEvidences = event;
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }


}
