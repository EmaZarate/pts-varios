import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { FindingsRoutingModule } from './findings-routing.module';
import { SharedModule } from 'ClientApp/app/shared/shared.module';

@NgModule({
  declarations: [],
  imports: [
    SharedModule,
    FindingsRoutingModule
  ]
})
export class FindingsModule { }
