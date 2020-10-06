import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateEditUserComponent } from './create-edit-user/create-edit-user.component';
import { ReadUserComponent } from './read-user/read-user.component';
import { SettingUserComponent } from './setting-user/setting-user.component';

import * as PERMISSIONS from '../../core/permissions/index';
import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';
import { RestartPasswordComponent } from './restart-password/restart-password.component';


const routes: Routes = [
  {
    path: '',
    component: ReadUserComponent,
    data: {
      expectedClaim: PERMISSIONS.USER.VIEW_USER
    },
    canActivate: [RoleGuardService]
  },
  {
    path: 'new',
    component: CreateEditUserComponent,
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.USER.ADD_USER
    },
    canActivate: [RoleGuardService]
  },
  {
    path: ':id/edit',
    component: CreateEditUserComponent,
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.USER.EDIT_USER
    },
    canActivate: [RoleGuardService]
  },
  {
    path: ':id/setting',
    component: SettingUserComponent
  },
  {
    path: ':id/restartpassword',
    component: RestartPasswordComponent,
    data: {
      expectedClaim: PERMISSIONS.USER.VIEW_USER
    },
    canActivate: [RoleGuardService]
  },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserRoutingModule { }
