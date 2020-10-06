import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { FindingsService } from '../../../../core/services/findings.service'
import { AuthService } from "../../../../core/services/auth.service";

import { Finding } from "../../models/Finding";
import { FindingReassignmentsHistory } from "../../models/FindingReassignmentsHistory";

@Component({
  selector: 'app-approve-reassignment',
  templateUrl: './approve-reassignment.component.html',
  styleUrls: ['./approve-reassignment.component.css']
})
export class ApproveReassignmentComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  _finding: any
  _userLogged : any;
  _findingReassignmentHistory: FindingReassignmentsHistory = new FindingReassignmentsHistory();
  _reassignmentUser : any;
  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private _findingsService: FindingsService,
    private _authService: AuthService,
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this._route.params
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res) => {
      this._findingsService.get(res.id)
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res: Finding) => {
          this._finding = res;
          console.log(this._finding);
          this._findingsService.getLastReassignment(this._finding.id)
            .takeUntil(this.ngUnsubscribe)
            .subscribe((res: any) => {
              this._findingReassignmentHistory = res;
              console.log(this._findingReassignmentHistory);
              this._findingsService.getOneUser(this._findingReassignmentHistory.reassignedUserID)
                .takeUntil(this.ngUnsubscribe)
                .subscribe((res: any) => {
                  this._reassignmentUser = res
                  this._userLogged = this._authService.getUserLogged();
                  this.blockUI.stop();
                })
            })
        })
    })
  }

  approve() {
    this.blockUI.start();
    this._findingReassignmentHistory.createdByUserID = this._userLogged.id;
    this._findingReassignmentHistory.workflowId = this._finding.workflowId;
    this._findingReassignmentHistory.lastResponsibleUserID = this._finding.responsibleUserID;
    this._findingReassignmentHistory.EventData = "ApproveReassignment";
    this._findingReassignmentHistory.plantTreatmentID = this._finding.plantTreatmentID;
    this._findingReassignmentHistory.sectorTreatmentID = this._finding.sectorTreatmentID;
    console.log(this._findingReassignmentHistory);
    this._findingsService.approveOrRejectReassignment(this._findingReassignmentHistory,this._userLogged.id).subscribe((res:any)=>{
      this._toastrManager.successToastr('La reasignación del hallazgo se ha aprobado correctamente', 'Éxito');
      this._router.navigate(['/quality/finding']);
      this.blockUI.stop();
    }) 
  }
  reject(){
    this._router.navigate(['/quality/finding',this._finding.id, 'rejectreassignment']);
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
