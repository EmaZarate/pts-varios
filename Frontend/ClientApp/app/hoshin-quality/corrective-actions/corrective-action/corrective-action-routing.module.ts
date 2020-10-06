import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListCorrectiveActionsComponent } from './list-corrective-actions/list-corrective-actions.component';
import { ScheduleCorrectiveActionsComponent } from './schedule-corrective-actions/schedule-corrective-actions.component';
import { GenerateAcComponent } from './generate-ac/generate-ac.component';
import { FishboneComponent } from './fishbone/fishbone.component';
import { FishboneCategoriesResolver } from './fishbone-categories.resolver';
import { CorrectiveActionResolver } from './corrective-action.resolver';
import { EvaluateAcComponent } from './evaluate-ac/evaluate-ac.component';
import { DetailCorrectiveActionComponent } from './detail-corrective-action/detail-corrective-action.component';
import { FishboneCategoriesActiveResolver } from './fishbone-categories-active.resolver';
import { ExtendDueDateComponent } from './extend-due-date/extend-due-date.component';
import { OverduedEvaluateDateACComponent } from './overdued-evaluate-date-ac/overdued-evaluate-date-ac.component';
import { OverduedPlanningDateComponent } from './overdued-planning-date/overdued-planning-date.component';

import * as PERMISSIONS from '../../../core/permissions/index';
import { RoleGuardService } from '../../../core/services/role-guard.service';
import { CORRECTIVE_ACTION } from '../../../core/permissions/quality/corrective-action';
import { ADMINISTRATION } from '../../../core/permissions/core/administration';
import { ReassingCorrectiveActionsComponent } from './reassing-corrective-actions/reassing-corrective-actions.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',
  },
  {
    path: 'list',
    component: ListCorrectiveActionsComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION.READ
    }
  },
  {
    path: 'schedule',
    component: ScheduleCorrectiveActionsComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION.SCHEDULE
    }
  },
  {
    path: ':id/generate-ac',
    component: GenerateAcComponent,
    resolve: {
      correctiveAction: CorrectiveActionResolver,
      categories: FishboneCategoriesResolver
    },
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION.PLANNING
    }
  },
  {
    path: ':id/extend-due-date',
    component: ExtendDueDateComponent,
    resolve: {
      correctiveAction: CorrectiveActionResolver
    },
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION.EXTEND_EVALUATE_DUEDATE
    }
  },
  {
    path: 'fishbone',
    resolve: {
      categories: FishboneCategoriesResolver
    },
    component: FishboneComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FISHBONE_CATEGORY.READ
    }
  },
  {
    path: ':id/evaluate-ac',
    component: EvaluateAcComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION.EVALUATE
    }
  },
  {
    path: ':id/detail',
    component: DetailCorrectiveActionComponent,
    resolve: {
      categories: FishboneCategoriesResolver
    },
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION.READ
    }
  }, 
  { 
    path: ':id/overdue-evaluate-date',
    component: OverduedEvaluateDateACComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION.REQUEST_EVALUATE_DUEDATE_EXTENTION
    }
  },
  { 
    path: ':id/overdue-plannig',
    component: OverduedPlanningDateComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION.REQUEST_PLANNING_DUEDATE_EXTENTION
    }

  },
  {
    path: ':id/reassign',
    component: ReassingCorrectiveActionsComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.REASSIGN
    }
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationCorrectiveActionRoutingModule {}
