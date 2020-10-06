import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PlantRoutingModule } from './plant-routing.module';

import { CreateEditPlantComponent } from './create-edit-plant/create-edit-plant.component';
import { ReadPlantsComponent } from './read-plants/read-plants.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';

@NgModule({
  declarations: [
    CreateEditPlantComponent,
    ReadPlantsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    PlantRoutingModule
  ]
})
export class PlantModule { }
