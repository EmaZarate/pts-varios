import { Component, OnInit, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { AuditService } from '../../audit.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Audit } from '../../models/Audit';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ApproveRejectAudit } from '../../models/ApproveRejectAudit';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-approve-reject-audit-report',
  templateUrl: './approve-reject-audit-report.component.html',
  styleUrls: ['./approve-reject-audit-report.component.css']
})
export class ApproveRejectAuditReportComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  audit: Audit;
  approveRejectReportForm: FormGroup;

  get ctrFinalComment() { return this.approveRejectReportForm.get('ctrFinalComment'); }

  constructor(
    private _auditService: AuditService,
    private _route: ActivatedRoute,
    private _fb: FormBuilder,
    private _router: Router,
    private _toastrManager: ToastrManager,
  ) { }

  ngOnInit() {
    this.blockUI.start();

    this.approveRejectReportForm = this.modelCreate();
    this._auditService.get(this._route.snapshot.params.id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(res => {
        this.audit = res;
        this.blockUI.stop();

      })
  }

  modelCreate() {
    return this._fb.group({
      ctrFinalComment: ['', Validators.required]
    })
  }

  approveReport() {
    this.submitForm("Approve");
  }

  rejectReport() {
    this.submitForm("Reject");
  }

  submitForm(eventData) {

    if(!this.approveRejectReportForm.valid) return;

    this.blockUI.start();
    const approveRejectData: ApproveRejectAudit = {
      approveReportComments: this.ctrFinalComment.value,
      approvePlanComments: '',
      auditID: this.audit.auditID,
      auditStateID: 0,
      eventData: eventData,
      workFlowId: this.audit.workflowId
    };

    this._auditService.approveRejectAuditReport(approveRejectData)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(res => {
        this._toastrManager.successToastr(`El informe ha sido ${eventData == "Reject" ? 'rechazado' : 'aprobado'} correctamente`, 'Éxito');
        this.blockUI.stop();
        this._router.navigate(['/quality/audits/list']);
      })

  }


  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
}
  

}
