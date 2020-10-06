import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { EditTaskComponent } from './edit-task/edit-task.component';
import { ListTaskComponent } from './list-task/list-task.component';
import { DetailTaskComponent } from './detail-task/detail-task.component';
import { OverdueTaskComponent } from './overdue-task/overdue-task.component';
import { TaskResolver } from './task.resolver';
import { ExtendDueDateTaskComponent } from './extend-due-date-task/extend-due-date-task.component';

import * as PERMISSIONS from '../../../core/permissions/index';
import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';
import { TASK } from '../../../core/permissions/quality/task';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'list'
  },
  {
    path: 'list',
    component: ListTaskComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.TASK.READ
    }
  },
  {
    path: ':id/edit',
    component: EditTaskComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.TASK.EDIT
    }
  },
  {
    path: ':id/detail',
    component: DetailTaskComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.TASK.READ
    }
  },
  {
    path: ':id/overdue-task',
    component: OverdueTaskComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.TASK.REQUEST_DUE_DATE_EXTEND
    }
  },
  {
    path: ':id/extend-due-date-task',
    component: ExtendDueDateTaskComponent,
    resolve: {
      task: TaskResolver
    },
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.TASK.EXTEND_DUEDATE
    }
  }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TaskRoutingModule { }
