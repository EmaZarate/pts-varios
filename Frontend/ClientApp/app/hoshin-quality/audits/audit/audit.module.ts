import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuditRoutingModule } from './audit-routing.module';
import { SharedModule } from 'ClientApp/app/shared/shared.module';
import { ListAuditComponent } from './list-audit/list-audit.component';
import { ScheduleAuditComponent } from './schedule-audit/schedule-audit.component';
import { ApproveRejectAuditComponent } from './approve-reject-audit/approve-reject-audit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { PlanAuditComponent } from './plan-audit/plan-audit.component';
import { CalendarAuditComponent } from './calendar-audit/calendar-audit.component';
import { EmitAuditReportComponent } from './emit-audit-report/emit-audit-report.component';
import { AddFindingComponent } from './add-finding/add-finding.component';
import { ApproveRejectAuditReportComponent } from './approve-reject-audit-report/approve-reject-audit-report.component';
import { DetailAuditComponent } from './detail-audit/detail-audit.component';

@NgModule({
  declarations: [
    ScheduleAuditComponent,
    ListAuditComponent,
      PlanAuditComponent,
      ApproveRejectAuditComponent,
      CalendarAuditComponent,
      EmitAuditReportComponent,
      AddFindingComponent,
      ApproveRejectAuditReportComponent,
      DetailAuditComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    AuditRoutingModule
  ]
})
export class AuditModule { }