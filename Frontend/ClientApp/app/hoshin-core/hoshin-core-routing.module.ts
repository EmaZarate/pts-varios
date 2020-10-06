import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ReadClaimsComponent } from './claim/read-claims/read-claims.component';
import { RoleGuardService } from '../core/services/role-guard.service';


const routes: Routes = [
    {
        path: 'jobs',
        loadChildren: './job/job.module#JobModule',
    },
    {
        path: 'associate',
        loadChildren: './job-sector-plant/job-sector-plant.module#JobSectorPlantModule',
    },
    {
        path: 'company',
        loadChildren: './company/company.module#CompanyModule'
    },
    {
        path: 'plants',
        loadChildren: './plant/plant.module#PlantModule'
    },
    {
        path: 'role',
        loadChildren: './role/role.module#RoleModule'
    },
    {
        path: 'sectors',
        loadChildren: './sector/sector.module#SectorModule'
    },
    {
        path: 'users',
        loadChildren: './user/user.module#UserModule'
    },
    
    {
        path: 'claims',
        component: ReadClaimsComponent
    },
    
]

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
  })

export class HoshinCoreRoutingModule { }