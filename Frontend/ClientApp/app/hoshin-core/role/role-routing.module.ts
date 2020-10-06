import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateEditRoleComponent } from './create-edit-role/create-edit-role.component';
import { ReadRolesComponent } from './read-roles/read-roles.component';
import { DetailRoleComponent } from './detail-role/detail-role.component';
import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';

import * as PERMISSIONS from '../../core/permissions/index';

const routes: Routes = [
  {
    path: '',
    component: ReadRolesComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedRole: PERMISSIONS.ROLE.VIEW_ROLE
    }
  },
  {
    path: 'new',
    component: CreateEditRoleComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'new',
      expectedRole: PERMISSIONS.ROLE.ADD_ROLE
    }
  },
  {
    path: ':id',
    component: DetailRoleComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedRole: PERMISSIONS.ROLE.VIEW_ROLE
    }
  },

  {
    path: ':id/edit',
    component: CreateEditRoleComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'edit',
      expectedRole: PERMISSIONS.ROLE.EDIT_ROLE
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RoleRoutingModule { }
