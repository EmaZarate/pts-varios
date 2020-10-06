import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatInputModule, 
         MatGridListModule, 
         MatCardModule, 
         MatSelectModule, 
         MatTooltipModule, 
         MatTableModule, 
         MatIconModule, 
         MatCheckboxModule 
        } from '@angular/material';
import { MccColorPickerModule } from 'material-community-components';
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';
import { HoshinCoreRoutingModule } from './hoshin-core-routing.module';

import { CreateEditPlantComponent } from './plant/create-edit-plant/create-edit-plant.component';
import { ReadPlantsComponent } from './plant/read-plants/read-plants.component';
import { ReadClaimsComponent } from './claim/read-claims/read-claims.component';
import { CreateEditRoleComponent } from './role/create-edit-role/create-edit-role.component';
import { ReadRolesComponent } from './role/read-roles/read-roles.component';
import { DetailRoleComponent } from './role/detail-role/detail-role.component';
import { CreateEditSectorComponent } from './sector/create-edit-sector/create-edit-sector.component';
import { ReadSectorsComponent } from './sector/read-sectors/read-sectors.component';
import { CreateEditJobComponent } from './job/create-edit-job/create-edit-job.component';
import { ReadJobsComponent } from './job/read-jobs/read-jobs.component';
import { CreateEditUserComponent } from './user/create-edit-user/create-edit-user.component';
import { FieldErrorDisplayComponent } from '../shared/components/field-error-display/field-error-display.component';
import { ReadUserComponent } from './user/read-user/read-user.component';

import { TranslateClaimsPipe } from '../shared/pipes/translate-claims.pipe';
import { SharedModule } from 'ClientApp/app/shared/shared.module';
import { D3tidytreeComponent } from './job-sector-plant/d3tidytree/d3tidytree.component';
import { PlantSectorFormComponent } from './job-sector-plant/plant-sector-form/plant-sector-form.component';
import { SectorJobFormComponent } from './job-sector-plant/sector-job-form/sector-job-form.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    HoshinCoreRoutingModule,

  ],
  declarations: [
    ReadClaimsComponent,
  ]
})
export class HoshinCoreModule { }
