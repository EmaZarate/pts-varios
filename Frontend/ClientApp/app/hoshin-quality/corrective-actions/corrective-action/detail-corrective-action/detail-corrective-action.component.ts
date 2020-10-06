import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { CorrectiveActionService } from '../../corrective-action.service';
import { ActivatedRoute } from '@angular/router';
import { CorrectiveActionStateCode } from '../../models/CorrectiveActionStateCode';
import { Subject } from 'rxjs';
import { SwalPartialTargets } from '@toverux/ngx-sweetalert2';

@Component({
  selector: 'app-detail-corrective-action',
  templateUrl: './detail-corrective-action.component.html',
  styleUrls: ['./detail-corrective-action.component.css']
})
export class DetailCorrectiveActionComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  _correctiveAction;
  correctiveActionStateCode = CorrectiveActionStateCode;
  fishboneCategories
  constructor(
    private _route: ActivatedRoute,
    private _correctiveActionService: CorrectiveActionService,
    public readonly swalTargets: SwalPartialTargets
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.fishboneCategories = this._route.snapshot.data.categories.slice(0, 6).map((el, index) => {
      return {
        id: `id${el.fishboneID}`,
        name: el.name,
        position: index > 2 ? 'bottom' : 'top'
      }
    });
    console.log(this.fishboneCategories);
    this._correctiveActionService.getOne(this._route.snapshot.params.id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this._correctiveAction = res;
        console.log(this._correctiveAction);
        this.blockUI.stop();
      });
  }
  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}

