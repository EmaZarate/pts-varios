import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ReadComponent } from './read/read.component';
import { CreateFindingComponent } from './create-finding/create-finding.component';
import { ViewFindingDetailComponent } from './view-finding-detail/view-finding-detail.component';
import { CloseFindingComponent } from './close-finding/close-finding.component';
import { ApproveFindingComponent } from './approve-finding/approve-finding.component';
import { RejectFindingComponent } from './reject-finding/reject-finding.component';
import { UpdateApprovedFindingComponent } from './update-approved-finding/update-approved-finding.component';
import { ReassingFindingComponent } from './reassing-finding/reassing-finding.component';
import { ApproveReassignmentComponent } from './approve-reassignment/approve-reassignment.component';
import { EditExpirationdateFindingComponent } from './edit-expirationdate-finding/edit-expirationdate-finding.component';

import * as PERMISSIONS from '../../../core/permissions/index';
import { RoleGuardService } from '../../../core/services/role-guard.service';
import { RejectReassignmentComponent } from './reject-reassignment/reject-reassignment.component';


const routes: Routes = [
  {
    path: '',
    redirectTo: 'list',

  },
  {
    path: 'list',
    component: ReadComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.READ_SECTOR
    }
  },
  {
    path: 'new',
    component: CreateFindingComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.ADD
    }
  },
  {
    path: ':id/detail',
    component: ViewFindingDetailComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.READ_SECTOR
    }
  },
  {
    path: ':id/close',
    component: CloseFindingComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.CLOSE
    }
  },
  {
    path: ':id/approve',
    component: ApproveFindingComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.APPROVE
    }
  },
  {
    path: ':id/reject',
    component: RejectFindingComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.REJECT
    }
  },
  {
    path: ':id/update',
    component: UpdateApprovedFindingComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.UPDATE_APPROVED
    }
  },
  {
    path: ':id/reassign',
    component: ReassingFindingComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.REASSIGN
    }
  },
  {
    path: ':id/approvereassignment',
    component: ApproveReassignmentComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.APPROVE_REASSIGNMENT
    }
  },
  {
    path: ':id/rejectreassignment',
    component: RejectReassignmentComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.APPROVE_REASSIGNMENT
    }
  },
  {
    path: ':id/editexpirationdate',
    component: EditExpirationdateFindingComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING.EDIT_EXPIRATION_DATE
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class FindingRoutingModule { }
