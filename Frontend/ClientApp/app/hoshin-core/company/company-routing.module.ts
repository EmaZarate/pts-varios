import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CreateEditCompanyComponent } from './create-edit-company/create-edit-company.component';
import { ReadCompanyComponent } from './read-company/read-company.component';
import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';
import * as PERMISSIONS from '../../core/permissions/index';

const routes: Routes = [
  {
    path: '',
    component: ReadCompanyComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.COMPANY.VIEW_COMPANY
    }
  },
  {
    path: 'new',
    component: CreateEditCompanyComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.COMPANY.ADD_COMPANY
    }
  },
  {
    path: ':id/edit',
    component: CreateEditCompanyComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.COMPANY.EDIT_COMPANY
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CompanyRoutingModule { }
