import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyRoutingModule } from './company-routing.module';

import { SharedModule } from 'ClientApp/app/shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CreateEditCompanyComponent } from './create-edit-company/create-edit-company.component';
import { ReadCompanyComponent } from './read-company/read-company.component';

@NgModule({
  declarations: [
    CreateEditCompanyComponent,
    ReadCompanyComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    CompanyRoutingModule
  ]
})
export class CompanyModule { }
