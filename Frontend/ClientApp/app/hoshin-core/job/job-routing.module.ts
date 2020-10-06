import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReadJobsComponent } from './read-jobs/read-jobs.component';
import { CreateEditJobComponent } from './create-edit-job/create-edit-job.component';
import * as PERMISSIONS from '../../core/permissions/index';
import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';

const routes: Routes = [
  {
    path: '',
    component: ReadJobsComponent,
    data: {
      expectedClaim: PERMISSIONS.JOB.VIEW_JOB
    },
    canActivate: [RoleGuardService]
  },
  {
    path: 'new',
    component: CreateEditJobComponent,
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.JOB.ADD_JOB
    },
    canActivate: [RoleGuardService]
  },
  {
    path: ':id/edit',
    component: CreateEditJobComponent,
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.JOB.EDIT_JOB
    },
    canActivate: [RoleGuardService]
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class JobRoutingModule { }
