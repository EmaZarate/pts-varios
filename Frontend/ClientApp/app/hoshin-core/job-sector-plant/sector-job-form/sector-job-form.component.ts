import { Component, OnInit, Input, Output, ViewChildren, EventEmitter, OnDestroy } from '@angular/core';

import { JobsService } from '../../../core/services/jobs.service';

import { NgBlockUI, BlockUI } from 'ng-block-ui';
import { Subject } from 'rxjs';

declare var $:any;

@Component({
  selector: 'app-sector-job-form',
  templateUrl: './sector-job-form.component.html',
  styleUrls: ['./sector-job-form.component.css']
})
export class SectorJobFormComponent implements OnInit, OnDestroy {

  @Input() jobs: Array<any> = [];
  @Input() sector;
  @Input() disabled: boolean;
  @Output() jobsSelectedEmitter = new EventEmitter<any>();
  @ViewChildren('jobsChecks') jobsChecks;
  @BlockUI() blockUI: NgBlockUI;
  private ngUnsubscribe: Subject<void> = new Subject<void>();
  
  jobSelected = [];
  allJobs =[];

  constructor(
    private _jobsService: JobsService
  ) { }

  ngOnInit() {
    this.blockUI.start();
    this._jobsService.getAll()
      .takeUntil(this.ngUnsubscribe)
      .subscribe((res) => {
        this.allJobs = res;
        this.blockUI.stop();
      })
  }

  ngAfterViewInit(){
    this.jobsChecks.changes
      .takeUntil(this.ngUnsubscribe)
      .subscribe(() => {
        this.patchJobs();
        this.blockUI.stop();
      })
  }

  patchJobs(){
    this.blockUI.start();
    this.jobSelected = [];  

    this.unselectChecks(this.disabled);
    this.selectChecks();
  }

  private unselectChecks(disabled){
    $('.jobCheck').each((index, el) => {
      el.disabled = disabled;
      el.checked = false;

      let $formCheck = $(el).parents('.form-check');
      if (disabled) {
        $formCheck.removeClass('enabled');
      } 
      else {
        $formCheck.addClass('enabled');
      }
    })
  }

  private selectChecks(){
    $('.jobCheck').each((index, el) => {
      if(!this.jobs) return;
      if(this.jobs.length > 0){
        let sec = this.jobs.find((job) => job.data.jobId == $(el).val() || job.data.id ==  $(el).val())
      
        if(sec != null){
            el.checked = true;  
            if(sec.data.id == null){
              sec.data.id = sec.data.jobId
            }
            this.jobSelected.push(sec.data);
        }
      }
   })
  }

  ngOnChanges(){
    this.patchJobs();
    this.blockUI.stop();
  }

  checkboxClicked(ev, job){

    if(ev.target.checked){
        //AddJob
        if(job.jobId != null){
          job.id = job.jobId;
        }
        this.jobSelected.push(job);
    }
    else{
        //Remove job
        let indexSectorToDelete = this.jobSelected.findIndex(x => x.sectorId == job.jobId || x.id == job.jobId);
        this.jobSelected.splice(indexSectorToDelete, 1);
    }

    this.jobsSelectedEmitter.emit({sector: this.sector, jobs: this.jobSelected});
  }

  ngOnDestroy(){
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

}
