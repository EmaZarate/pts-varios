import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { CreateEditUserComponent } from './create-edit-user/create-edit-user.component';
import { ReadUserComponent } from './read-user/read-user.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { SharedModule } from 'ClientApp/app/shared/shared.module';
import { SettingUserComponent } from './setting-user/setting-user.component';
import { RestartPasswordComponent } from './restart-password/restart-password.component';

@NgModule({
  declarations: [
    CreateEditUserComponent,
    ReadUserComponent,
    SettingUserComponent,
    RestartPasswordComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    UserRoutingModule
  ]
})
export class UserModule { }
