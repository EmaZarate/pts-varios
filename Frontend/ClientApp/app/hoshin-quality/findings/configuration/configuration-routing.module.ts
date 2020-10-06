import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ReadComponent } from './parametrization-criteria/read/read.component';
import { CreateEditComponent } from './parametrization-criteria/create-edit/create-edit.component';
import { ReadFindingsTypesComponent } from './findings-type/read-findings-types/read-findings-types.component';
import { CreateEditFindingsTypesComponent } from './findings-type/create-edit-findings-types/create-edit-findings-types.component';
import { ReadComponent as ReadFindingStates} from './statuses/read/read.component';
import { CreateEditComponent as CreateEditFindingStatesComponent } from './statuses/create-edit/create-edit.component';

import * as PERMISSIONS from '../../../core/permissions/index';
import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';

const routes: Routes = [
  {
    path: 'parametrization-criteria',
    component: ReadComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING_PARAMETRIZATION_CRITERIA.READ
    }
  },
  {
    path: 'parametrization-criteria/new',
    component: CreateEditComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.FINDING_PARAMETRIZATION_CRITERIA.ADD
    }
  },
  {
    path: 'parametrization-criteria/:id/edit',
    component: CreateEditComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.FINDING_PARAMETRIZATION_CRITERIA.EDIT
    }
  },
  {
    path: 'types',
    component: ReadFindingsTypesComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING_TYPES.READ
    }
  },
  {
    path: 'types/new',
    component: CreateEditFindingsTypesComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING_TYPES.ADD
    }
  },
  {
    path: 'types/:id/edit',
    component: CreateEditFindingsTypesComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING_TYPES.EDIT
    }
  },
  {
    path: 'states',
    component: ReadFindingStates,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FINDING_STATES.READ
    }
  },
  {
    path: 'states/new',
    component: CreateEditFindingStatesComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.FINDING_STATES.ADD
    }
  },
  {
    path: 'states/:id/edit',
    component: CreateEditFindingStatesComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.FINDING_STATES.EDIT
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationRoutingModule { }
