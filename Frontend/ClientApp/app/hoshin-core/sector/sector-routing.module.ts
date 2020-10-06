import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReadSectorsComponent } from './read-sectors/read-sectors.component';
import { CreateEditSectorComponent } from './create-edit-sector/create-edit-sector.component';

import * as PERMISSIONS from '../../core/permissions/index';

import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';

const routes: Routes = [
  {
    path: '',
    component: ReadSectorsComponent,
    data: {
      expectedClaim: PERMISSIONS.SECTOR.VIEW_SECTOR
    },
    canActivate: [RoleGuardService]
  },
  {
    path: 'new',
    component: CreateEditSectorComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.SECTOR.ADD_SECTOR
    }
  },
  {
    path: ':id/edit',
    component: CreateEditSectorComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.SECTOR.EDIT_SECTOR
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SectorRoutingModule { }
