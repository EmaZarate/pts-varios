import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AuditsRoutingModule } from './audits-routing.module';
import { SharedModule } from 'ClientApp/app/shared/shared.module';


@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    SharedModule,
    AuditsRoutingModule
  ]
})
export class AuditsModule { }
