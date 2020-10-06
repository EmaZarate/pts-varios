import { Component, OnInit, ViewChildren, ElementRef, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrManager } from 'ng6-toastr-notifications';
import { AuditService } from '../../audit.service';
import { ApproveRejectAudit } from '../../models/ApproveRejectAudit'

@Component({
  selector: 'app-approve-reject-audit',
  templateUrl: './approve-reject-audit.component.html',
  styleUrls: ['./approve-reject-audit.component.css']
})
export class ApproveRejectAuditComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  approveRejectAuditForm: FormGroup;
  get ctrFinalComment() { return this.approveRejectAuditForm.get('ctrFinalComment'); }
  
//   _auditId: any;

  audit;

  constructor(
      private _route: ActivatedRoute,
      private _router: Router,
      private fb: FormBuilder,
      private _toastrManager: ToastrManager,
      private _auditService: AuditService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    
    this.approveRejectAuditForm = this.modelCreate();
    this._auditService.get(this._route.snapshot.params.id)
        .takeUntil(this.ngUnsubscribe)
        .subscribe(res => {
            this.audit = res;
            this.blockUI.stop();
            console.log(res);
            
        })
}

ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
}

modelCreate() {
    return this.fb.group({
        ctrFinalComment: ['', Validators.required],
        
    });
}

onSubmit() {
    var buttonName = document.activeElement.getAttribute("Name");    

    if (this.approveRejectAuditForm.valid) {
        this.blockUI.start();
        let approveRejectAudit = new ApproveRejectAudit();
        approveRejectAudit.approvePlanComments = this.ctrFinalComment.value;
        approveRejectAudit.auditStateID = buttonName == "reject" ? 3 : 4;
        approveRejectAudit.eventData = buttonName == 'reject' ? "Reject" : "Approve";
        approveRejectAudit.auditID = this.audit.auditID;
        approveRejectAudit.workFlowId = this.audit.workflowId; 

        this._auditService.approveRejectAudit(approveRejectAudit)
        .takeUntil(this.ngUnsubscribe)
        .subscribe(() => {
            this._toastrManager.successToastr(`La auditoría se ha ${buttonName == "reject" ? 'rechazado' : 'aprobado'} con éxito`, 'Éxito');
            this._router.navigate(['/quality/audits/list']);
            this.blockUI.stop();
        });
    }
}

}
