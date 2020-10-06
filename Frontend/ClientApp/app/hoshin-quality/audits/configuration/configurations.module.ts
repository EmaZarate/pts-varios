import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';

import { ConfigurationsRoutingModule } from './configurations-routing.module';

import { ReadComponent } from './aspect-state/read/read.component';
import { CreateEditComponent } from './aspect-state/create-edit/create-edit.component';
import { ReadAuditTypesComponent } from './audit-type/read-audit-types/read-audit-types.component';
import { CreateEditAuditTypesComponent } from './audit-type/create-edit-audit-types/create-edit-audit-types.component';
import { ReadStateComponent } from './state/read-state/read-state.component';
import { CreateEditStateComponent } from './state/create-edit-state/create-edit-state.component';
import { ReadStandardComponent } from './standard/read/read-standard.component';
import { CreateEditStandardComponent } from './standard/create-edit/create-edit-standard.component';

@NgModule({
  declarations: [ 
    ReadComponent,
    CreateEditComponent,
    ReadAuditTypesComponent, 
    CreateEditAuditTypesComponent,
    ReadStandardComponent,
    CreateEditStandardComponent,
    ReadStateComponent,
    CreateEditStateComponent, 
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    ConfigurationsRoutingModule
  ]
})
export class ConfigurationsModule { }
