import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { Router, ActivatedRoute } from "@angular/router";
import { Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { FindingsService } from "../../../../core/services/findings.service";
import { FindingEvidence } from '../../models/FindingEvidence';

@Component({
  selector: 'app-reject-finding',
  templateUrl: './reject-finding.component.html',
  styleUrls: ['./reject-finding.component.css']
})
export class RejectFindingComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  finding: any;
  _findingEvidences: FindingEvidence[];

  constructor(
    private _router: Router,
    private _route: ActivatedRoute,
    private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _findingsService: FindingsService) { }

  rejectFindingForm: FormGroup;

  get comment() { return this.rejectFindingForm.get('comment'); }


  ngOnInit() {
    this.blockUI.start();
    this.rejectFindingForm = this.modelCreate();
    this._findingsService.get(this._route.snapshot.params.id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this.finding = res;
        this._findingEvidences = res.findingsEvidences.map(element => {          
          element.id = element.findingEvidenceID;
          element.name = element.fileName;
          return element;
        });
        
        //console.log(this.finding);
        this.blockUI.stop();
      });
  }

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  modelCreate() {
    return this.fb.group({
      comment: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.rejectFindingForm.valid) {
      this.blockUI.start();
      this.finding.finalComment = this.comment.value;
      this.finding.EventData = "Rejected";
      this.finding.FindingId = this.finding.id;
      this.finding.findingEvidences = this._findingEvidences.filter(x => x.isInsert);
      this.finding.fileNamesToDelete = this._findingEvidences.filter(x => x.isDelete).map(y => y.name);
      //console.log(this.finding);
      this._findingsService.approveFindingStep(this.finding).subscribe(() => {
        this._toastrManager.successToastr('El hallazgo se ha rechazado correctamente', 'Éxito');
        this._router.navigate(['/quality/finding']);
        this.blockUI.stop();
      });
    }
  }

}
