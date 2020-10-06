import { Component, OnInit, OnDestroy } from '@angular/core';

import { ClaimsService } from '../../../core/services/claims.service';

import { FindingClaims } from '../../models/claims/FindingClaims';
import { CoreClaims } from '../../models/claims/CoreClaims';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

@Component({
  selector: 'app-read-claims',
  templateUrl: './read-claims.component.html',
  styleUrls: ['./read-claims.component.css']
})
export class ReadClaimsComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  
  findingClaims : FindingClaims;
  coreClaims : CoreClaims;
  allClaims = []
  constructor(
    private _claimsService: ClaimsService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.getClaims();
  }

  getClaims(){
    this._claimsService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: Array<any>) => {
        this.allClaims = res;
        this.blockUI.stop();
      });
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
