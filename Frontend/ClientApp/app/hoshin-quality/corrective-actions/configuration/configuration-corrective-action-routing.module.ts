import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CreateEditStateComponent } from './states/create-edit-state/create-edit-state.component';
import { ReadStateComponent } from './states/read-state/read-state.component';

import { ReadFishboneCategoriesComponent } from './fishbone-category/read-fishbone-categories/read-fishbone-categories.component';
import { NewFishBoneCategoryComponent } from './fishbone-category/new-fishbone-category/new-fishbone-category.component';
import { ReadParametrizationComponent } from './parametrization/read-parametrization/read-parametrization.component';
import { NewParametrizationComponent } from './parametrization/new-parametrization/new-parametrization.component';

import * as PERMISSIONS from '../../../core/permissions/index';
import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';

const routes: Routes = [

  {
    path: 'states',
    component: ReadStateComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION_STATE.READ
    }
  },
  {
    path: 'states/new',
    component: CreateEditStateComponent,
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION_STATE.ADD
    },
    canActivate: [RoleGuardService]
  },
  {
    path: 'states/:id/edit',
    component: CreateEditStateComponent,
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION_STATE.EDIT
    },
    canActivate: [RoleGuardService]
  },
  {
    path: 'fish-bone',
    component: ReadFishboneCategoriesComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.FISHBONE_CATEGORY.READ
    }
  },
  {
    path: 'fish-bone/:id/edit',
    component: NewFishBoneCategoryComponent,
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.FISHBONE_CATEGORY.EDIT
    },
    canActivate: [RoleGuardService]
  },
  {
    path: 'fish-bone/new',
    component: NewFishBoneCategoryComponent,
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.FISHBONE_CATEGORY.ADD
    },
    canActivate: [RoleGuardService]
  },
  {
    path: 'parametrization',
    component: ReadParametrizationComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION_PARAMETRIZATION.READ_CORRECTIVE_ACTION_PARAMETRIZATION
    }
  },
  {
    path: 'parametrization/:id/edit',
    component: NewParametrizationComponent,
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION_PARAMETRIZATION.EDIT_CORRECTIVEACTION_PARAMETRIZATION
    },
    canActivate: [RoleGuardService],
  },
  {
    path: 'parametrization/new',
    component: NewParametrizationComponent,
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.CORRECTIVE_ACTION_PARAMETRIZATION.ADD_CORRECTIVEACTION_PARAMETRIZATION
    },
    canActivate: [RoleGuardService],
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ConfigurationCorrectiveActionRoutingModule { }
