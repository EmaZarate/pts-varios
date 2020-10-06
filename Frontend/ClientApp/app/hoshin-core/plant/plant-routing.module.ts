import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReadPlantsComponent } from './read-plants/read-plants.component';
import { CreateEditPlantComponent } from './create-edit-plant/create-edit-plant.component';
import { RoleGuardService } from 'ClientApp/app/core/services/role-guard.service';
import * as PERMISSIONS from '../../core/permissions/index';

const routes: Routes = [
  {
    path: '',
    component: ReadPlantsComponent,
    canActivate: [RoleGuardService],
    data: {
      expectedClaim: PERMISSIONS.PLANT.VIEW_PLANT
    }
  },
  {
    path: 'new',
    component: CreateEditPlantComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'new',
      expectedClaim: PERMISSIONS.PLANT.ADD_PLANT
    }
  },
  {
    path: ':id/edit',
    component: CreateEditPlantComponent,
    canActivate: [RoleGuardService],
    data: {
      typeForm: 'edit',
      expectedClaim: PERMISSIONS.PLANT.EDIT_PLANT
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PlantRoutingModule { }
