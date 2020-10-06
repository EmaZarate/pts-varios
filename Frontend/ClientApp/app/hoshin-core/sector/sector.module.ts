import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SectorRoutingModule } from './sector-routing.module';

import { CreateEditSectorComponent } from './create-edit-sector/create-edit-sector.component';
import { ReadSectorsComponent } from './read-sectors/read-sectors.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';

@NgModule({
  declarations: [
    CreateEditSectorComponent,
    ReadSectorsComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    SectorRoutingModule
  ]
})
export class SectorModule { }
