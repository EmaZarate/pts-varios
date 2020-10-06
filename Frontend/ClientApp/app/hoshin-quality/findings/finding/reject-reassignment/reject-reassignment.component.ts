import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { FindingsService } from "../../../../core/services/findings.service";
import { AuthService } from "../../../../core/services/auth.service";

import { FindingReassignmentsHistory } from "../../models/FindingReassignmentsHistory";

@Component({
  selector: 'app-reject-reassigment',
  templateUrl: './reject-reassignment.component.html',
  styleUrls: ['./reject-reassignment.component.css']
})
export class RejectReassignmentComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  finding: any;
  _findingReassignmentHistory: FindingReassignmentsHistory = new FindingReassignmentsHistory();
  _userLogged: any;
  

  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _findingsService: FindingsService,
    private _authService: AuthService) { }

  rejectFindingForm: FormGroup;

  get comment() { return this.rejectFindingForm.get('comment'); }


  ngOnInit() {
    this.blockUI.start();
    this.rejectFindingForm = this.modelCreate();
    this._findingsService.get(this._route.snapshot.params.id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this.finding = res;
        //console.log(this.finding);
        this._findingsService.getLastReassignment(this.finding.id)
          .takeUntil(this.ngUnsubscribe)
          .subscribe((res: any) => {
            this._findingReassignmentHistory = res;
            //console.log(this._findingReassignmentHistory);
            this._userLogged = this._authService.getUserLogged();
            this.blockUI.stop();
          })
      })
  }



  modelCreate() {
    return this.fb.group({
      comment: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.rejectFindingForm.valid) {
      this.blockUI.start();
      this._findingReassignmentHistory.rejectComment = this.comment.value;
      this._findingReassignmentHistory.workflowId = this.finding.workflowId;
      this._findingReassignmentHistory.createdByUserID = this._userLogged.id;
      this._findingReassignmentHistory.EventData = "NotApproveReassignment";

      //this._findingReassignmentHistory.createdByUserID = this._userLogged.id;
      this._findingsService.approveOrRejectReassignment(this._findingReassignmentHistory,this._userLogged.id).subscribe((res: any) => {
        this._toastrManager.successToastr('La reasignación del hallazgo se ha rechazado correctamente', 'Éxito');
        this._router.navigate(['/quality/finding']);
        this.blockUI.stop();
      })
    }
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }


}
