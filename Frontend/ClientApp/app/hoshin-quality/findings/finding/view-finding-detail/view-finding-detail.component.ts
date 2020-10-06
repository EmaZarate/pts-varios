import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { NgBlockUI, BlockUI } from 'ng-block-ui';

import { FindingsService } from '../../../../core/services/findings.service';
import { FindingEvidence } from '../../models/FindingEvidence';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-view-finding-detail',
  templateUrl: './view-finding-detail.component.html',
  styleUrls: ['./view-finding-detail.component.css']
})
export class ViewFindingDetailComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  finding;
  _findingEvidences: FindingEvidence[] = [];
  comments = [];

  constructor(
    private _route: ActivatedRoute,
    private _findingsService: FindingsService,
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this._findingsService.get(this._route.snapshot.params.id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this.finding = res;
        this._findingEvidences = res.findingsEvidences;
        this.comments = (res.findingComments as Array<any>).sort((a, b) => b.findingCommentID - a.findingCommentID );
        this.formatDate();  
        this.blockUI.stop();
      });
  }

  formatDate(){
    this.comments.forEach((el, index) => {
      let d = new Date(el.date);
      el.date = d.getDate() + "/" + d.getMonth() + "/" + d.getFullYear();
    })
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
