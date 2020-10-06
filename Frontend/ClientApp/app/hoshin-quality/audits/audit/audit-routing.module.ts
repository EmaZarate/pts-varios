import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListAuditComponent } from "./list-audit/list-audit.component";
import { ScheduleAuditComponent } from "./schedule-audit/schedule-audit.component";
import { ApproveRejectAuditComponent } from './approve-reject-audit/approve-reject-audit.component';
import { PlanAuditComponent } from "./plan-audit/plan-audit.component";
import { CalendarAuditComponent } from "./calendar-audit/calendar-audit.component";
import { EmitAuditReportComponent } from './emit-audit-report/emit-audit-report.component';
import { AddFindingComponent } from './add-finding/add-finding.component';
import { ApproveRejectAuditReportComponent } from './approve-reject-audit-report/approve-reject-audit-report.component';
import { DetailAuditComponent } from './detail-audit/detail-audit.component';
import { RoleGuardService } from '../../../core/services/role-guard.service';
import * as PERMISSIONS from '../../../core/permissions/index';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
  },
  {
    path: 'list',
    component: ListAuditComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.READAUDIT
    }
  },  
  {
    path: 'schedule',
    component: ScheduleAuditComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.SCHEDULE,
      typeForm: 'new'
    }
  },
  {
    path: ':id/detail',
    component: DetailAuditComponent ,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.READAUDIT
    }
  },
  {
    path: 'schedule/:id/edit',
    component: ScheduleAuditComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.SCHEDULE,
      typeForm: 'edit'
    }
  },
  {
    path: 'plan/:id',
    component: PlanAuditComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.PLANNING,
    }
  },
  {
    path: ':id/approve',
    component: ApproveRejectAuditComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.APPROVE_PLANNING,
    }
  },
  {
    path: 'calendar',
    component: CalendarAuditComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.READ_AUDIT_CALENDAR,
    }
  },
  {
    path: ':id/report',
    component: EmitAuditReportComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.EMMIT_REPORT,
    }
  },
  {
    path: ':id/report/:standard/add/:aspect',
    component: AddFindingComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.ADD_FINDINGS,
    }
  },
  {
    path: ':id/report/:finding/edit',
    component: AddFindingComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.ADD_FINDINGS,
    }
  },
  {
    path: ':id/report/approve',
    component: ApproveRejectAuditReportComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT.APPROVE_REPORT,
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuditRoutingModule { }
