import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RoleRoutingModule } from './role-routing.module';

import { CreateEditRoleComponent } from './create-edit-role/create-edit-role.component';
import { DetailRoleComponent } from './detail-role/detail-role.component';
import { ReadRolesComponent } from './read-roles/read-roles.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';

@NgModule({
  declarations: [
    CreateEditRoleComponent,
    DetailRoleComponent,
    ReadRolesComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    RoleRoutingModule
  ]
})
export class RoleModule { }
