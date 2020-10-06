import { Component, OnInit, AfterViewInit, OnDestroy } from '@angular/core';
import { HomeService } from "../../../core/services/home.service";
import { Subject } from 'rxjs/Subject';
import { forkJoin } from 'rxjs';

import { AuthService } from 'ClientApp/app/core/services/auth.service';
import * as PERMISSIONS from '../../../core/permissions/index';
import { CorrectiveActionService } from 'ClientApp/app/hoshin-quality/corrective-actions/corrective-action.service';
import { CorrectiveAction } from 'ClientApp/app/hoshin-quality/corrective-actions/models/CorrectiveAction';
import { FindingsService } from 'ClientApp/app/core/services/findings.service';
import { Finding } from 'ClientApp/app/hoshin-quality/findings/models/Finding';
import { AuditService } from 'ClientApp/app/hoshin-quality/audits/audit.service';
import { Audit } from 'ClientApp/app/hoshin-quality/audits/models/Audit';
import { TaskService } from 'ClientApp/app/hoshin-quality/tasks/taskService.service';
import { Task } from 'ClientApp/app/hoshin-quality/corrective-actions/models/Task';
import { TaskConfigService } from 'ClientApp/app/hoshin-quality/corrective-actions/task.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit, OnDestroy {
  constructor(
    private homeService: HomeService,
    private authService: AuthService,
    private _correctiveActionService: CorrectiveActionService,
    private _findingService: FindingsService,
    private _auditService: AuditService,
    private _taskService: TaskConfigService) { }

  public findings: Finding[];
  public totalFindingCount: number;
  public ngUnsubscribe: Subject<void> = new Subject<void>();
  public audits: Audit[];
  public totalAudits: number;
  public totalCorrectiveActions: number;
  public correctiveActions: CorrectiveAction[];
  public tasks: Task[];
  public totalTasks: number;
  progress;
  canReadFinding;


  ngOnInit() {
    this.setPermissions();
    if (this.canReadFinding) {
      this.getTotalFindings();
      }
      this.getTotalAudits();
        this.getTotalCorrectiveActions();
        this.getTotalTasks();
  }

  ngAfterViewInit() {
    // if (this.canReadFinding) {
    //   this.homeService.getFindingsCount().takeUntil(this.ngUnsubscribe)
    //     .subscribe((res) => {
    //       this.totalFindingCount = res;
    //     });
    // }
    // this.homeService.getAuditsCount().takeUntil(this.ngUnsubscribe).subscribe((resp) => {
    //   this.totalAudits = resp;
    // });
  }
  getTotalTasks(){
    this._taskService.GetAllTaks()
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this.tasks = res;
          if(this.tasks.length > 0){
            this.totalTasks = this.tasks.length;
          }
          else{
            this.totalTasks = 0;
          }
        });
  }
  getTotalCorrectiveActions(){
    this._correctiveActionService.getAll()
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this.correctiveActions = res;
          if(this.correctiveActions.length > 0){
            this.totalCorrectiveActions = this.correctiveActions.length;
          }
          else{
            this.totalCorrectiveActions = 0;
          }
        });
  }
  getTotalFindings(){
    this._findingService.getAll()
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this.findings = res;
          if(this.findings.length > 0){
            this.totalFindingCount = this.findings.length;
          }
          else{
            this.totalFindingCount = 0;
          }
        });
  }
  getTotalAudits(){
    this._auditService.getAll()
        .takeUntil(this.ngUnsubscribe)
        .subscribe((res) => {
          this.audits = res;
          if(this.audits.length > 0){
            this.totalAudits = this.audits.length;
          }
          else{
            this.totalAudits = 0;
          }
        });
  }
  test(ev) {
    console.log(ev);
  }

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }

  uploadFiles(files) {
    let allProgressObservables = [];
    this.progress = this.homeService.upload(files);
    for (let key in this.progress) {
      allProgressObservables.push(this.progress[key].progress);
    };

    forkJoin(allProgressObservables).subscribe(end => {
      console.log(end);
      console.log("finished");
    })
  }

  setPermissions() {
    this.canReadFinding = this._canReadFinding();
  }

  private _canReadFinding() {
    return this.authService.hasClaim(PERMISSIONS.FINDING.READ_SECTOR);
  }
}
