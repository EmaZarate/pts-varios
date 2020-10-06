import { Component, OnInit, OnDestroy } from '@angular/core';
import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';
import { Router, ActivatedRoute } from '@angular/router';
import { TaskService } from '../../taskService.service';
import {RequestExtension} from '../../../../shared/models/requestExtension';
import { Subject } from 'rxjs';
@Component({
  selector: 'app-overdue-task',
  templateUrl: './overdue-task.component.html',
  styleUrls: ['./overdue-task.component.css']
})
export class OverdueTaskComponent implements OnInit, OnDestroy {
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  requestExtension;

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private _taskService: TaskService
  ) {}

  ngOnInit() {
  }


  Submit(event: RequestExtension) {
    
    this.blockUI.start();
    this._route.params
    .takeUntil(this.ngUnsubscribe)
    .subscribe(res => {
      const TaskID = res.id;
      const date =  event.date.toDateString();
      this.requestExtension = { overdureTime: date, observation: event.observation, TaskID };
      this._taskService
        .overdueExtend(this.requestExtension)
        .takeUntil(this.ngUnsubscribe)
        .subscribe(resp => {
          this._router.navigate(['/quality/tasks/list']);
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
