import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ConfigurationRoutingModule } from './configuration-routing.module';

import { ReadComponent } from './parametrization-criteria/read/read.component';
import { CreateEditComponent } from './parametrization-criteria/create-edit/create-edit.component';
import { ReadFindingsTypesComponent } from './findings-type/read-findings-types/read-findings-types.component';
import { CreateEditFindingsTypesComponent } from './findings-type/create-edit-findings-types/create-edit-findings-types.component';
import { ReadComponent as ReadFindingStates} from './statuses/read/read.component';
import { CreateEditComponent as CreateEditFindingStatesComponent } from './statuses/create-edit/create-edit.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';

@NgModule({
  declarations: [
    ReadComponent,
    CreateEditComponent,
    ReadFindingStates,
    ReadFindingsTypesComponent,
    CreateEditFindingStatesComponent,
    CreateEditFindingsTypesComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    ConfigurationRoutingModule
  ]
})
export class ConfigurationModule { }
