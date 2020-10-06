import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { JobSectorPlantRoutingModule } from './job-sector-plant-routing.module';

import { D3tidytreeComponent } from './d3tidytree/d3tidytree.component';
import { PlantSectorFormComponent } from './plant-sector-form/plant-sector-form.component';
import { SectorJobFormComponent } from './sector-job-form/sector-job-form.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';

@NgModule({
  declarations: [
    D3tidytreeComponent,
    PlantSectorFormComponent,
    SectorJobFormComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    JobSectorPlantRoutingModule
  ]
})
export class JobSectorPlantModule { }
