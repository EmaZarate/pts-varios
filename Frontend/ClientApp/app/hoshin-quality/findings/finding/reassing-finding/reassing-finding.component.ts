import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { FormGroup, FormControlName, Validators, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { FindingsService } from '../../../../core/services/findings.service'
import { AuthService } from "../../../../core/services/auth.service";

import { FindingReassignmentsHistory } from "../../models/FindingReassignmentsHistory";
import { Finding } from "../../models/Finding";
import { PlantsService } from 'ClientApp/app/core/services/plants.service';

@Component({
  selector: 'app-reassing-finding',
  templateUrl: './reassing-finding.component.html',
  styleUrls: ['./reassing-finding.component.css']
})
export class ReassingFindingComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  reassingFindingForm: FormGroup;
  _finding: any
  _users: any
  _userLogged: any;
  allPlants = [];
  sectorLocations = [];
  sectorTreatments = [];
  responsibleUsers = [];
  _findingReassignmentHistory: FindingReassignmentsHistory = new FindingReassignmentsHistory();
  get user() { return this.reassingFindingForm.get('user'); }
  get plantTreatment() { return this.reassingFindingForm.get('plantTreatment'); }
  get sectorTreatment() { return this.reassingFindingForm.get('sectorTreatment'); }
  get responsibleUser() { return this.reassingFindingForm.get('responsibleUser'); }

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _findingsService: FindingsService,
    private _authService: AuthService,
    private _plantsService: PlantsService,
  ) { }

  ngOnInit() {
    this.reassingFindingForm = this.modelCreate();
    this._userLogged = this._authService.getUserLogged();
    this.blockUI.start();
    this.sectorTreatment.disable();
    this.responsibleUser.disable();
    this._route.params.subscribe((res) => {
      this._findingsService.get(res.id)
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res: Finding) => {
          this._finding = res;
          this._plantsService.getAll()
                .takeUntil(this.ngUnsubscribe)
                .subscribe((res) => {
                  this.allPlants = res;
                  this.blockUI.stop();
            })
        })
    })
  }

  modelCreate() {
    return this.fb.group({
      // user: ['', Validators.required],
      plantTreatment: ['', Validators.required],
      sectorTreatment: ['', Validators.required],
      responsibleUser: ['', Validators.required],
    });
  }

  onSubmit() {
    if (!this.reassingFindingForm.valid) return;
    this.blockUI.start();
    this._findingReassignmentHistory.reassignedUserID = this.responsibleUser.value;
    this._findingReassignmentHistory.lastResponsibleUserID = this._finding.responsibleUserID;
    //
    this._findingReassignmentHistory.plantTreatmentID = this.plantTreatment.value;
    this._findingReassignmentHistory.sectorTreatmentID = this.sectorTreatment.value;
      //
    this._findingReassignmentHistory.createdByUserID = this._userLogged.id;
    this._findingReassignmentHistory.findingID = this._finding.id
    this._findingReassignmentHistory.workflowId = this._finding.workflowId;

    //if the user is SGC Responsable:
    //this._findingReassignmentHistory.EventData = "ReassignedWithoutApproval"
    this._findingReassignmentHistory.EventData = "Reassigned";

    this._findingsService.reassignFindingStep(this._findingReassignmentHistory)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this._toastrManager.successToastr('El hallazgo se ha reasignado correctamente', 'Éxito');
        this._router.navigate(['/quality/finding']);
        this.blockUI.stop();
      });
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

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
