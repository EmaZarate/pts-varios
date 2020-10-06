import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { FormGroup, FormControlName, FormBuilder, Validators, FormControl } from '@angular/forms';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { CorrectiveAction } from '../../models/CorrectiveAction';
import { Router } from '@angular/router';
import { ToastrManager } from 'ng6-toastr-notifications';
import { PlantsService } from 'ClientApp/app/core/services/plants.service';
import { FindingsService } from 'ClientApp/app/core/services/findings.service';
import { UsersService } from 'ClientApp/app/core/services/users.service';
import { CorrectiveActionService } from '../../corrective-action.service';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-schedule-corrective-actions',
  templateUrl: './schedule-corrective-actions.component.html',
  styleUrls: ['./schedule-corrective-actions.component.css']
})
export class ScheduleCorrectiveActionsComponent implements OnInit, OnDestroy {

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  
  correctiveActionForm: FormGroup;
  relatedFindingType = new FormControl();

  _correctiveAction = new CorrectiveAction();

  get description() { return this.correctiveActionForm.get('description'); }
  get relatedFinding() { return this.correctiveActionForm.get('relatedFinding'); }
  // get emissionPlant() { return this.correctiveActionForm.get('emissionPlant'); }
  // get emissionSector() { return this.correctiveActionForm.get('emissionSector'); }
  get locationPlant() { return this.correctiveActionForm.get('locationPlant'); }
  get locationSector() { return this.correctiveActionForm.get('locationSector'); }
  get treatmentPlant() { return this.correctiveActionForm.get('treatmentPlant'); }
  get treatmentSector() { return this.correctiveActionForm.get('treatmentSector'); }
  get responsibleUser() { return this.correctiveActionForm.get('responsibleUser'); }
  get evaluatorSector() { return this.correctiveActionForm.get('evaluatorSector'); }
  get evaluatorPlant() { return this.correctiveActionForm.get('evaluatorPlant'); }
  get reviewer() { return this.correctiveActionForm.get('reviewer'); }

  constructor(
    private _router: Router,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _correctiveActionService: CorrectiveActionService,
    private _plantService: PlantsService,
    private _findingService: FindingsService,
    private _userService: UsersService
  ) { }

  allPlants = [];
  allFindings = [];
  responsibleUsers = [];
  evaluatorUsers = [];

  // emissionSectors = [];
  locationSectors = [];
  treatmentSectors = [];
  evaluatorSectors = [];

  ngOnInit() {
    this.blockUI.start();
    this.correctiveActionForm = this.modelCreate();
    // this.emissionSector.disable();
    this.locationSector.disable();
    this.treatmentSector.disable();
    this.responsibleUser.disable();
    this.reviewer.disable();
    //evaluator
    this.evaluatorSector.disable();

    //Recuperar solo las activas ??
    this._plantService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.allPlants = res;
        this.blockUI.stop();
      });

    //Recupera los que no estén 1. En espera de aprobación, 2. Finalizado OK, 3. Finalizado No OK, 4. Cerrado, 5. Vencido
    this._findingService.getAllApprovedInProgress()
     .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.allFindings = res;
        this.blockUI.stop();
      });

      //Recuperar todos los usuarios ??

      this.relatedFinding.valueChanges
      .takeUntil(this.ngUnsubscribe)
      .subscribe((val) => {
        let fd = this.allFindings.find(x => x.id == val);
        this.relatedFindingType.patchValue(fd.findingTypeName);
      });
  }

  modelCreate(){
    return this.fb.group({
      description: ['', Validators.required],
      relatedFinding: [''],
      // emissionPlant: ['', Validators.required],
      // emissionSector: ['', Validators.required],
      locationPlant: ['', Validators.required],
      locationSector: ['', Validators.required],
      treatmentPlant: ['', Validators.required],
      treatmentSector: ['', Validators.required],
      responsibleUser: ['', Validators.required],
      evaluatorPlant: ['', Validators.required],
      evaluatorSector: ['', Validators.required],
      reviewer: ['', Validators.required]
    });
  }

  // changeSelectionEmissionPlant(val){
  //   let pl = this.allPlants.find(x => x.plantID == val);
  //   this.emissionPlant.patchValue(val);
  //   this.emissionSectors = pl.sectors;
  //   this.emissionSector.enable();
  // }

  changeSelectionLocationPlant(val){
    let pl = this.allPlants.find(x => x.plantID == val);
    this.locationPlant.patchValue(val);
    this.locationSectors = pl.sectors;
    this.locationSector.enable();
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
    this._findingService.getAllUsers(val, plantId)
     .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any[]) => {
        this.responsibleUser.enable();
        res.forEach(function(user){
          if(user.active){
            that.evaluatorUsers.push(user);
          }
        });
        console.log(this.evaluatorUsers);
        let sector = this.evaluatorSectors.find(x => x.id = val);
        this.reviewer.enable();
        this.blockUI.stop();
      });
  }
  changeSelectionTreatmentPlant(val){
    let pl = this.allPlants.find(x => x.plantID == val);
    this.treatmentPlant.patchValue(val);
    this.treatmentSectors = pl.sectors;
    this.treatmentSector.enable();
  }
  changeSelectionTreatmentSector(val){
    this.blockUI.start();
    let plantId = this.treatmentPlant.value;
    this.responsibleUsers = [];
    const that = this;
    this._findingService.getAllUsers(val, plantId)
     .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any[]) => {
        this.responsibleUser.enable();
        res.forEach(function(user){
          if(user.active){
            that.responsibleUsers.push(user);
          }
        });
        console.log(this.responsibleUsers);
        let sector = this.treatmentSectors.find(x => x.id = val);
        // this.responsibleUser.patchValue(sector.resposibleUserPlantSector);
        this.blockUI.stop();
      });
  }

  changeResponsibleUserOrReviewerUser(val, userType){
     if(this.responsibleUser.value == this.reviewer.value){
       this._toastrManager.errorToastr('El usuario responsable no puede ser el mismo que el usuario evaluador', 'Error');
       if(userType === 'responsive'){
         this.responsibleUser.patchValue('');
       }
       else{
         this.reviewer.patchValue('');
       }
     }
  }

  onSubmit() {
    this.blockUI.start();
    if(this.correctiveActionForm.valid){
      this._correctiveAction.description = this.description.value;
      this._correctiveAction.RelatedFindingId = this.relatedFinding.value;
      // this._correctiveAction.EmissionPlantId = this.emissionPlant.value;
      // this._correctiveAction.EmissionSectorId = this.emissionSector.value;
      this._correctiveAction.plantLocationID = this.locationPlant.value;
      this._correctiveAction.sectorLocationID = this.locationSector.value;
      this._correctiveAction.plantTreatmentID = this.treatmentPlant.value;
      this._correctiveAction.sectorTreatmentID = this.treatmentSector.value;
      this._correctiveAction.responsibleUserID = this.responsibleUser.value;
      this._correctiveAction.reviewerUserID = this.reviewer.value;
      this._correctiveAction.correctiveActionStateID = 1; //Abierta
      this._correctiveAction.creationDate = new Date();
      this._correctiveActionService.add(this._correctiveAction)
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this._toastrManager.successToastr('La acción correctiva se ha creado correctamente', 'Éxito');
          this._router.navigate(['/quality/corrective-actions/list']);
          this.blockUI.stop();
        });
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
