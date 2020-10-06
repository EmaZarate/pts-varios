import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { AuditService } from '../../audit.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject } from 'rxjs';
import { AuditStateCode } from '../../models/AuditStateCode';
import { AuditStandardAspectService } from '../../audit-standard-aspect.service';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Audit } from '../../models/Audit';
import { FindingsService } from 'ClientApp/app/core/services/findings.service';
import { FormBuilder, Validators, FormGroup, FormControl } from '@angular/forms';
import { SwalPartialTargets } from '@toverux/ngx-sweetalert2';
import * as moment from 'moment';

declare const $: any;

@Component({
  selector: 'app-detail-audit',
  templateUrl: './detail-audit.component.html',
  styleUrls: ['./detail-audit.component.css']
})
export class DetailAuditComponent implements OnInit, OnDestroy {

  
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  

  auditStateCode = AuditStateCode;

  constructor(
    private _route: ActivatedRoute,
    private _auditService: AuditService,
    private _auditStandardAspect: AuditStandardAspectService,
    private _toastrManager: ToastrManager,
    private _findingService: FindingsService,
    private _router: Router,
    private _fb: FormBuilder,
    public readonly swalTargets: SwalPartialTargets
  ) { }

  _audit: Audit;  
  emittingReport: boolean = false;
  standars;
  emitReportForm: FormGroup;
  auditStandardAspectSelected = [];

  ngOnInit() {
    this.blockUI.start();
    this._route.params
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this._auditService.get(res.id)
        .takeUntil(this.ngUnsubscribe)
        .subscribe(res => {
            this._audit = res; 
            console.log(this._audit);
            this.getAuditStandardAspects();
            console.log(this.auditStandardAspectSelected);
            this.PatchTime();
      })
      });
  }

  

  getAuditStandardAspects() {
    this._auditStandardAspect.getAllForAudit(this._audit.auditID)
    .takeUntil(this.ngUnsubscribe)
    .subscribe(res => {
      this.patchAspect(res);
      console.log(res);
      this.blockUI.stop();
    })
  }

  PatchTime(){
    this._audit.auditInitDate = new Date (moment.utc(this._audit.auditInitDate).toISOString())
    this._audit.auditInitTime = new Date (moment.utc(this._audit.auditInitTime).toISOString())
    this._audit.auditFinishDate = new Date (moment.utc(this._audit.auditFinishDate).toISOString())
    this._audit.auditFinishTime = new Date (moment.utc(this._audit.auditFinishTime).toISOString())
    this._audit.closeMeetingDate = new Date (moment.utc(this._audit.closeMeetingDate).toISOString())
    this._audit.documentsAnalysisDate = new Date (moment.utc(this._audit.documentsAnalysisDate).toISOString())
  }

  deleteFinding(findingId) {
    this.blockUI.start();
    this._auditStandardAspect.delete(findingId)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(res => {
        this.auditStandardAspectSelected = [];
        this.getAuditStandardAspects();
      })
  }

  setWithoutFindings(standardId, aspectId) {
    this.blockUI.start();
    this._auditStandardAspect.setWithoutFindings(this._audit.auditID, standardId, aspectId)
    .takeUntil(this.ngUnsubscribe)  
    .subscribe(res => {
        this.auditStandardAspectSelected = [];
        this.getAuditStandardAspects();
      });
  }

  setNoAudited(description, aspectStandard) {
    this.blockUI.start();
    this._auditStandardAspect.setNoAudited(this._audit.auditID, aspectStandard.standardID, aspectStandard.aspectID, description)
    .takeUntil(this.ngUnsubscribe)  
    .subscribe(res => {
        this.auditStandardAspectSelected = [];
        this.getAuditStandardAspects();
      });
  }



  patchAspect(auditStandardAspects) {
    auditStandardAspects.forEach(auditStandardAspect => {
      let idaspect = "#" + auditStandardAspect.aspectID
      $(idaspect).attr('checked', true);
      this.auditStandardAspectSelected.push(auditStandardAspect)
    });
    this._audit.auditStandard
  }

  completeAllAspects() {
    let canEmitReport = true;

    this.auditStandardAspectSelected.forEach(auditStandardAspect => {
      if (!(auditStandardAspect.noAudited || auditStandardAspect.withoutFindings || auditStandardAspect.findings.length > 0)) {
        canEmitReport = false;
      }
    });

    return canEmitReport;
  }



  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
