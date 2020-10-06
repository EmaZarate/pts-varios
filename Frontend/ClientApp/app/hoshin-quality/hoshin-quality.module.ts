import { NgModule, Input } from '@angular/core';
import { FormsModule, ReactiveFormsModule }from '@angular/forms';

import { BlockUIModule } from 'ng-block-ui'
import { SweetAlert2Module } from '@toverux/ngx-sweetalert2';

import { HoshinQualityRoutingModule } from './hoshin-quality-routing.module';


import { SharedModule } from 'ClientApp/app/shared/shared.module';




@NgModule({
  imports: [
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    HoshinQualityRoutingModule,
    BlockUIModule,
    SweetAlert2Module.forRoot({
      buttonsStyling: false,
      customClass: 'modal-content',
      confirmButtonClass: 'btn btn-primary',
      cancelButtonClass: 'btn'
    }),
  ],
  declarations: []
})
export class HoshinQualityModule { }
