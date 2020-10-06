import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CorrectiveActionsRoutingModule } from './corrective-actions-routing.module';

import { SharedModule } from 'ClientApp/app/shared/shared.module';


@NgModule({
  declarations: [
 ],
  imports: [
    CommonModule,
    SharedModule,
    CorrectiveActionsRoutingModule,
  ]
})
export class CorrectiveActionsModule { }
