import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import * as PERMISSIONS from '../../../core/permissions/index';
import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';

import { ReadComponent } from "./aspect-state/read/read.component";
import { CreateEditComponent } from "./aspect-state/create-edit/create-edit.component";
import { ReadAuditTypesComponent } from './audit-type/read-audit-types/read-audit-types.component';
import { CreateEditAuditTypesComponent } from './audit-type/create-edit-audit-types/create-edit-audit-types.component';
import { ReadStateComponent } from "./state/read-state/read-state.component";
import { CreateEditStateComponent } from "./state/create-edit-state/create-edit-state.component"
import { ReadStandardComponent } from "./standard/read/read-standard.component";
import { CreateEditStandardComponent } from "./standard/create-edit/create-edit-standard.component";
const routes: Routes = [
  {
    path: 'aspect-states',
    component: ReadComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.ASPECT_STATE.READ
    }
  },
  {
    path: 'aspect-states/new',
    component: CreateEditComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.ASPECT_STATE.ADD,
      typeForm: 'new'
    }
  },
  {
    path: 'aspect-states/:id/edit',
    component: CreateEditComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.ASPECT_STATE.EDIT,
      typeForm: 'update'
    }
  },
  {
    path: 'types',
    component: ReadAuditTypesComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDITTYPES.READ
    }
  },
  {
    path: 'types/new',
    component: CreateEditAuditTypesComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.AUDITTYPES.ADD
    }
  },
  {
    path: 'types/:id/edit',
    component: CreateEditAuditTypesComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.AUDITTYPES.EDIT
    }
  },
  {
    path: 'state',
    component: ReadStateComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.AUDIT_STATE.READ
    }
  },
  {
    path: 'state/new',
    component: CreateEditStateComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.AUDIT_STATE.ADD
    }
  },
  {
    path: 'state/:id/edit',
    component: CreateEditStateComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.AUDIT_STATE.EDIT
    }
  },
  {
    path: 'standard',
    component: ReadStandardComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.STANDARD.READ
    }
  },
  {
    path: 'standard/new',
    component: CreateEditStandardComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.STANDARD.ADD
    }
  },
  {
    path: 'standard/:id/edit',
    component: CreateEditStandardComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.STANDARD.EDIT
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationsRoutingModule { }
