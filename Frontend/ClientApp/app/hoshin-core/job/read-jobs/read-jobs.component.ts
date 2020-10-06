import { Component, OnInit, Injector, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs';

import { BlockUI, NgBlockUI } from 'ng-block-ui';
import { ToastrManager } from 'ng6-toastr-notifications';

import { Job } from '../../models/Job';

import { JobsService } from '../../../core/services/jobs.service';

import * as PERMISSIONS from '../../../core/permissions/index';
import { AuthService } from 'ClientApp/app/core/services/auth.service';

@Component({
  selector: 'app-read-jobs',
  templateUrl: './read-jobs.component.html',
  styleUrls: ['./read-jobs.component.css']
})
export class ReadJobsComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  jobs: Job[] = [];

  canSwitch;
  canEdit;
  canAdd;

  constructor(
    private _jobsService: JobsService,
    private _toastrManager: ToastrManager,
    private _authService: AuthService
  ) { }

  ngOnInit() {
    this.loadJobsList();
    this.setPermissions();
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  updateJobState(job: Job){
    this.blockUI.start();
    job.active = !job.active;
    this._jobsService.update(job)
      .takeUntil(this.ngUnsubscribe)
      .subscribe(() => {
        this._toastrManager.successToastr('El puesto se ha actualizado correctamente', 'Éxito');
        this.loadJobsList();
        this.blockUI.stop();
      });
  }

  loadJobsList(){
    this.blockUI.start();
    this._jobsService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res: Job[]) => {
        this.jobs = res;
        this.blockUI.stop();
      });
  }

  setPermissions(){
    this.canAdd = this._canAdd();
    this.canEdit = this._canEdit();
    this.canSwitch = this._canSwitch();
  }

  private _canSwitch(){
    return this._authService.hasClaim(PERMISSIONS.JOB.DEACTIVATE_JOB) && this._authService.hasClaim(PERMISSIONS.JOB.ACTIVATE_JOB)
  }

  private _canEdit(){
    return this._authService.hasClaim(PERMISSIONS.JOB.EDIT_JOB);
  }

  private _canAdd(){
    return this._authService.hasClaim(PERMISSIONS.JOB.ADD_JOB);
  }
}
