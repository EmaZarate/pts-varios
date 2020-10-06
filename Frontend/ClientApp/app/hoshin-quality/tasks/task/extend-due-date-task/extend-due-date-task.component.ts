import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrManager } from 'ng6-toastr-notifications';
import { TaskService } from '../../taskService.service';
import { Task } from '../../models/Task';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-extend-due-date-task',
  templateUrl: './extend-due-date-task.component.html',
  styleUrls: ['./extend-due-date-task.component.css']
})
export class ExtendDueDateTaskComponent implements OnInit, OnDestroy {
  constructor(private _route: ActivatedRoute,private fb: FormBuilder,
    private _toastrManager: ToastrManager,
    private _taskService: TaskService,
    private _router: Router
    ) { }

  get newDate() { return this.extendDueDateForm.get('newDate') };

  date
  task: Task;
  extendDueDateForm: FormGroup
  typeDate: string;

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();

  ngOnInit() {
    this.task = this._route.snapshot.data.task;
    this.extendDueDateForm = this.modelCreate();
  }

  modelCreate() {
    return this.fb.group({
      newDate: ['', Validators.required]
    });
  }

  validDate() {
    if (this.extendDueDateForm.valid) {
      let newDate = new Date(this.newDate.value);
      newDate.setHours(0, 0, 0, 0);
      let today = new Date();
      if (newDate.getTime() > today.getTime()) {
        return true;
      }
      this._toastrManager.errorToastr('Ingrese una fecha mayor a la de hoy', 'Error');
      return false;
    }
    else {
      this._toastrManager.errorToastr('Ingrese una fecha', 'Error');
    }
  }

  onSubmit(){
    if(this.validDate()){
      this.blockUI.start();
      this.task.implementationPlannedDate = this.newDate.value;
        this._taskService.extendDueDate(this.task)
        .takeUntil(this.ngUnsubscribe)
        .subscribe(() =>{
          this.blockUI.stop();
          this._toastrManager.successToastr('Se ha extendido la fecha de vencimiento, Éxito');
          this._router.navigate(['/quality/tasks/list']);
        });
    }
  }
  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
