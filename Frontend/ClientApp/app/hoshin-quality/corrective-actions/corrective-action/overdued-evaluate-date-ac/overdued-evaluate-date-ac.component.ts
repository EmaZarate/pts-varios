
import { Component, OnInit, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Router, ActivatedRoute } from '@angular/router';
import {CorrectiveActionService} from '../../corrective-action.service';
import {RequestExtension} from '../../../../shared/models/requestExtension';
import { Subject } from 'rxjs';
@Component({
  selector: 'app-overdued-evaluate-date-ac',
  templateUrl: './overdued-evaluate-date-ac.component.html',
  styleUrls: ['./overdued-evaluate-date-ac.component.css']
})
export class OverduedEvaluateDateACComponent implements OnInit, OnDestroy {
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  requestExtension;
  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private _correctiveActionsService : CorrectiveActionService) { }

  ngOnInit() {
  }
  submit(event: RequestExtension) {
    this.blockUI.start();
    this._route.params
    .takeUntil(this.ngUnsubscribe)
    .subscribe(res => {
      const CorrectiveActionID = res.id;
      this.requestExtension = { date: event.date.toDateString() , observation: event.observation, CorrectiveActionID };
      this._correctiveActionsService.overdueEvaluatedDate(this.requestExtension)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(resp => {
        this._router.navigate(['/quality/corrective-actions/list']);
        this.blockUI.stop();
        this._toastrManager.successToastr(
          'Se envió correctamente la solicitud de Extensión'
        );
      });
    });

  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}


