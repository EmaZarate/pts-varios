import { Component, OnInit, Injector, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

import { ToastrManager } from 'ng6-toastr-notifications';

import { Job } from '../../models/Job';

import { JobsService } from '../../../core/services/jobs.service';
import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-create-edit-job',
  templateUrl: './create-edit-job.component.html',
  styleUrls: ['./create-edit-job.component.css']
})
export class CreateEditJobComponent implements OnInit, OnDestroy {

  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  jobForm: FormGroup;

  get name() { return this.jobForm.get('name'); }
  get code() { return this.jobForm.get('code'); }

  isCreate: boolean;
  job = new Job();

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _toastrManager: ToastrManager,
    private fb: FormBuilder,
    private _jobService: JobsService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this.jobForm = this.modelCreate();

    this._route.data
    .takeUntil(this.ngUnsubscribe)
    .subscribe((data) => {
      if(data.typeForm == 'new'){
        this.isCreate = true;
        this.blockUI.stop();
      }
      else{
        this.isCreate = false;
        this._route.params
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this.getJob(res.id);
        });
      }
    });
  }

  getJob(id){
    this._jobService.getOne(id)
    .takeUntil(this.ngUnsubscribe)
    .subscribe((res: Job) => {
      this.job = res;
      this.name.patchValue(this.job.name);
      this.code.patchValue(this.job.code);
      this.blockUI.stop();
    });
  }

  modelCreate() {
    return this.fb.group({
      name: ['', Validators.required],
      code: ['', Validators.required]
    });
  }

  onSubmit(){
    if(this.jobForm.valid){
      this.blockUI.start();
  
      this.job.name = this.name.value;
      this.job.code = this.code.value;
      
      if(this.isCreate){
        this.job.active = true;
        this._jobService.add(this.job)
          .takeUntil(this.ngUnsubscribe)
          .subscribe(() => {
            this._toastrManager.successToastr('El puesto se ha creado correctamente', 'Éxito');
            this._router.navigate(['/core/jobs']);
            this.blockUI.stop();
          });
      }
      else{
        this._jobService.update(this.job)
          .takeUntil(this.ngUnsubscribe)
          .subscribe(() => {
            this._toastrManager.successToastr('El puesto se ha actualizado correctamente', 'Éxito');
            this._router.navigate(['/core/jobs']);
            this.blockUI.stop();
          });
      }
    }
  }
  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
