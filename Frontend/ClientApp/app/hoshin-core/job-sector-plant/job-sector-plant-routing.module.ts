import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { D3tidytreeComponent } from './d3tidytree/d3tidytree.component';
import * as PERMISSIONS from '../../core/permissions/index';
import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';

const routes: Routes = [
  {
    path: 'tree',
    component: D3tidytreeComponent,
    data:{
      expectedClaim: PERMISSIONS.ADMINISTRATION.CONFIGURE_PLANTS
    },
    canActivate: [RoleGuardService]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class JobSectorPlantRoutingModule { }
