import { Component, OnInit, OnDestroy } from '@angular/core';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { TaskConfigService } from '../../../corrective-actions/task.service';
import { ActivatedRoute } from '@angular/router';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-detail-task',
  templateUrl: './detail-task.component.html',
  styleUrls: ['./detail-task.component.css']
})
export class DetailTaskComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  _task;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  constructor(
    private _route: ActivatedRoute,
    private _taskService: TaskConfigService,
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this._taskService.Get(this._route.snapshot.params.id)
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: any) => {
        this._task = res; 
        console.log(this._task);
        this.blockUI.stop();
      });
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}


